using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
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
    public sealed partial class MainPage : Page
    {
        public string token;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User();

                user.Username = TextBox1.Text;
                user.Password = PasswordBox1.Password;

                string uri = "http://demo0530082.mockable.io/login";
                RootObject response = await Verify.PostAsJsonAsync(uri, user);

                if(response.status != false)
                {
                    token = response.token;
                    this.Frame.Navigate(typeof(Register), token);
                }
                else
                {
                    Invalid.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}