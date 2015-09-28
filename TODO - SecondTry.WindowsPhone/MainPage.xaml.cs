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

        private void AddButton_Click(object sender, RoutedEventArgs e)
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

            TextBlock text = new TextBlock();
            text.Text = "TAsk to do mothafocka de la muerte";
            text.FontSize = 20;
            text.Margin = new Thickness(10.0,0.0,0.0,0.0);
            text.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

            StackPanel panel = new StackPanel();
            panel.Background = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            panel.Height = Responsive.GetScreenHeight() / 8;
            panel.Children.Add(text);
            panel.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
            panel.Tapped += Task_Clicked;

            TaskPanel.Children.Add(panel);
        }

       private void Task_Clicked(object sender, RoutedEventArgs e)
       {
           System.Diagnostics.Debug.WriteLine("Panel cliked");
       }

       private void AboutButton_Click(object sender, RoutedEventArgs e)
       {
           Frame.Navigate(typeof(About));
       }
    }
}
