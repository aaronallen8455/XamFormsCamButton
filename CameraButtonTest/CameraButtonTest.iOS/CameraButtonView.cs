using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CameraButtonTest.iOS
{
    public class CameraButtonView : UIControl, ICALayerDelegate
    {
        const float BorderThickness = 6;

        protected CameraButton FormsButton;

        public CameraButtonView(CameraButton cameraButton)
        {
            BackgroundColor = UIColor.Clear;
            FormsButton = cameraButton;
            Layer.SetNeedsDisplay();
        }

		public override void DrawLayer(CALayer layer, CGContext context)
		{
            var radius = (float)FormsButton.WidthRequest / 2;
            var offset = BorderThickness / 2;
            float angle = (FormsButton.Completion + .75f) * 2 * (float)Math.PI;

            context.AddEllipseInRect(CGRect.FromLTRB(offset, offset, (float)FormsButton.WidthRequest - offset, (float)FormsButton.HeightRequest - offset));
            context.SetStrokeColor(UIColor.White.CGColor);
            context.SetLineWidth(BorderThickness);
            context.StrokePath();

            context.SetStrokeColor(UIColor.Red.CGColor);
            context.AddArc(radius, radius, radius - offset, 1.5f * (float)Math.PI, angle, false);
            context.StrokePath();
		}
    }
}
