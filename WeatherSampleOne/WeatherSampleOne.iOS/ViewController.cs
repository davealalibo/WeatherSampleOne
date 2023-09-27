using CoreLocation;
using Foundation;
using System;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
using WeatherSampleOne.Config;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;
using WeatherSampleOne.Services.Factory;
using WeatherSampleOne.ViewModels;
using Xamarin.Essentials;

namespace WeatherSampleOne.iOS
{
    public partial class ViewController : UIViewController
    {
        WeatherSearchViewModel viewModel;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.NavigationController.SetNavigationBarHidden(true, false);
            //this.NavigationController.NavigationBarHidden = true;

            string savedFavouriteCity = Preferences.Get(Constants.FAVOURITE_CITY, "");
            if (!string.IsNullOrEmpty(savedFavouriteCity))
            {
                CityTextField.Text = savedFavouriteCity;
                viewModel.TheWeatherRequest.City = savedFavouriteCity;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            this.NavigationController.SetNavigationBarHidden(false, false);
            //this.NavigationController.NavigationBarHidden = false;
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            viewModel = ServiceFactory.Resolve<WeatherSearchViewModel>();
            viewModel.TheWeatherRequest = new WeatherRequest();

            CityTextField.EditingChanged += CityTextField_EditingChanged;

            SearchButton.TouchUpInside += SearchButton_TouchUpInside;

        }

        private void CityTextField_EditingChanged(object sender, EventArgs e)
        {
            CityTextField.RemoveError();
            viewModel.TheWeatherRequest.City = CityTextField.Text;
            if (string.IsNullOrEmpty(CityTextField.Text))
            {
                CityTextField.SetError("City is required");
            }
        }

        private void SearchButton_TouchUpInside(object sender, EventArgs e)
        {
            ClearFieldErrors();

            viewModel.TheWeatherRequest.City = CityTextField.Text;

            if (viewModel.ValidateCity())
            {
                GetGeolocationFromCity();
            }
            else
            {
                if (viewModel.Errors.ContainsKey(nameof(viewModel.TheWeatherRequest.City)))
                {
                    CityTextField.SetError(viewModel.Errors[nameof(viewModel.TheWeatherRequest.City)]);
                }
            }

        }

        private async void GetGeolocationFromCity()
        {
            try
            {
                var locations = await Xamarin.Essentials.Geocoding.GetLocationsAsync(viewModel.TheWeatherRequest.City);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    viewModel.TheWeatherRequest.Latitude = location.Latitude;
                    viewModel.TheWeatherRequest.Longitude = location.Longitude;

                    if (viewModel.ValidateCoordinates())
                    {
                        GetWeatherAsync();
                    }
                    else
                    {
                        CityTextField.SetError(string.Join("\n", viewModel.Errors.Values));
                    }

                }
                else
                {
                    CityTextField.SetError("Could not get the geo-location of the entered city");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                CityTextField.SetError("Geocoding not support on this device");
                Console.WriteLine(fnsEx.Message);
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                CityTextField.SetError("Sorry, an error occurred");
                Console.WriteLine(ex.Message);
            }
        }

        private void ClearFieldErrors()
        {
            CityTextField.RemoveError();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        private async void GetWeatherAsync()
        {
            await viewModel.GetWeatherAsync(onGetWeatherAsyncSuccess, onGetWeatherAsyncError);
        }

        private void onGetWeatherAsyncSuccess(WeatherResponse obj)
        {
            InvokeOnMainThread(() =>
            {
                Console.WriteLine(viewModel.TheWeatherResponse);
                Console.WriteLine(obj);
            });
        }

        private void onGetWeatherAsyncError(ApiError obj)
        {
            InvokeOnMainThread(() =>
            {
                CityTextField.SetError(obj?.ConcatenatedErrors);
            });
        }
    }
}
