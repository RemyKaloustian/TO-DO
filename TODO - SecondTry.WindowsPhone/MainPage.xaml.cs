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

        public StackPanel Tasks { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _screenHeight =Responsive.GetScreenHeight() ;
            _phoneAccent = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);


            AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            AddButton.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);

            foreach (StackPanel panel in FindVisualChildren<StackPanel>(this))
            {
                panel.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
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

        private void Task_Clicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Panel cliked");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
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
                panel.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
                panel.Height = _screenHeight / 6;

                panel.Orientation = Orientation.Vertical;
                panel.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
                panel.Tapped += Task_Clicked;
                NewTaskTextBox.Text = "";
                NewTaskTextBox.Visibility = Visibility.Collapsed;

                text.Height = panel.Height / 2; 
                
                ////Creates the textbox to change the task name
                //TextBox changeNameTextBox = new TextBox();
                //changeNameTextBox.Visibility = Visibility.Collapsed;

                panel.Children.Add(text);
                //panel.Children.Add(changeNameTextBox);

                //Handles new events
                panel.Holding += DeleteTask;
                panel.Tapped += ModifyTask;



             


                //Creates two stackpanels
                StackPanel modifyButtonPanel = new StackPanel();
                modifyButtonPanel.Width = Responsive.GetScreenWidth() / 2;
                modifyButtonPanel.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
                modifyButtonPanel.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

                StackPanel deleteButtonPanel = new StackPanel();
                deleteButtonPanel.Width = Responsive.GetScreenWidth() / 2;
                deleteButtonPanel.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
                deleteButtonPanel.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

                //Creates a main stackpanel
                StackPanel mainPanel = new StackPanel();
                mainPanel.Orientation = Orientation.Horizontal;

                //Add modifyButton button
                Button modifyButton = new Button();
                modifyButton.Content = "modifyButton";
                modifyButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                modifyButton.Width = Responsive.GetScreenWidth() / 2;
                modifyButton.BorderBrush = null;

                //Add deleteButton button
                Button deleteButton = new Button();
                deleteButton.Content = "deleteButton";
                deleteButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                deleteButton.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                deleteButton.Width = Responsive.GetScreenWidth() / 2;
                deleteButton.BorderBrush = (null);

                //Adds the buttons to the panels, and thes panels to the mainPanel              
                modifyButtonPanel.Children.Add(modifyButton);
                deleteButtonPanel.Children.Add(deleteButton);
                mainPanel.Children.Add(modifyButtonPanel);
                mainPanel.Children.Add(deleteButtonPanel);

                //Adds everything to the panel of the task
                //panel.Children.Add(mainPanel);

                //Adds the panel to the Big TaskPanel
                TaskPanel.Children.Add(panel);

                //Changes the colors of the button +
                ChangeAddButtonColors(true);
            }
        }//NewTaskTextBox_KeyDown()

      

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            ((StackPanel)sender).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            //TextBox changeNameTextBox = new TextBox();
            //changeNameTextBox.Visibility = Visibility;
            //((StackPanel)sender).Children.Add(changeNameTextBox);
            //changeNameTextBox.Focus(FocusState.Keyboard);
            System.Diagnostics.Debug.WriteLine("Sender = " + sender.ToString());

            _currentTask = ((StackPanel)sender).Children.OfType<TextBlock>().FirstOrDefault();

            ChangeNameTextBox.Visibility = Visibility.Visible;
            ChangeNameTextBox.Focus(FocusState.Keyboard);
            ChangeNameTextBox.LostFocus += DestroyChangeNameTextBox;
            ChangeNameTextBox.KeyDown += ChangeNameTextBox_KeyDown;

            //changeNameTextBox.LostFocus += DestroyChangeNameTextBox;
        }

        void ChangeNameTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if((e.Key == Windows.System.VirtualKey.Enter) && (!ChangeNameTextBox.Text.Equals("")))
            {
                System.Diagnostics.Debug.WriteLine(ChangeNameTextBox.Text);
                _currentTask.Text = ChangeNameTextBox.Text;
                ChangeNameTextBox.Text = "";
                ChangeNameTextBox.Visibility = Visibility.Collapsed;
            }
            
        }

      


        private void   DestroyChangeNameTextBox(object sender, RoutedEventArgs e)
        {
            ChangeNameTextBox.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e) //Displays the textBox to create a new task
        {
            NewTaskTextBox.Visibility = Visibility.Visible;
            NewTaskTextBox.Focus(FocusState.Keyboard);
        }

        private void AddButton_Tapped(object sender, TappedRoutedEventArgs e) //Creates a hover effect
        {
            ChangeAddButtonColors(false);
        }

        private void NewTaskTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NewTaskTextBox.Visibility = Visibility.Collapsed;
            ChangeAddButtonColors(true);
        }

        private void ChangeAddButtonColors(bool tapped) //Changes the color of the Add button
        {
            if (!tapped)
            {
                AddButton.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
                AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
            else
            {
                AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                AddButton.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            }
        }

        #region TestedButUnusedFunctions


        private void AddButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //AddButton.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            //AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }

        private void AddButton_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            //AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            //AddButton.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
        }

        private void AddButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //AddButton.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            //AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }

        private void AddButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //AddButton.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            //AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }

        //Marchent mais pas comme il faut
        private void AddButton_GotFocus(object sender, RoutedEventArgs e)
        {
            //AddButton.Foreground = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            //AddButton.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }

        private void AddButton_LostFocus(object sender, RoutedEventArgs e)
        {
            //AddButton.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            //AddButton.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
        }

        private void AddButton_Drop(object sender, DragEventArgs e)
        {

        }

        #endregion

    }//class MainPage
}//ns
