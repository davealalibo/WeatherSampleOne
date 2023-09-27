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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		WeatherSampleOne.iOS.Extensions.TextFields.FormTextBox CityTextField { get; set; }

		[Outlet]
		UIKit.UIButton SearchButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}

			if (CityTextField != null) {
				CityTextField.Dispose ();
				CityTextField = null;
			}
		}
	}
}
