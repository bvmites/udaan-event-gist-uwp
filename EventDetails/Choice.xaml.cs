using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EventDetails
{
    public sealed partial class Choice : Page
    {
        public string token;

        public Choice()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            No_Events.Visibility = Visibility.Collapsed;
            token = e.Parameter.ToString();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            No_Events.Visibility = Visibility.Collapsed;
            this.Frame.Navigate(typeof(Register), token);
        }

        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                No_Events.Visibility = Visibility.Collapsed;
                List<EditObject> obj = await EditDetails.GetDetails(token);
                Data d = new Data();
                d.obj = obj;
                d.token = token;

                if (obj.Count > 0)
                    this.Frame.Navigate(typeof(EventSelect), d);
                else
                    No_Events.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                No_Events.Visibility = Visibility.Visible;
            }
        }

        public void logoutbutton_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }

    public class Data
    {
        public List<EditObject> obj { get; set; }
        public string token { get; set; }
    }
}