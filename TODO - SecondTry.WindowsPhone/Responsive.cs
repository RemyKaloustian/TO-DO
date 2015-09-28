using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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


namespace TODO___SecondTry
{
    public static class Responsive
    {
        public static double GetScreenHeight()
        {
            return  Window.Current.Bounds.Height;
        }//GetScreenHeight()

        public static double GetScreenWidth()
        {
            return Window.Current.Bounds.Width;
        }//GetScreenWidth()



    }//Resonsive()
}
