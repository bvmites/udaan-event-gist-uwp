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
                RootObject response = new RootObject();

                if (TextBox1.Text != "" && PasswordBox1.Password != "")
                {
                    user.username = TextBox1.Text;
                    user.password = PasswordBox1.Password;
                    string uri = "http://udaan18-events-api.herokuapp.com/users/login";
                    response = await Verify.PostAsJsonAsync(uri, user);
                    if (response.token != "Invalid")
                    {
                        token = response.token;
                        this.Frame.Navigate(typeof(Choice), token);
                    }
                    else
                    {
                        Invalid.Visibility = Visibility.Visible;
                    }
                }
                else
                    Invalid.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}