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
using Xamarin.Forms.Platform.Android;
using AdsHub.Controls;
using Android.Gms.Ads;
using System.Threading;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AdPageView), typeof(AdsHub.Droid.Renderers.AdPageRenderer))]
namespace AdsHub.Droid.Renderers
{
    public class AdPageRenderer : ViewRenderer<AdPageView, View>
    {
        private View adView;
        private InterstitialAd pageAd;

        View CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            adView = new View(Xamarin.Forms.Forms.Context);
            pageAd = new InterstitialAd(Xamarin.Forms.Forms.Context);
            pageAd.AdUnitId = "ca-app-pub-3940256099942544/1033173712";
            pageAd.AdListener = new PageAdListener { Ad = pageAd };

            pageAd.LoadAd(new AdRequest
                            .Builder()
                            .Build());
            ThreadPool.QueueUserWorkItem(s =>
            {
                // Wait the ad to be loading.
                Thread.Sleep(20000);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (pageAd.IsLoaded)
                        pageAd.Show();
                });
            });

            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdPageView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }

    class PageAdListener : AdListener
    {
        public InterstitialAd Ad { get; set; }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();
            Ad.Show();
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            Ad.LoadAd(new AdRequest.Builder().Build());
        }

        public override void OnAdFailedToLoad(int errorCode)
        {
            base.OnAdFailedToLoad(errorCode);
            Ad.LoadAd(new AdRequest.Builder().Build());
        }
    }
}