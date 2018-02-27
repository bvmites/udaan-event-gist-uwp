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
    public sealed partial class EventSelect : Page
    {
        public Data d;
        public List<EditObject> obj;
        public string token;

        public EventSelect()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            d = (Data)e.Parameter;
            obj = d.obj;
            token = d.token.ToString();

            int count = obj.Count;

            for(int i = 0; i<count; i++)
            {
                EventList.Items.Add(obj[i].eventName.ToString());
            }
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if(EventList.SelectedIndex >= 0)
            { 
                int index = EventList.SelectedIndex;
                EditObject obj1 = obj[index];
                SelectedEvent s = new SelectedEvent();
                s.obj = obj1;
                s.token = token;
                this.Frame.Navigate(typeof(Edit), s);
            }   
        }

        public void logoutbutton_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public void backbutton_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Choice), token);
        }
    }

    public class SelectedEvent
    {
        public EditObject obj { get; set; }
        public string token { get; set; }
    }
}