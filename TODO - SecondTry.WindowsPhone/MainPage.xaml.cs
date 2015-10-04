using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.Xml.Serialization;
using System.Xml;
using Windows.ApplicationModel;
using Windows.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Forms;





// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TODO___SecondTry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public double _screenHeight { get; set; }

        public TextBlock _currentTask { get; set; }

        public static SolidColorBrush _phoneAccent { get; set; }

        public List<StackPanel> _tasks { get; set; }

        public List<TextBlock> _taskNames { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _screenHeight = Responsive.GetScreenHeight();
            _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            _tasks = new List<StackPanel>();
            _taskNames = new List<TextBlock>();

            HeaderStackPanel.Background = _phoneAccent;
            AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);

            //Sets the colors to the Phone Acce

            foreach (StackPanel panel in FindVisualChildren<StackPanel>(this))
            {
                panel.Background = _phoneAccent;
                //StackPanel chil = (StackPanel)panel.Children;
            }

            foreach (Button panel in FindVisualChildren<Button>(this))
            {
                panel.Background = _phoneAccent;
            }

            foreach (var panel in _tasks)
            {
                panel.Background = _phoneAccent;
            }

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigated to");
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            System.Diagnostics.Debug.WriteLine("Old phone accent : " + _phoneAccent.ToString());
            _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            System.Diagnostics.Debug.WriteLine("New phone accent : " + _phoneAccent.ToString());
            foreach (var panel in _tasks)
            {
                panel.Background = _phoneAccent;
                System.Diagnostics.Debug.WriteLine("In foreach tasks");

            }

            HeaderStackPanel.Background = _phoneAccent;
            AddButton.Background = _phoneAccent;
            _taskNames = new List<TextBlock>();

            LoadData();

            //foreach (Control panel in FindVisualChildren<Control>(this))
            //{
            //    panel.Background = _phoneAccent;
            //    //StackPanel chil = (StackPanel)panel.Children;
            //    System.Diagnostics.Debug.WriteLine("In foreach stackpanels");
            //}

            //foreach (Button panel in FindVisualChildren<Button>(this))
            //{
            //    panel.Background = _phoneAccent;

            //    System.Diagnostics.Debug.WriteLine("In foreach button");
            //}
        }

        private void MainButtonClick(object sender, RoutedEventArgs e)
        {

        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void NewTaskTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && sender.Equals(NewTaskTextBox)) //If Enter pressed
            {
                //Creates the textblock with the task
                TextBlock text = new TextBlock();
                text.Text = NewTaskTextBox.Text;
                text.FontSize = 20;
                text.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
                text.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                //Creates the stackpanel containing the textbox
                StackPanel panel = new StackPanel();
                panel.Background = _phoneAccent;
                panel.Height = _screenHeight / 6;

                panel.Orientation = Orientation.Vertical;
                panel.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);

                NewTaskTextBox.Text = "";
                NewTaskTextBox.Visibility = Visibility.Collapsed;

                text.Height = panel.Height / 2;



                panel.Children.Add(text);


                //Handles new events
                panel.Holding += DeleteTask;
                panel.Tapped += ModifyTask;


                //Adds the new task to the List of tasks
                if (_taskNames == null)
                    _taskNames = new List<TextBlock>();
                _taskNames.Add(text);

                //Adds the panel to the Big TaskPanel
                TaskPanel.Children.Add(panel);

                SaveTasks();

                //Changes the colors of the button +
                AddButton.Background = _phoneAccent;
                AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }//NewTaskTextBox_KeyDown()


        private void SaveTasks()
        {
            //if(_taskNames != null)
            //    ApplicationData.Current.LocalSettings.Values["_taskNames"] = _taskNames.ToArray<TextBlock>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<TextBlock>));
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true,
            };


            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                serializer.Serialize(xmlWriter, _taskNames);
            } 
        }

        private void LoadData()
        {
            if ((List<TextBlock>)ApplicationData.Current.LocalSettings.Values["_taskNames"] != null)
                _taskNames = ((List<TextBlock>)ApplicationData.Current.LocalSettings.Values["_taskNames"]).ToList();

            if (_taskNames != null)
            {
                foreach (var taskname in _taskNames)
                {
                    //Creates the textblock with the task
                    TextBlock text = new TextBlock();
                    text.Text = (taskname as TextBlock).Text;
                    text.FontSize = 20;
                    text.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
                    text.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                    //Creates the stackpanel containing the textbox
                    StackPanel panel = new StackPanel();
                    panel.Background = _phoneAccent;
                    panel.Height = _screenHeight / 6;

                    panel.Orientation = Orientation.Vertical;
                    panel.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);

                    NewTaskTextBox.Text = "";
                    NewTaskTextBox.Visibility = Visibility.Collapsed;

                    text.Height = panel.Height / 2;



                    panel.Children.Add(text);


                    //Handles new events
                    panel.Holding += DeleteTask;
                    panel.Tapped += ModifyTask;

                    TaskPanel.Children.Add(panel);
                }
            }
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            ((StackPanel)sender).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            _currentTask = ((StackPanel)sender).Children.OfType<TextBlock>().FirstOrDefault();

            ChangeNameTextBox.Visibility = Visibility.Visible;
            ChangeNameTextBox.Focus(FocusState.Keyboard);
            ChangeNameTextBox.LostFocus += DestroyChangeNameTextBox;
            ChangeNameTextBox.KeyDown += ChangeNameTextBox_KeyDown;

            //changeNameTextBox.LostFocus += DestroyChangeNameTextBox;
        }

        void ChangeNameTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ((e.Key == Windows.System.VirtualKey.Enter) && (!ChangeNameTextBox.Text.Equals("")))
            {
                System.Diagnostics.Debug.WriteLine(ChangeNameTextBox.Text);
                _currentTask.Text = ChangeNameTextBox.Text;
                ChangeNameTextBox.Text = "";
                ChangeNameTextBox.Visibility = Visibility.Collapsed;
            }

        }

        private void DestroyChangeNameTextBox(object sender, RoutedEventArgs e)
        {
            ChangeNameTextBox.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e) //Displays the textBox to create a new task
        {
            NewTaskTextBox.Visibility = Visibility.Visible;
            NewTaskTextBox.Focus(FocusState.Keyboard);
            AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.Foreground = _phoneAccent;
        }

        private void AddButton_Tapped(object sender, TappedRoutedEventArgs e) //Creates a hover effect
        {
            //ChangeAddButtonColors(false);
        }

        private void NewTaskTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NewTaskTextBox.Visibility = Visibility.Collapsed;
            AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.Background = _phoneAccent;
        }

        private void ChangeAddButtonColors(bool tapped) //Changes the color of the Add button
        {
            if (!tapped)
            {
                AddButton.Foreground = _phoneAccent;
                AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
            else
            {
                AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                AddButton.Background = _phoneAccent;
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }

        private void SettingsBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private void Page_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (_phoneAccent != new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color))
            {
                Frame.Navigate(typeof(MainPage));
            }
        }



        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("Loaded");
        //    // TODO: Prepare page for display here.

        //    // TODO: If your application contains multiple pages, ensure that you are
        //    // handling the hardware Back button by registering for the
        //    // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
        //    // If you are using the NavigationHelper provided by some templates,
        //    // this event is handled for you.

        //    System.Diagnostics.Debug.WriteLine("Old phone accent : " + _phoneAccent.ToString());
        //    _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
        //    System.Diagnostics.Debug.WriteLine("New phone accent : " + _phoneAccent.ToString());
        //    foreach (var panel in _tasks)
        //    {
        //        panel.Background = _phoneAccent;
        //        System.Diagnostics.Debug.WriteLine("In foreach tasks");

        //    }

        //    foreach (Control panel in FindVisualChildren<Control>(this))
        //    {
        //        panel.Background = _phoneAccent;
        //        //StackPanel chil = (StackPanel)panel.Children;
        //        System.Diagnostics.Debug.WriteLine("In foreach stackpanels");
        //    }

        //    foreach (Button panel in FindVisualChildren<Button>(this))
        //    {
        //        panel.Background = _phoneAccent;

        //        System.Diagnostics.Debug.WriteLine("In foreach button");
        //    }

        //}

        //private void Page_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        //{
        //    System.Diagnostics.Debug.WriteLine("dataContextChanged");
        //    // TODO: Prepare page for display here.

        //    // TODO: If your application contains multiple pages, ensure that you are
        //    // handling the hardware Back button by registering for the
        //    // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
        //    // If you are using the NavigationHelper provided by some templates,
        //    // this event is handled for you.

        //    System.Diagnostics.Debug.WriteLine("Old phone accent : " + _phoneAccent.ToString());
        //    _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
        //    System.Diagnostics.Debug.WriteLine("New phone accent : " + _phoneAccent.ToString());
        //    foreach (var panel in _tasks)
        //    {
        //        panel.Background = _phoneAccent;
        //        System.Diagnostics.Debug.WriteLine("In foreach tasks");

        //    }

        //    foreach (Control panel in FindVisualChildren<Control>(this))
        //    {
        //        panel.Background = _phoneAccent;
        //        //StackPanel chil = (StackPanel)panel.Children;
        //        System.Diagnostics.Debug.WriteLine("In foreach stackpanels");
        //    }

        //    foreach (Button panel in FindVisualChildren<Button>(this))
        //    {
        //        panel.Background = _phoneAccent;

        //        System.Diagnostics.Debug.WriteLine("In foreach button");
        //    }

        //}

        //private void Page_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("GotFocus");
        //    // TODO: Prepare page for display here.

        //    // TODO: If your application contains multiple pages, ensure that you are
        //    // handling the hardware Back button by registering for the
        //    // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
        //    // If you are using the NavigationHelper provided by some templates,
        //    // this event is handled for you.

        //    System.Diagnostics.Debug.WriteLine("Old phone accent : " + _phoneAccent.ToString());
        //    _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
        //    System.Diagnostics.Debug.WriteLine("New phone accent : " + _phoneAccent.ToString());
        //    foreach (var panel in _tasks)
        //    {
        //        panel.Background = _phoneAccent;
        //        System.Diagnostics.Debug.WriteLine("In foreach tasks");

        //    }

        //    foreach (Control panel in FindVisualChildren<Control>(this))
        //    {
        //        panel.Background = _phoneAccent;
        //        //StackPanel chil = (StackPanel)panel.Children;
        //        System.Diagnostics.Debug.WriteLine("In foreach stackpanels");
        //    }

        //    foreach (Button panel in FindVisualChildren<Button>(this))
        //    {
        //        panel.Background = _phoneAccent;

        //        System.Diagnostics.Debug.WriteLine("In foreach button");
        //    }

        //}

        //private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("sizechanged");
        //    // TODO: Prepare page for display here.

        //    // TODO: If your application contains multiple pages, ensure that you are
        //    // handling the hardware Back button by registering for the
        //    // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
        //    // If you are using the NavigationHelper provided by some templates,
        //    // this event is handled for you.

        //    System.Diagnostics.Debug.WriteLine("Old phone accent : " + _phoneAccent.ToString());
        //    _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
        //    System.Diagnostics.Debug.WriteLine("New phone accent : " + _phoneAccent.ToString());
        //    foreach (var panel in _tasks)
        //    {
        //        panel.Background = _phoneAccent;
        //        System.Diagnostics.Debug.WriteLine("In foreach tasks");

        //    }

        //    foreach (Control panel in FindVisualChildren<Control>(this))
        //    {
        //        panel.Background = _phoneAccent;
        //        //StackPanel chil = (StackPanel)panel.Children;
        //        System.Diagnostics.Debug.WriteLine("In foreach stackpanels");
        //    }

        //    foreach (Button panel in FindVisualChildren<Button>(this))
        //    {
        //        panel.Background = _phoneAccent;

        //        System.Diagnostics.Debug.WriteLine("In foreach button");
        //    }

        //}


    }//class MainPage
}//ns
