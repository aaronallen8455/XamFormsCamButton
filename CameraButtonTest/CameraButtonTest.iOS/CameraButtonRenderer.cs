using System;
using System.ComponentModel;
using CameraButtonTest;
using CameraButtonTest.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CameraButton), typeof(CameraButtonRenderer))]
namespace CameraButtonTest.iOS
{
    public class CameraButtonRenderer : ViewRenderer<CameraButton, CameraButtonView>
    {
		protected override void OnElementChanged(ElementChangedEventArgs<CameraButton> e)
		{
            base.OnElementChanged(e);

            if (Control == null && Element != null)
            {
                SetNativeControl(new CameraButtonView(Element));

                Control.TouchUpInside += Control_TouchUp;
                Control.TouchDown += Control_TouchDown;
            }

            if (e.OldElement != null)
            {
                Control.TouchUpInside -= Control_TouchUp;
                Control.TouchDown -= Control_TouchDown;
            }
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CameraButton.CompletionProperty.PropertyName)
            {
                // redraw view when completion property changes
                Control.Layer.SetNeedsDisplay();
            }
		}

        void Control_TouchUp(object sender, EventArgs e)
        {
            Element.OnReleased();
        }

        void Control_TouchDown(object sender, EventArgs e)
        {
            Element.OnPressed();
        }
	}
}
