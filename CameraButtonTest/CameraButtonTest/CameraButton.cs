using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CameraButtonTest
{
    public class CameraButton : View
    {
        /// <summary>
        /// Maximum video duration in seconds
        /// </summary>
        const int MAX_VID_DURATION = 10;

        #region Properties
        public static readonly BindableProperty CompletionProperty = BindableProperty.Create("Completion", typeof(float), typeof(CameraButton), 0f);

        /// <summary>
        /// A value from 0 to 1 that determines how much of the completion arc is drawn.
        /// </summary>
        public float Completion
        {
            get => (float)GetValue(CompletionProperty);
            protected set
            {
                SetValue(CompletionProperty, value);
            }
        }

        public static readonly BindableProperty TakePictureCommandProperty = BindableProperty.Create("TakePictureCommand", typeof(ICommand), typeof(CameraButton), propertyChanged: TakePictureChanged);

        /// <summary>
        /// Executes when button is tapped
        /// </summary>
        public ICommand TakePictureCommand
        {
            get => (ICommand)GetValue(TakePictureCommandProperty);
            set => SetValue(TakePictureCommandProperty, value);
        }

        static protected void TakePictureChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CameraButton).TakePictureCommand = (ICommand)newValue;
        }

        public static readonly BindableProperty StartTakingVideoCommandProperty = BindableProperty.Create("StartTakingVideoCommand", typeof(ICommand), typeof(CameraButton), propertyChanged: StartVideoChanged);

        /// <summary>
        /// Executes when starting to capture video
        /// </summary>
        public ICommand StartTakingVideoCommand
        {
            get => (ICommand)GetValue(StartTakingVideoCommandProperty);
            set => SetValue(StartTakingVideoCommandProperty, value);
        }

        static protected void StartVideoChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CameraButton).StartTakingVideoCommand = (ICommand)newValue;
        }

        public static readonly BindableProperty StopTakingVideoCommandProperty = BindableProperty.Create("StopTakingVideoCommand", typeof(ICommand), typeof(CameraButton), propertyChanged: StopVideoChanged);

        /// <summary>
        /// Executes when stopping video recording
        /// </summary>
        public ICommand StopTakingVideoCommand
        {
            get => (ICommand)GetValue(StopTakingVideoCommandProperty);
            set => SetValue(StopTakingVideoCommandProperty, value);
        }

        static protected void StopVideoChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CameraButton).StopTakingVideoCommand = (ICommand)newValue;
        }
        #endregion


        public CameraButton() : base()
        {

        }

        protected bool IsHeldDown;
        protected bool WasNotHeldDown;

        /// <summary>
        /// Called when the button is pressed
        /// </summary>
        public void OnPressed()
        {
            if (!this.AnimationIsRunning("DrawArc"))
            {
                WasNotHeldDown = false;
                IsHeldDown = false;

                // set a timer to determine whether button is being held down or tapped.
                Device.StartTimer(TimeSpan.FromMilliseconds(150), () =>
                {

                    // invoke the start taking video command if button is being held down
                    if (!WasNotHeldDown && (StartTakingVideoCommand == null || StartTakingVideoCommand.CanExecute(null)))
                    {
                        var animation = new Animation(v => Completion = (float)v, 0, 1, Easing.Linear);
                        animation.Commit(this, "DrawArc", 42, MAX_VID_DURATION * 1000, finished: (_,c) => StopRecordingVideo());
                        IsHeldDown = true;

                        StartTakingVideoCommand?.Execute(null);
                    }

                    return false;
                });
            }
        }

        /// <summary>
        /// Called when the button is released
        /// </summary>
        public void OnReleased()
        {
            if (!IsHeldDown)
            {
                WasNotHeldDown = true;
                // invoke the take picture command
                if (TakePictureCommand != null && TakePictureCommand.CanExecute(null))
                {
                    TakePictureCommand?.Execute(null);
                }
            }

            IsHeldDown = false;

            // reset the animation
            if (this.AnimationIsRunning("DrawArc"))
            {
                this.AbortAnimation("DrawArc");
            }
        }

        /// <summary>
        /// Called when DrawArc animation is aborted or when it completes, signifies that recording is complete
        /// </summary>
        protected void StopRecordingVideo()
        {
            if (StopTakingVideoCommand != null && StopTakingVideoCommand.CanExecute(null))
            {
                StopTakingVideoCommand?.Execute(null);
            }

            Completion = 0;
        }
    }
}
