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
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

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
           if (e.Key == Windows.System.VirtualKey.Enter) //If Enter pressed
           {
               //Create the textblock with the task
               TextBlock text = new TextBlock();
               text.Text = NewTaskTextBox.Text;
               text.FontSize = 20;
               text.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
               text.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

               //Creates the stackpanel containing the textbox
               StackPanel panel = new StackPanel();
               panel.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
               panel.Height = Responsive.GetScreenHeight() / 6;
               panel.Children.Add(text);
               panel.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
               panel.Tapped += Task_Clicked;
               NewTaskTextBox.Text = "";
               NewTaskTextBox.Visibility = Visibility.Collapsed;

               //Adding the textbox to the stackpanel
               TaskPanel.Children.Add(panel);


               //Changing the colors of the button +
               ChangeAddButtonColors(true);
           }
       }//NewTaskTextBox_KeyDown()

       private void AddButton_Click_1(object sender, RoutedEventArgs e)
       {
           //AVEC DES BOUTONS
           //Button but = new Button();
           //but.Content = "Task";
           ////but.Background = (SolidColorBrush)Application.Current.Resources["PhoneAccentColor"];
           //but.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
           //but.Width = 100;
           //but.BorderBrush = null;

           ////but.Foreground = Brushes.Blue;

           //TaskPanel.Children.Add(but);

           //AVEC DES PANELS



           NewTaskTextBox.Visibility = Visibility.Visible;
           NewTaskTextBox.Focus(FocusState.Keyboard);
       }

      

       private void AddButton_Tapped(object sender, TappedRoutedEventArgs e)
       {
           ChangeAddButtonColors(false);
       }

       private void NewTaskTextBox_LostFocus(object sender, RoutedEventArgs e)
       {
           NewTaskTextBox.Visibility = Visibility.Collapsed;
           ChangeAddButtonColors(true);
       }

       private void ChangeAddButtonColors(bool tapped)
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
