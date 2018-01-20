using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinFormsHelper.Behaviors;

namespace Lesson1.Behaviors
{
    class MapBehavior : BehaviorBase<Map>
    {
        public static readonly BindableProperty PinsProperty =
            BindableProperty.Create("Pins", typeof(IList<Pin>), typeof(MapBehavior), null, propertyChanged: OnPinsPropertyChanged);

        public static readonly BindableProperty VisibleRegionProperty =
            BindableProperty.Create("VisibleRegion", typeof(MapSpan), typeof(MapBehavior), propertyChanged: OnVisibleRegionPropertyChanged);

        public IList<Pin> Pins
        {
            get => (IList<Pin>)GetValue(PinsProperty);
            set => SetValue(PinsProperty, value);
        }

        public MapSpan VisibleRegion
        {
            get => (MapSpan)GetValue(VisibleRegionProperty);
            set => SetValue(VisibleRegionProperty, value);
        }

        private static void OnPinsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (MapBehavior)bindable;
            if (behavior.AssociatedObject == null) return;

            IList<Pin> oldList = oldValue as IList<Pin>;
            IList<Pin> newList = newValue as IList<Pin>;
            if (oldList is INotifyCollectionChanged)
                ((INotifyCollectionChanged)oldList).CollectionChanged -= behavior.OnExternalPinsCollectionChanged;
            if (newList is INotifyCollectionChanged)
                ((INotifyCollectionChanged)newList).CollectionChanged += behavior.OnExternalPinsCollectionChanged;
            behavior.OnExternalPinsCollectionChanged(behavior, null);
        }

        private void OnExternalPinsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            // Just to simplify the code we clear the Pins collection at the beginning and then restore it again.
            AssociatedObject.Pins.Clear();
            if (Pins == null) return;
            foreach(var pin in Pins)
            {
                AssociatedObject.Pins.Add(pin);
            }
        }

        private static void OnVisibleRegionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (MapBehavior)bindable;
            if (behavior.AssociatedObject == null || newValue as MapSpan == null) return;

            behavior.AssociatedObject.MoveToRegion((MapSpan)newValue);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            // Avoiding possible memory leak...
            Pins = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
