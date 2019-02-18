using HomeHub.Models;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction.GazeHidParsers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input.Preview;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace HomeHub
{
    public sealed partial class DetailsView : Page
    {
        SystemNavigationManager _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
        private GazeInputSourcePreview gazeInputSourcePreview;
        private GazeHidPositionsParser gazeHidPositionsParser;

        ObservableCollection<Room> Devices { get; set; }
        public DetailsView()
        {
            this.InitializeComponent();

            Devices = new ObservableCollection<Room>();
            Devices.Add(new Room() { Name = "Light 1 " });
            Devices.Add(new Room() { Name = "Light 2 " });
            Devices.Add(new Room() { Name = "Light 3 " });
            Devices.Add(new Room() { Name = "Light 4 " });
            Devices.Add(new Room() { Name = "Light 5 " });
            Devices.Add(new Room() { Name = "Light 6 " });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            gazeInputSourcePreview = GazeInputSourcePreview.GetForCurrentView();
            gazeInputSourcePreview.GazeMoved += GazeInputSourcePreview_GazeMoved;
        }

        private void GazeInputSourcePreview_GazeMoved(GazeInputSourcePreview sender, GazeMovedPreviewEventArgs args)
        {
            if (args.CurrentPoint.HidInputReport != null)
            {
                var hidReport = args.CurrentPoint.HidInputReport;
                var sourceDevice = args.CurrentPoint.SourceDevice;

                if (gazeHidPositionsParser == null)
                {
                    gazeHidPositionsParser = new GazeHidPositionsParser(sourceDevice);
                }

                GazeHidPositions positions = gazeHidPositionsParser.GetGazeHidPositions(hidReport);

                if (positions.RightEyePosition != null)
                {

                    double DistanceInMM = positions.RightEyePosition.Z / 10000;

                    DistanceText.Text = "Head distance: " + DistanceInMM.ToString() + " mm";
                    if (DistanceInMM >= 50)
                    {
                        DevicesList.ItemTemplate = (DataTemplate)this.Resources["LargeDeviceTemplate"];
                    }
                    else
                    {
                        DevicesList.ItemTemplate = (DataTemplate)this.Resources["SmallDeviceTemplate"];
                    } 
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GazeInput.SetIsCursorVisible(this, false);
        }
    }
}