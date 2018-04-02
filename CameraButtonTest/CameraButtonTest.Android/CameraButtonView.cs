using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CameraButtonTest.Droid
{
    public class CameraButtonView : View
    {
        const float BorderThickness = 16;

        protected CameraButton FormsButton;

        Paint ArcPaint;

        //Paint CirclePaint;

        Paint BorderPaint;

        public CameraButtonView(Context context, CameraButton cameraButton) : base(context)
        {
            FormsButton = cameraButton;

            ArcPaint = new Paint(PaintFlags.AntiAlias) { Color = Color.Red, StrokeWidth = BorderThickness };
            ArcPaint.SetStyle(Paint.Style.Stroke);

            //CirclePaint = new Paint(PaintFlags.AntiAlias) { Color = Color.LightGray };
            //CirclePaint.SetStyle(Paint.Style.Fill);

            BorderPaint = new Paint(PaintFlags.AntiAlias) { Color = Color.White, StrokeWidth = BorderThickness };
            BorderPaint.SetStyle(Paint.Style.Stroke);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // Draw background
            var radius = (float)FormsButton.WidthRequest * Resources.DisplayMetrics.Density / 2;
            var offset = BorderThickness / 2;
            //canvas.DrawCircle(radius, radius, radius - 5, CirclePaint);
            canvas.DrawCircle(radius, radius, radius - offset, BorderPaint);

            float angle = FormsButton.Completion * 360;

            // Draw completion arc
            Path path = new Path();
            RectF rectf = new RectF(offset, offset, radius * 2 - offset, radius * 2 - offset);
            path.ArcTo(rectf, 270, angle);
            canvas.DrawPath(path, ArcPaint);
        }
    }
}