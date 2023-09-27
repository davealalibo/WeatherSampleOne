using System;
using CoreGraphics;
using Foundation;
using System.ComponentModel;
using UIKit;

namespace WeatherSampleOne.iOS.Extensions.TextFields
{
    [Register("FormTextBox"), DesignTimeVisible(true)]
    public partial class FormTextBox : FormErrorTextBox
    {
        public FormTextBox(IntPtr p)
            : base(p)
        {

        }
    }
}

