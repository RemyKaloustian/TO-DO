﻿

#pragma checksum "D:\Documents\Desktop\TRAVAIL\DÉVELOPPEMENT\APPLICATIONS C#\TODO - SecondTry\TODO - SecondTry.WindowsPhone\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5A3232089372430C34A2DA4F1DA1E5F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TODO___SecondTry
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 30 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.NewTaskTextBox_KeyDown;
                 #line default
                 #line hidden
                #line 30 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.NewTaskTextBox_LostFocus;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 32 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.NewTaskTextBox_KeyDown;
                 #line default
                 #line hidden
                #line 32 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.NewTaskTextBox_LostFocus;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 21 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AddButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 58 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.HelpButton_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 59 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SettingsBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 60 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AboutButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


