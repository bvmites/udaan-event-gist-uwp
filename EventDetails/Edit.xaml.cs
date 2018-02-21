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
    public sealed partial class Edit : Page
    {
        public EditObject obj;
        public string type, token, dept;
        public int r_Count, m_Count, i, j;

        public Edit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            obj = (EditObject)e.Parameter;
            Type.SelectedValuePath = obj.type.ToString();
            if(obj.type == "Technical")
            {
                Department.Visibility = Visibility.Visible;
                Department.SelectedValuePath = obj.department.ToString();
            }
            TextBox1.Text = obj.name.ToString();
            TextBox2.Text = obj.tagline.ToString();
            TextBox3.Text = obj.description.ToString();
            TextBox4.Text = obj.teamSize.ToString();
            TextBox5.Text = obj.fees.ToString();

            List<Manager> m1 = obj.managers.ToList();

            TextBox6.Text = m1[0].name.ToString();
            TextBox7.Text = m1[0].phone.ToString();
            TextBox8.Text = obj.rounds[0].ToString();
            m_Count = m1.Count() + 1;
            r_Count = obj.rounds.Count + 1;

            j = 1;
            i = 2;
            while(i < m_Count)
            {
                Warning1.Visibility = Visibility.Collapsed;

                TextBlock tb = new TextBlock();
                tb.Name = "Manager" + i;
                tb.Text = "Manager " + i;
                tb.Margin = new Thickness(0, 10, 0, 0);
                tb.HorizontalAlignment = HorizontalAlignment.Stretch;
                tb.VerticalAlignment = VerticalAlignment.Stretch;
                tb.Foreground = TextBox1.Foreground;
                Manager.Children.Add(tb);
                Rounds.UpdateLayout();

                TextBox t = new TextBox();
                t.Name = "ManagerName" + i;
                t.PlaceholderText = "Name";
                t.Text = m1[j].name.ToString();
                t.HorizontalAlignment = HorizontalAlignment.Stretch;
                t.VerticalAlignment = VerticalAlignment.Stretch;
                t.Margin = new Thickness(0, 10, 0, 0);
                t.Foreground = TextBox1.Foreground;
                Manager.Children.Add(t);
                Rounds.UpdateLayout();

                TextBox n = new TextBox();
                n.Name = "Number" + i;
                n.PlaceholderText = "Phone Number";
                n.Text = m1[j].phone.ToString();
                n.HorizontalAlignment = HorizontalAlignment.Stretch;
                n.VerticalAlignment = VerticalAlignment.Stretch;
                n.Margin = new Thickness(0, 10, 0, 0);
                n.Foreground = TextBox1.Foreground;
                Manager.Children.Add(n);
                Rounds.UpdateLayout();

                j++;
                i++;
            }

            j = 1;
            i = 2;
            while(i < r_Count)
            {
                Warning2.Visibility = Visibility.Collapsed;
                TextBox t = new TextBox();
                t.Name = "Round" + i;
                t.PlaceholderText = "Round " + i;
                t.Text = obj.rounds[j].ToString();
                t.HorizontalAlignment = HorizontalAlignment.Stretch;
                t.VerticalAlignment = VerticalAlignment.Stretch;
                t.Height = 100;
                t.Margin = new Thickness(0, 10, 0, 0);
                t.Foreground = TextBox1.Foreground;
                Rounds.Children.Add(t);
                Rounds.UpdateLayout();
                j++;
                i++;
            }
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
                    tb.HorizontalAlignment = HorizontalAlignment.Stretch;
                    tb.VerticalAlignment = VerticalAlignment.Stretch;
                    tb.Foreground = TextBox1.Foreground;
                    Manager.Children.Add(tb);
                    Rounds.UpdateLayout();

                    TextBox t = new TextBox();
                    t.Name = "ManagerName" + m_Count;
                    t.PlaceholderText = "Name";
                    t.HorizontalAlignment = HorizontalAlignment.Stretch;
                    t.VerticalAlignment = VerticalAlignment.Stretch;
                    t.Margin = new Thickness(0, 10, 0, 0);
                    t.Foreground = TextBox1.Foreground;
                    Manager.Children.Add(t);
                    Rounds.UpdateLayout();

                    TextBox n = new TextBox();
                    n.Name = "Number" + m_Count;
                    n.PlaceholderText = "Phone Number";
                    n.HorizontalAlignment = HorizontalAlignment.Stretch;
                    n.VerticalAlignment = VerticalAlignment.Stretch;
                    n.Margin = new Thickness(0, 10, 0, 0);
                    n.Foreground = TextBox1.Foreground;
                    Manager.Children.Add(n);
                    Rounds.UpdateLayout();

                    m_Count++;
                }
            }
            catch (Exception)
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
                    t.HorizontalAlignment = HorizontalAlignment.Stretch;
                    t.VerticalAlignment = VerticalAlignment.Stretch;
                    t.Height = 100;
                    t.Margin = new Thickness(0, 10, 0, 0);
                    t.Foreground = TextBox1.Foreground;
                    Rounds.Children.Add(t);
                    Rounds.UpdateLayout();
                    r_Count++;
                }
            }
            catch (Exception)
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
            catch (Exception)
            {
                Warning2.Text = "Atleast one round is required";
                Warning2.Visibility = Visibility.Visible;
            }
        }        

        public async void submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditObject d = new EditObject();
                d.name = TextBox1.Text.ToString();
                d.type = type;
                d.id = obj.id.ToString();

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

                d.managers = new List<Manager>();
                int k = 0, l = 0;

                while (k < names.Count)
                {
                    Manager m1 = new Manager();
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
                this.Frame.Navigate(typeof(Finish));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void logoutbutton_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public void backbutton_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Choice));
        }
    }
}