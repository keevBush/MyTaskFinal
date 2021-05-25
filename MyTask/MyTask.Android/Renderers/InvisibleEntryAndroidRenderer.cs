using Android.Content;
using Android.Graphics.Drawables;
using MyTask.CustomControls;
using MyTask.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(InvisibleEntry), typeof(InvisibleEntryAndroidRenderer))]
namespace MyTask.Droid.Renderers
{
    public class InvisibleEntryAndroidRenderer:EntryRenderer
    {
        public InvisibleEntryAndroidRenderer(Context context):base(context){}

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
        }
    }
}