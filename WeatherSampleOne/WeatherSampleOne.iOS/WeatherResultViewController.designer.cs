// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WeatherSampleOne.iOS
{
	[Register ("WeatherResultViewController")]
	partial class WeatherResultViewController
	{
		[Outlet]
		UIKit.UIButton GoBackButton { get; set; }

		[Outlet]
		UIKit.UIButton SaveCityAndGoBackButton { get; set; }

		[Outlet]
		UIKit.UILabel TemperaturesDescriptionLabel { get; set; }

		[Outlet]
		UIKit.UILabel WeatherDescriptionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (WeatherDescriptionLabel != null) {
				WeatherDescriptionLabel.Dispose ();
				WeatherDescriptionLabel = null;
			}

			if (TemperaturesDescriptionLabel != null) {
				TemperaturesDescriptionLabel.Dispose ();
				TemperaturesDescriptionLabel = null;
			}

			if (SaveCityAndGoBackButton != null) {
				SaveCityAndGoBackButton.Dispose ();
				SaveCityAndGoBackButton = null;
			}

			if (GoBackButton != null) {
				GoBackButton.Dispose ();
				GoBackButton = null;
			}
		}
	}
}
