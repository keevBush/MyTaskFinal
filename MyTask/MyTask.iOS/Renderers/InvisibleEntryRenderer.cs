using MyTask.CustomControls;
using MyTask.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(InvisibleEntry), typeof(InvisibleEntryRenderer))]
namespace MyTask.iOS.Renderers
{
    public class InvisibleEntryRenderer:EntryRenderer
    {
        public InvisibleEntryRenderer() : base()
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.BackgroundColor = UIColor.Clear;
            }
        }
    }
}