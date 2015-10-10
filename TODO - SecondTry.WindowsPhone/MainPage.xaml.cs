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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TODO___SecondTry
{    
    public sealed partial class MainPage : Page
    {
        public static bool created { get; set; }

        public double _screenHeight { get; set; } //The screen height

        public TextBlock _currentTask { get; set; } // The current task TextBlock

        public static SolidColorBrush _phoneAccent { get; set; } //the phone accent color

        public string[] _taskArray { get; set; }// the list of the names of the tasks

        public int _currentItem { get; set; } //current item of the task array

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Échecs de stockage des noms

        public List<StackPanel> _tasks { get; set; }

        public List<TextBlock> _taskNames { get; set; }

        public List<string> _taskList { get; set; }

        public MainPage()
        {
            created = false;
            this.InitializeComponent();//Native code

            this.NavigationCacheMode = NavigationCacheMode.Required;// Native code

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //for (int i = 0; localSettings.Values["task" + i] != null; ++i)
            //    localSettings.Values["task" + i] = null;

            //Check if saves exist
            //var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (localSettings.Values["task0"] == null)
                {
                    System.Diagnostics.Debug.WriteLine("In Constructor - No data saved");
                    _taskArray = new string[0]; //creates the new task array that will contain the names of the tasks
                    _taskList = new List<string>();
                    _currentItem = 0;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("In constructor - Data saved");
                    //for (int i = 0; localSettings.Values["task" + i] != null; i++)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("In Constructor, Data saved :" + localSettings.Values["task" + i]);

                    //}

                   
                    //_taskArray = LoadTasks();
                    _taskList = LoadTasks().ToList<string>();
                    _currentItem = _taskList.Count; //NULL POINTER EXCEPTION
                    System.Diagnostics.Debug.WriteLine("In constructor : currentItem : " + _currentItem);
                    //CreateTasks();

                }

            //Initializes the members of MainPage
            _screenHeight = Responsive.GetScreenHeight();
            _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            _tasks = new List<StackPanel>();
            _taskNames = new List<TextBlock>();           
            
           
            
            HeaderStackPanel.Background = _phoneAccent;
            AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);

            //Sets the colors to the Phone Accent
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

        //Creates the tasks on the UI
        private void CreateTasks()
        {
            System.Diagnostics.Debug.WriteLine("In CreateTasks() : _taskList.Count = " + _taskList.Count);
            for (int i = 0; i < _taskList.Count; ++i)
            {
                if (i == _taskList.Count) break;
                System.Diagnostics.Debug.WriteLine("In CreateTasks : ITERATION " + i);
                System.Diagnostics.Debug.WriteLine("In CreateTasks , _taskList[" + i + "] = " + _taskList.ElementAt(i));//OUT OF RANGE EXCEPTION
                AddTaskPanel(_taskList.ElementAt(i));
            }
        }

        private void AddTaskPanel(string taskName)
        {


             System.Diagnostics.Debug.WriteLine("In AddTaskPanel , adding " + taskName);
                 
            //Creates the textblock with the task
            TextBlock text = new TextBlock();
            text.Text = taskName;
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
            //Creates a temporary array and stores it into _taskArray
            //string[] tempTaskArray = _taskArray;
            //Array.Resize(ref tempTaskArray, _taskArray.Length + 1); //NULLREFERENCE EXCEPTION
            //_taskArray = tempTaskArray;

            //Puts the task name into the array
           // _taskArray[_currentItem] = text.Text;
            //System.Diagnostics.Debug.WriteLine("In AddTaskPanel, _taskList before");
            //int j = 0;
            //foreach (string item in _taskList)
            //{
            //    System.Diagnostics.Debug.WriteLine("----> " + _taskList.ElementAt(j));
            //    ++j;
            //}

            //_taskList.Add(text.Text);

            //System.Diagnostics.Debug.WriteLine("In AddTaskPanel, _taskList after");
            // j = 0;
            //foreach (string item in _taskList)
            //{
            //    System.Diagnostics.Debug.WriteLine("----> " + _taskList.ElementAt(j));
            //    ++j;
            //}
           // _currentItem++;

            //SaveTasks();
            
        }//CreateTasks()

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

            //Refresh the phone accent color
            System.Diagnostics.Debug.WriteLine("Old phone accent : " + _phoneAccent.ToString());
            _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            System.Diagnostics.Debug.WriteLine("New phone accent : " + _phoneAccent.ToString());

            //Refresh the color of the tasks with the phone accent color
            foreach (var panel in _tasks)
            {
                panel.Background = _phoneAccent;
                System.Diagnostics.Debug.WriteLine("In foreach tasks");

            }

            HeaderStackPanel.Background = _phoneAccent;
            AddButton.Background = _phoneAccent;
            _taskNames = new List<TextBlock>();

            //Loads the taskss and dipslay them in the page
            //LoadTasks(); 

            if (!created)
            {
                CreateTasks();
                created = true;
            }
        }// MainPage()

       
        //Selects all the stackpanels
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

        //Creates the panel with the task
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
                //Creates a temporary array and stores it into _taskArray
                //string[] tempTaskArray = _taskArray;
                //Array.Resize(ref tempTaskArray, _taskArray.Length + 1);
                //_taskArray = tempTaskArray;
                
                //Puts the task name into the array
                //_taskArray[_currentItem] = text.Text;
                _taskList.Add(text.Text);
                SaveTasks();
                _currentItem++;         

                
            }
        }//NewTaskTextBox_KeyDown()


        //Save the tasks 
        private void SaveTasks()
        {
            //WITH LOCALSETTINGS

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            System.Diagnostics.Debug.WriteLine("In SaveTasks, Addin " + _taskList.ElementAt(_currentItem)+" to " + _currentItem);
            localSettings.Values["task" + _currentItem] = _taskList.ElementAt(_currentItem);
            //for (int i = 0; i < _taskList.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine("In first for of saveTasks");
            //     localSettings.Values["task" + i] = _taskArray[i];
            //}


            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            for (int i = 0; i < _taskArray.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine("SaveTask, showing _taskList values: " + localSettings.Values["task" + i]);
                 
            }
        }//SaveTasks()

        //Loads the names of the tasks
        private string[] LoadTasks()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string[] tempArray = new string[1];

            List<string> taskList = new List<string>();

            for (int i = 0; localSettings.Values["task" + i] != null; ++i )
            {
                taskList.Add((string)localSettings.Values["task" + i]);
                System.Diagnostics.Debug.WriteLine("In LoadTasks, task" + i +" = "+taskList.ElementAt(i) );
                
            }

            //for (int i = 0; i < tempArray.Length; i++)
            //{
            //    if (localSettings.Values["task" + i] == null)
            //    {
            //        System.Diagnostics.Debug.WriteLine("In Loadtasks null returned");
            //        return null;
            //    }
                    

            //    if (tempArray.Length == 1)
                    
            //    {
            //        tempArray[i] = (string)localSettings.Values["task" + i];
            //        System.Diagnostics.Debug.WriteLine("In LoadTasks , tempArray[1] = " + tempArray[i]);
            //        System.Diagnostics.Debug.WriteLine("In LoadTasks , tempArray.Length = " + tempArray.Length);
            //        Array.Resize(ref tempArray, tempArray.Length + 1);
            //        System.Diagnostics.Debug.WriteLine("In LoadTasks , tempArray.Length = " + tempArray.Length);
            //    }
            //    else
            //    {
                    
            //        tempArray[i] = (string)localSettings.Values["task" + i];
            //        System.Diagnostics.Debug.WriteLine("In LoadTasks , tempArray["+i+"] = " + tempArray[i]);
            //        System.Diagnostics.Debug.WriteLine("In LoadTasks , tempArray.Length = " + tempArray.Length);
            //        Array.Resize(ref tempArray, tempArray.Length + 1);
            //        System.Diagnostics.Debug.WriteLine("In LoadTasks , tempArray.Length = " + tempArray.Length);
            //    }
            //}

            return taskList.ToArray<string>();
        }//LoadTasks()

        //Deletes the selected task
        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            for (int i = 0; i < _taskList.Count; i++)
            {
                if(((StackPanel)sender).Children.OfType<TextBlock>().FirstOrDefault().Text.Equals((string)localSettings.Values["task"+i]))
                {
                    System.Diagnostics.Debug.WriteLine("In DeleteTask, Gonna remove : "  +_taskList.ElementAt(i) );
                    _taskList.RemoveAt(i);

                }
            }

           
            for (int i = 0; i < _taskList.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("In DeleteTask, addin " + _taskList.ElementAt(i));
                localSettings.Values["task" + i] = _taskList.ElementAt(i);
            }

            ((StackPanel)sender).Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }//DeleteTask()

        //Displays the textbox to change the name of teh task
        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            _currentTask = ((StackPanel)sender).Children.OfType<TextBlock>().FirstOrDefault();

            ChangeNameTextBox.Visibility = Visibility.Visible;
            ChangeNameTextBox.Focus(FocusState.Keyboard);
            ChangeNameTextBox.LostFocus += DestroyChangeNameTextBox;
            ChangeNameTextBox.KeyDown += ChangeNameTextBox_KeyDown;
        }//ModifyTask()

        //Really changes the name of the task once enter is pressed
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
        

        private void debug_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            for (int i = 0; localSettings.Values["task" + i] != null; ++i)
                localSettings.Values["task" + i] = null;
            localSettings.Values["nbitem"] = 0;
        }
    }//class MainPage
}//ns
