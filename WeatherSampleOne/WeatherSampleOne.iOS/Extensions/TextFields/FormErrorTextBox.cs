using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using UIKit;

namespace WeatherSampleOne.iOS.Extensions.TextFields
{
    [Register("FormErrorTextBox"), DesignTimeVisible(true)]
    public class FormErrorTextBox : UITextField, IUITextFieldDelegate
    {
        private UILabel errorLabel;
        private int _numberOfCharacters;
        private float borderWidth = 1;
        private UIColor borderColor = UIColor.LightGray;
        private bool hasError = false;

        private float cornerRadius = 5.0F;

        [Export("NumberOfCharacters"), Browsable(true)]
        public int NumberOfCharacters
        {
            get { return _numberOfCharacters; }
            set { _numberOfCharacters = value; }
        }

        [Export("CornerRadius"), Browsable(true)]
        public float CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; }
        }

        [Export("BorderWidth"), Browsable(true)]
        public float BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        [Export("BorderColor"), Browsable(true)]
        public UIColor BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public FormErrorTextBox(IntPtr p)
            : base(p)
        {
            this.Delegate = this;

        }
        public FormErrorTextBox()
        {
            this.Delegate = this;

        }

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public new bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            if (NumberOfCharacters > 0)
            {
                if (((Text ?? "") + replacementString).Length > NumberOfCharacters)
                    return false;

            }
            return true;
        }
        public void SetError(string message, NSLayoutXAxisAnchor rightAnchor = null)
        {
            if (errorLabel == null) return;

            if (string.IsNullOrWhiteSpace(message))
            {
                RemoveError();
                return;
            }
            hasError = true;
            errorLabel.Text = message;
            this.Superview.AddSubview(errorLabel);
            errorLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] {
                errorLabel.LeftAnchor.ConstraintEqualTo(this.LeftAnchor),
                errorLabel.TopAnchor.ConstraintEqualTo(this.BottomAnchor,4),
                errorLabel.RightAnchor.ConstraintEqualTo(rightAnchor != null ? rightAnchor : this.RightAnchor),
            });
            this.SetNeedsDisplay();
        }

        public void RemoveError()
        {
            hasError = false;
            errorLabel?.RemoveFromSuperview();
            SetNeedsDisplay();
        }

        protected virtual void PartialSetup()
        {
            ReturnKeyType = UIReturnKeyType.Done;

            Layer.BorderColor = BorderColor.CGColor;
            Layer.BorderWidth = BorderWidth;
            Layer.CornerRadius = CornerRadius;

            errorLabel = new UILabel();
            errorLabel.TextColor = UIColor.Red;
            errorLabel.Font = UIFont.SystemFontOfSize(10, UIFontWeight.Light);
            errorLabel.Lines = 2;

            TranslatesAutoresizingMaskIntoConstraints = false;
            HeightAnchor.ConstraintEqualTo(40).Active = true;

        }

        public void SetBorderColor()
        {
            this.Layer.BorderColor = (this.hasError ? UIColor.Red : BorderColor).CGColor;
        }

        public void SetLimit(int maximum)
        {
            if (Text.Length > maximum)
            {
                Text = Text.Substring(0, Text.Length - 1);
            }
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            SetBorderColor();

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            PartialSetup();
        }

    }
}