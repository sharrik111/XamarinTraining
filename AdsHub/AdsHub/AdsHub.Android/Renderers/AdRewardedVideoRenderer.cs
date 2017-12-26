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
using Android.Gms.Ads;
using AdsHub.Controls;
using Android.Gms.Ads.Reward;
using System.Threading;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AdsHub.Controls.AdRewardedVideoView), typeof(AdsHub.Droid.Renderers.AdRewardedVideoRenderer))]
namespace AdsHub.Droid.Renderers
{
    public class AdRewardedVideoRenderer : ViewRenderer<Controls.AdRewardedVideoView, Android.Views.View>
    {
        Android.Views.View view = null;

        private void SetUpViewAndAd()
        {
            if (view != null) return;

            view = new View(Xamarin.Forms.Forms.Context);

            var ad = MobileAds.GetRewardedVideoAdInstance(Xamarin.Forms.Forms.Context);
            ad.RewardedVideoAdListener = new RewardedVideoAdListener { Ad = ad };

            ad.LoadAd("ca-app-pub-3940256099942544/5224354917", new AdRequest.Builder().Build());
            ThreadPool.QueueUserWorkItem(s =>
            {
                // Wait the ad to be loading.
                Thread.Sleep(20000);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (ad.IsLoaded)
                        ad.Show();
                });
            });
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdRewardedVideoView> e)
        {
            base.OnElementChanged(e);
            if(view == null)
            {
                SetUpViewAndAd();
                SetNativeControl(view);
            }
        }
    }

    class RewardedVideoAdListener : IRewardedVideoAdListener
    {
        public IRewardedVideoAd Ad { get; set; }

        public IntPtr Handle => new IntPtr();

        public void Dispose()
        {
            
        }

        public void OnRewarded(IRewardItem reward)
        {
            int a = 1;
        }

        public void OnRewardedVideoAdClosed()
        {
            int a = 1;
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            int a = 1;
        }

        public void OnRewardedVideoAdLeftApplication()
        {
            int a = 1;
        }

        public void OnRewardedVideoAdLoaded()
        {
            if (Ad.IsLoaded)
                Ad.Show();
        }

        public void OnRewardedVideoAdOpened()
        {
            int a = 1;
        }

        public void OnRewardedVideoStarted()
        {
            int a = 1;
        }
    }
}