using HomeHub.Models;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input.Preview;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace HomeHub
{
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Person> PersonsList { get; set; }
        ObservableCollection<Room> RoomsList { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            RoomsList = new ObservableCollection<Room>();
            RoomsList.Add(new Room() { Name = "Bathroom", Image = "ms-appx:///Assets/Images/Bathroom.jpg" });
            RoomsList.Add(new Room() { Name = "Entrance", Image = "ms-appx:///Assets/Images/FrontDoor.jpg" });
            RoomsList.Add(new Room() { Name = "Living room", Image = "ms-appx:///Assets/Images/LivingRoom.jpg" });
            RoomsList.Add(new Room() { Name = "Terrace", Image = "ms-appx:///Assets/Images/Terrace.jpg" });
            RoomsList.Add(new Room() { Name = "Garden", Image = "ms-appx:///Assets/Images/Garden.jpg" });

            PersonsList = new ObservableCollection<Person>();
            PersonsList.Add(new Person() { Name = "Dianna", Image = "ms-appx:///Assets/Images/Dianna.jpg" });
            PersonsList.Add(new Person() { Name = "Loki", Image = "ms-appx:///Assets/Images/Loki.jpeg" });
            PersonsList.Add(new Person() { Name = "Ana", Image = "ms-appx:///Assets/Images/Ana.jpg" });
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GazeInput.SetIsCursorVisible(this, false);
            GazeInput.SetCursorRadius(this, 40);
            GazeInput.DwellStrokeThickness = 6;
            GazeInput.DwellFeedbackEnterBrush = new SolidColorBrush(Colors.DarkGray);
            GazeInput.DwellFeedbackProgressBrush = new SolidColorBrush(Colors.LightGray);
            GazeInput.DwellFeedbackCompleteBrush = new SolidColorBrush(Colors.White);
        }

        private void RoomsView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var container = RoomsView.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if (container != null)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("Image");

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }

            var item = (Room)e.ClickedItem;

            // Add a fade out effect
            Transitions = new TransitionCollection();
            Transitions.Add(new ContentThemeTransition());

            Frame.Navigate(typeof(DetailsView), item);
        }
    }
}