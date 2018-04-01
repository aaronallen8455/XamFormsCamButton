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
        protected CameraButton FormsButton;

        Paint ArcPaint;

        Paint CirclePaint;

        Paint BorderPaint;

        public CameraButtonView(Context context, CameraButton cameraButton) : base(context)
        {
            FormsButton = cameraButton;

            ArcPaint = new Paint(PaintFlags.AntiAlias) { Color = Color.Red, StrokeWidth = 10 };
            ArcPaint.SetStyle(Paint.Style.Stroke);

            CirclePaint = new Paint(PaintFlags.AntiAlias) { Color = Color.LightGray };
            CirclePaint.SetStyle(Paint.Style.Fill);

            BorderPaint = new Paint(PaintFlags.AntiAlias) { Color = Color.DarkGray, StrokeWidth = 10 };
            BorderPaint.SetStyle(Paint.Style.Stroke);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // Draw background
            canvas.DrawCircle(300, 300, 300, CirclePaint);
            canvas.DrawCircle(300, 300, 300, BorderPaint);

            float angle = FormsButton.Completion * 360;

            // Draw completion arc
            Path path = new Path();
            RectF rectf = new RectF(0, 0, 600, 600);
            path.ArcTo(rectf, 270, angle);
            canvas.DrawPath(path, ArcPaint);
        }
    }
}