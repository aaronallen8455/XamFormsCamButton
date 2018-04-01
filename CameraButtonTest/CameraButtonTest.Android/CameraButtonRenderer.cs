using System.ComponentModel;

using Android.Content;

using Android.Views;
using CameraButtonTest;
using CameraButtonTest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CameraButton), typeof(CameraButtonRenderer))]
namespace CameraButtonTest.Droid
{
    public class CameraButtonRenderer : ViewRenderer<CameraButton, CameraButtonView>
    {

        public CameraButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<CameraButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null && Element != null)
            {
                SetNativeControl(new CameraButtonView(Context, Element));

                Control.Touch += Control_Touch;
            }

            if (e.OldElement != null)
            {
                Control.Touch -= Control_Touch;
            }
        }

        /// <summary>
        /// Handles touch events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Touch(object sender, TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Down)
            {
                Element.OnPressed();
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                Element.OnReleased();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CameraButton.CompletionProperty.PropertyName)
            {
                Control.Invalidate();
            }
        }
    }
}