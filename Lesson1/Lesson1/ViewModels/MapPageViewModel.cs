using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using GeoAbstractions = Plugin.Geolocator.Abstractions;

namespace Lesson1.ViewModels
{
    class MapPageViewModel : PageViewModel
    {
        #region Constants

        public const double MinimumDistance = 1d;

        #endregion

        #region Fields

        private MapType mapType = MapType.Street;
        private bool mapTypeToggleValue = false; // These 2 values will form a pair.

        private double distanceToIsSoft = Double.NaN;
        private ObservableCollection<Pin> pins = new ObservableCollection<Pin>();
        private MapSpan mapCenterSpan;

        // TODO(Pavel Ostreyko): Ideally it could be better to avoid this hard reference...
        // Maybe in future it will be good idea to move it to the service part. Not sure...
        private GeoAbstractions.IGeolocator locator = CrossGeolocator.Current;

        #endregion

        #region Properties

        public MapType MapType
        {
            get => mapType;
            set
            {
                mapType = value;
                OnPropertyChanged();
            }
        }

        public bool MapTypeToggleValue
        {
            get => mapTypeToggleValue;
            set
            {
                mapTypeToggleValue = value;
                OnMapTypeToggleChanged();
                OnPropertyChanged();
            }
        }

        public double DistanceToIsSoft
        {
            get => distanceToIsSoft;
            set
            {
                distanceToIsSoft = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pin> Pins
        {
            get => pins;
            set
            {
                pins = value;
                OnPropertyChanged();
            }
        }

        public MapSpan MapCenterSpan
        {
            get => mapCenterSpan;
            set
            {
                mapCenterSpan = value;
                OnPropertyChanged();
            }
        }

        public ICommand AppearingCommand { get; set; }

        #endregion

        #region Methods

        private void OnMapTypeToggleChanged()
        {
            MapType = MapTypeToggleValue == false ? MapType.Street : MapType.Satellite;
        }

        public async void StartGeolocator()
        {
            // As we use geolocator only here we don't have to worry about extra listening starts.
            if (!locator.IsListening && await locator.StartListeningAsync(TimeSpan.FromSeconds(5), MinimumDistance))
                locator.PositionChanged += LocatorPositionChanged;
        }

        #endregion

        #region Event handlers

        private void LocatorPositionChanged(object sender, GeoAbstractions.PositionEventArgs e)
        {
            // TODO(Pavel Ostreyko): Also looks as a not needed reference...
            Device.BeginInvokeOnMainThread(() =>
            {
                var currentPosition = new Position(e.Position.Latitude, e.Position.Longitude);
                MapCenterSpan = MapSpan.FromCenterAndRadius(currentPosition, Distance.FromKilometers(MinimumDistance * 5));
                DistanceToIsSoft = Helpers.CoordsDistanceHelper.CalculateDistance(Pins[0].Position, currentPosition);
            });
        }

        #endregion

        #region Life Cycle

        public MapPageViewModel()
        {
            Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(53.904172, 27.590062),
                Label = "ISSoft",
                Address = "Chapaeva st., 5"
            });
            // Ideally we will need to create an Async command here (with async void Execute definition).
            AppearingCommand = new Command(StartGeolocator);
        }

        #endregion
    }
}
