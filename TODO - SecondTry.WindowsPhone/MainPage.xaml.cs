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

        public string[] _taskArray { get; set; }

        public int _currentItem { get; set; }

        //Échecs de stockage des noms

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

            _currentItem = 0;
            _taskArray = new string[0];

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

            LoadTasks();

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
        }//FindVisualChildren()

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

              

                //Changes the colors of the button +
                AddButton.Background = _phoneAccent;
                AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White); 

                
      //---------------------PARTIE SAUVEGARDE--------------------------------------------          
                string[] tempTaskArray = _taskArray;
                Array.Resize(ref tempTaskArray, _taskArray.Length + 1);
                _taskArray = tempTaskArray;
                
                _taskArray[_currentItem] = text.Text;
                _currentItem++;
                
               

                SaveTasks();
            }
        }//NewTaskTextBox_KeyDown()


        private void SaveTasks()
        {
            //if(_taskNames != null)
            //    ApplicationData.Current.LocalSettings.Values["_taskNames"] = _taskNames.ToArray<TextBlock>();

            //WITH LOCALSETTINGS

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            for (int i = 0; i < _taskArray.Length; i++)
            {
                 localSettings.Values["task" + i] = _taskArray[i];
            }

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            for (int i = 0; i < _taskArray.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine("Dans saved task : " + localSettings.Values["task" + i]);
                 
            }
        }//SaveTasks()

        private string[] LoadTasks()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string[] tempArray = new string[1];

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (localSettings.Values["task" + i] == null)
                    return null;

                if (tempArray.Length == 1)
                    tempArray[i] = (string)localSettings.Values["task" + i];
                else
                {
                    Array.Resize(ref tempArray, tempArray.Length + 1);
                    tempArray[i] = (string)localSettings.Values["task" + i];
                }
            }

            return tempArray;
        }//LoadTasks()

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            ((StackPanel)sender).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }//DeleteTask()

        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            _currentTask = ((StackPanel)sender).Children.OfType<TextBlock>().FirstOrDefault();

            ChangeNameTextBox.Visibility = Visibility.Visible;
            ChangeNameTextBox.Focus(FocusState.Keyboard);
            ChangeNameTextBox.LostFocus += DestroyChangeNameTextBox;
            ChangeNameTextBox.KeyDown += ChangeNameTextBox_KeyDown;
        }//ModifyTask()

        void ChangeNameTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ((e.Key == Windows.System.VirtualKey.Enter) && (!ChangeNameTextBox.Text.Equals("")))
            {
                System.Diagnostics.Debug.WriteLine(ChangeNameTextBox.Text);
                _currentTask.Text = ChangeNameTextBox.Text;
                ChangeNameTextBox.Text = "";
                ChangeNameTextBox.Visibility = Visibility.Collapsed;
            }

        }//ChangeNameTextBox_KeyDown()

        private void DestroyChangeNameTextBox(object sender, RoutedEventArgs e)
        {
            ChangeNameTextBox.Visibility = Visibility.Collapsed;
        }//DestroyChangeNameTextBox()

        private void AddButton_Click_1(object sender, RoutedEventArgs e) //Displays the textBox to create a new task
        {
            NewTaskTextBox.Visibility = Visibility.Visible;
            NewTaskTextBox.Focus(FocusState.Keyboard);
            AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.Foreground = _phoneAccent;
        }//AddButton_Click_1()       

        private void NewTaskTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NewTaskTextBox.Visibility = Visibility.Collapsed;
            AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.Background = _phoneAccent;
        }//NewTaskTextBox_LostFocus()

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
        }//ChangeAddButtonColors()

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }//HelpButton_Click()

        private void SettingsBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }//SettingsBarButton()

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }//AboutButton_Click()
        
    }//class MainPage
}//ns
