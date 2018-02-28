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
        public SelectedEvent s;
        public EditObject obj;
        public string type, token, dept;
        public int r_Count, m_Count, i, j;

        public Edit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            s = (SelectedEvent)e.Parameter;
            obj = s.obj;
            token = s.token;
            
            if (obj.eventType == "Technical")
            {
                Type.SelectedIndex = 0;
                Department.IsEnabled = true;
                if (obj.department.ToString() == "Computer/IT")
                    Department.SelectedIndex = 0;
                else if (obj.department.ToString() == "Civil")
                    Department.SelectedIndex = 1;
                else if (obj.department.ToString() == "Electrical")
                    Department.SelectedIndex = 2;
                else if (obj.department.ToString() == "Mechanical/Production")
                    Department.SelectedIndex = 3;
                else
                    Department.SelectedIndex = 4;
            }
            else if (obj.eventType == "Non-Technical")
            {
                Type.SelectedIndex = 1;
                Department.IsEnabled = false;
            }
            else
            {
                Type.SelectedIndex = 2;
                Department.IsEnabled = false;
            }
            TextBox1.Text = obj.eventName.ToString();
            TextBox2.Text = obj.tagline.ToString();
            TextBox3.Text = obj.description.ToString();
            TextBox4.Text = obj.teamSize.ToString();
            TextBox5.Text = obj.entryFee.ToString();
            List<int> prizeList = obj.prizeMoney.ToList();
            Winner.Text = prizeList[0].ToString();
            Runner_Up.Text = prizeList[1].ToString();

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
                if (m_Count > 5) throw new Exception();
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
                Warning1.Text = "Can not add more than 5 managers";
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
                if (r_Count > 5) throw new Exception();
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
                Warning2.Text = "Can not add more than 5 rounds";
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
                Invalid.Visibility = Visibility.Collapsed;
                EditObject d = new EditObject();
                d.eventName = TextBox1.Text.ToString();
                d.eventType = type;
                d._id = obj._id.ToString();

                if (Department.IsEnabled == true)
                    d.department = dept;
                else
                    d.department = " ";

                d.tagline = TextBox2.Text.ToString();
                d.description = TextBox3.Text.ToString();
                d.teamSize = Convert.ToInt32(TextBox4.Text);
                d.entryFee = Convert.ToInt32(TextBox5.Text);

                ArrayList prize = new ArrayList();
                prize.Add(Winner.Text.ToString());
                prize.Add(Runner_Up.Text.ToString());

                d.prizeMoney = new List<int>();
                int p_Count = 0;
                while (p_Count < prize.Count)
                {
                    d.prizeMoney.Add(Convert.ToInt32(prize[p_Count]));
                    p_Count++;
                }

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

                names.Reverse();
                phone.Reverse();

                string name1 = (names[names.Count - 1]).ToString();
                names.RemoveAt(names.Count - 1);
                names.Insert(0, name1);

                string phone1 = (phone[phone.Count - 1]).ToString();
                phone.RemoveAt(phone.Count - 1);
                phone.Insert(0, phone1);

                while (r_Count - 1 > 1)
                {
                    r_Count--;
                    TextBox r = (TextBox)this.Rounds.FindName("Round" + (r_Count));

                    round.Add(r.Text.ToString());
                }

                string round1 = (round[round.Count - 1]).ToString();
                round.RemoveAt(round.Count - 1);
                round.Insert(0, round1);

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

                string uri = "http://udaan18-events-api.herokuapp.com/events";
                ResponseObject response = await Submit.PutAsJsonAsync(uri, d, token);

                if (response.status == true)
                    this.Frame.Navigate(typeof(Finish), token);
                else
                {
                    Invalid.Text = "Submission unsuccessfull. (Are you missing any field..?)";
                    Invalid.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Invalid.Text = "Each field is complusory..!";
                Invalid.Visibility = Visibility.Visible;
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
}