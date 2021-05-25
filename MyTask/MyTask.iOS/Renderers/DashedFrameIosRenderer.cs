using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MyTask.CustomControls;
using MyTask.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(DashedFrame), typeof(DashedFrameIosRenderer))]
namespace MyTask.iOS.Renderers
{
    public class DashedFrameIosRenderer:FrameRenderer
    {
        public DashedFrameIosRenderer():base(){}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (Element != null)
            {
                Element.IsClippedToBounds = true;
                Layer.CornerRadius = 30;
                
                CAShapeLayer viewBorder = new CAShapeLayer();
                viewBorder.StrokeColor = Color.FromHex("#C1C8D1").ToUIColor().CGColor;
                viewBorder.FillColor = null;
                viewBorder.LineDashPattern = new NSNumber[] { new NSNumber(5), new NSNumber(4) };
                viewBorder.Frame = Layer.Bounds;
                viewBorder.Path = UIBezierPath.FromRoundedRect(this.Bounds, UIRectCorner.AllCorners,radii: new CGSize(20,60)).CGPath;

                if (Layer.Sublayers != null && Layer.Sublayers.Length >= 2)
                {
                    Layer.ReplaceSublayer(Layer.Sublayers[1], viewBorder);

                }
                else
                {
                    Layer.AddSublayer(viewBorder);
                }
                // If you don't want the shadow effect
                Element.HasShadow = false;
            }
            

        }
    }
}