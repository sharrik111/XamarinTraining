using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Ads;
using AdsHub.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdBannerView), typeof(AdsHub.Droid.Renderers.AdBannerRenderer))]
namespace AdsHub.Droid.Renderers
{
    public class AdBannerRenderer : ViewRenderer<AdBannerView, AdView>
    {
        private AdView adView;
        private AdSize adSize = AdSize.SmartBanner;

        AdView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            adView = new AdView(Forms.Context);
            adView.AdSize = adSize;
            adView.AdUnitId = "ca-app-pub-3940256099942544/6300978111";

            var adParams = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                            .Builder()
                            .Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdBannerView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }
}