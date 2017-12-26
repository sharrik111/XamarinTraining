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
using Xamarin.Forms.Platform.Android;
using AdsHub.Controls;
using Android.Gms.Ads.Formats;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AdNativeView), typeof(AdsHub.Droid.Renderers.AdNativeRenderer))]
namespace AdsHub.Droid.Renderers
{
    public class AdNativeRenderer : ViewRenderer<AdNativeView, NativeAppInstallAdView>
    {
        NativeAppInstallAd ad = null;

        void LaunchAdLoader()
        {
            if (ad != null) return;

            var adLoader = new AdLoader.Builder(Xamarin.Forms.Forms.Context, "ca-app-pub-3940256099942544/2247696110")
                                                .ForAppInstallAd(new NativeAppInstallAdListener { Renderer = this })
                                                .WithAdListener(new NativeAdListener()).Build();

            adLoader.LoadAd(new AdRequest.Builder().Build());
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdNativeView> e)
        {
            base.OnElementChanged(e);
            if (ad == null)
            {
                LaunchAdLoader();
                // SetNativeControl(adView);
            }
        }

        public void SetElement(NativeAppInstallAd ad)
        {
            var adView = new NativeAppInstallAdView(Xamarin.Forms.Forms.Context);
            // Here we can set views to adView.
            // var textView = new TextView(Xamarin.Forms.Forms.Context);
            adView.SetNativeAd(ad);
            SetNativeControl(adView);
        }
    }

    class NativeAppInstallAdListener : NativeAppInstallAd.IOnAppInstallAdLoadedListener
    {
        public AdNativeRenderer Renderer { get; set; }

        public IntPtr Handle => new IntPtr();

        public void Dispose()
        {
            
        }

        public void OnAppInstallAdLoaded(NativeAppInstallAd ad)
        {
            Renderer.SetElement(ad);
        }
    }

    class NativeAdListener : AdListener
    {
        public override void OnAdFailedToLoad(int errorCode)
        {
            base.OnAdFailedToLoad(errorCode);
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
        }
    }
}