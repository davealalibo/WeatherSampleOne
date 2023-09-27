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


            SearchButton.SetTitle("Search Weather Info", UIControlState.Normal);
            SearchButton.SetTitle("Please wait...", UIControlState.Disabled);

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
            string theMessage = string.Empty;
            SearchButton.Enabled = false;

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
                        SearchButton.Enabled = true;
                        theMessage = string.Join("\n", viewModel.Errors.Values);
                        CityTextField.SetError(theMessage);
                        ShowErrorMessage(theMessage);
                    }

                }
                else
                {
                    SearchButton.Enabled = true;
                    theMessage = "Could not get the geo-location of the entered city";
                    CityTextField.SetError(theMessage);
                    ShowErrorMessage(theMessage);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                SearchButton.Enabled = true;
                // Feature not supported on device
                theMessage = "Geocoding not support on this device";
                CityTextField.SetError(theMessage);
                Console.WriteLine(fnsEx.Message);
                ShowErrorMessage(theMessage);
            }
            catch (Exception ex)
            {
                SearchButton.Enabled = true;
                // Handle exception that may have occurred in geocoding
                theMessage = "Sorry, an error occurred. Could not get the geo-location of the entered city. Please check and enter a valid city.";
                CityTextField.SetError(theMessage);
                Console.WriteLine(ex.Message);
                ShowErrorMessage(theMessage);
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
                SearchButton.Enabled = true;

                Console.WriteLine(viewModel.TheWeatherResponse);
                Console.WriteLine(obj);

                PerformSegue("WeatherResultSegue", this);

            });
        }

        private void onGetWeatherAsyncError(ApiError obj)
        {
            InvokeOnMainThread(() =>
            {
                SearchButton.Enabled = true;

                ShowErrorMessage(obj?.ConcatenatedErrors);
            });
        }

        private void ShowErrorMessage(string message)
        {
            // Display an alert with the error message
            var alert = UIAlertController.Create("Error", message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);
        }
    }
}
