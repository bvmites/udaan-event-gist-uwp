using System;
using System.Collections;
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
    public sealed partial class Register : Page
    {
        public string token, type, dept;

        public Register()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            token = e.Parameter.ToString();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            type = item.Content.ToString();

            if (item.Content.ToString() == "Technical")
            {
                Department.IsEnabled = true;
            }
            else
                Department.IsEnabled = false;
        }

        public int r_Count = 2;

        public void AddRound_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (r_Count > 3) throw new Exception();
                {
                    Warning2.Visibility = Visibility.Collapsed;
                    TextBox t = new TextBox();
                    t.Name = "Round" + r_Count;
                    t.PlaceholderText = "Round " + r_Count;
                    t.Width = 350;
                    t.Height = 100;
                    t.Margin = new Thickness(0, 10, 0, 0);
                    Rounds.Children.Add(t);
                    Rounds.UpdateLayout();
                    r_Count++;
                }
            }
            catch(Exception)
            {
                Warning2.Text = "Can not add more than 3 rounds";
                Warning2.Visibility = Visibility.Visible;
            }
        }

        public void RemoveRound_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (r_Count < 3) throw new Exception();
                --r_Count;
                TextBox t = (TextBox)this.Rounds.FindName("Round" + (r_Count));
                this.Rounds.Children.Remove(t);
                Rounds.UpdateLayout();
                Warning2.Visibility = Visibility.Collapsed;
            }
            catch(Exception)
            {
                Warning2.Text = "Atleast one round is required";
                Warning2.Visibility = Visibility.Visible;
            }
        }

        public int m_Count = 2;

        public void Add_Manager(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m_Count > 3) throw new Exception();
                {
                    Warning1.Visibility = Visibility.Collapsed;

                    TextBlock tb = new TextBlock();
                    tb.Name = "Manager" + m_Count;
                    tb.Text = "Manager " + m_Count;
                    tb.Margin = new Thickness(0, 10, 0, 0);
                    Manager.Children.Add(tb);
                    Rounds.UpdateLayout();

                    TextBox t = new TextBox();
                    t.Name = "ManagerName" + m_Count;
                    t.PlaceholderText = "Name";
                    t.Width = 350;
                    t.Margin = new Thickness(0, 10, 0, 0);
                    Manager.Children.Add(t);
                    Rounds.UpdateLayout();

                    TextBox n = new TextBox();
                    n.Name = "Number" + m_Count;
                    n.PlaceholderText = "Phone Number";
                    n.Width = 350;
                    n.Margin = new Thickness(0, 10, 0, 0);
                    Manager.Children.Add(n);
                    Rounds.UpdateLayout();

                    m_Count++;
                }
            }
            catch(Exception)
            {
                Warning1.Text = "Can not add more than 3 managers";
                Warning1.Visibility = Visibility.Visible;
            }
        }

        public void Remove_Manager(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m_Count < 3) throw new Exception();
                --m_Count;
                TextBlock tb = (TextBlock)this.Manager.FindName("Manager" + (m_Count));

                this.Manager.Children.Remove(tb);
                Manager.UpdateLayout();

                TextBox t = (TextBox)this.Manager.FindName("ManagerName" + (m_Count));
                this.Manager.Children.Remove(t);
                Manager.UpdateLayout();

                TextBox n = (TextBox)this.Manager.FindName("Number" + (m_Count));
                this.Manager.Children.Remove(n);
                Manager.UpdateLayout();

                Warning1.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                Warning1.Text = "Atleast one manager is required";
                Warning1.Visibility = Visibility.Visible;
            }
        }

        public void Department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            dept = item.Content.ToString();
        }

        public async void submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Details d = new Details();
                d.name = TextBox1.Text.ToString();
                d.type = type;

                if (Department.IsEnabled == true)
                    d.department = dept;
                else
                    d.department = " ";

                d.tagline = TextBox2.Text.ToString();
                d.description = TextBox3.Text.ToString();
                d.teamSize = Convert.ToInt32(TextBox4.Text);
                d.fees = Convert.ToInt32(TextBox5.Text);
                
                ArrayList names = new ArrayList();
                ArrayList phone = new ArrayList();
                ArrayList round = new ArrayList();

                names.Add(TextBox6.Text.ToString());
                phone.Add(TextBox7.Text.ToString());
                round.Add(TextBox8.Text.ToString());
                
                while (m_Count - 1 > 1)
                {
                    m_Count--;
                    TextBox n = (TextBox)this.Manager.FindName("ManagerName" + (m_Count));
                    TextBox p = (TextBox)this.Manager.FindName("Number" + (m_Count));

                    names.Add(n.Text.ToString());
                    phone.Add(p.Text.ToString());
                }

                while (r_Count - 1 > 1)
                {
                    r_Count--;
                    TextBox r = (TextBox)this.Rounds.FindName("Round" + (r_Count));
                
                    round.Add(r.Text.ToString());                    
                }
                                
                d.managers = new List<Managers>();
                int k = 0, l = 0;

                while (k < names.Count)
                {
                    Managers m1 = new Managers();
                    m1.name = names[k].ToString();
                    m1.phone = phone[k].ToString();
                    d.managers.Add(m1);
                    k++;
                }

                d.rounds = new List<string>();
                while (l < round.Count)
                {
                    d.rounds.Add(round[l].ToString());
                    l++;
                }

                string uri = "http://editor.swagger.io/events";
                ResponseObject response = await Submit.PostAsJsonAsync(uri, d);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}