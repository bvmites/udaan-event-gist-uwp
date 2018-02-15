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
            token = e.Parameter.ToString();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Register), token);
        }

        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<EditObject> obj = await EditDetails.GetDetails(token);
                this.Frame.Navigate(typeof(EventSelect), obj);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}