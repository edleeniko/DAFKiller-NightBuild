using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace DAFKiller_NightBuild.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UiWindow
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void UiWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            { DragMove(); }
        }

        private void UiWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (this.WindowState == WindowState.Normal)
            { this.WindowState = WindowState.Maximized; }
            else
            { this.WindowState = WindowState.Normal; }*/
        }

        private void diagnosticexpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (specialfunctionsexpander.IsExpanded == true || documentspexpander.IsExpanded == true || settingspexpander.IsExpanded == true )
            {
                specialfunctionsexpander.IsExpanded = false;
                documentspexpander.IsExpanded = false;
                settingspexpander.IsExpanded=false;
            }
        }
        private void specialfunctionsexpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (diagnosticexpander.IsExpanded == true || documentspexpander.IsExpanded == true || settingspexpander.IsExpanded == true)
            {
                diagnosticexpander.IsExpanded = false;
                documentspexpander.IsExpanded = false;
                settingspexpander.IsExpanded = false;
            }
        }
        private void documentspexpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (diagnosticexpander.IsExpanded == true || specialfunctionsexpander.IsExpanded == true || settingspexpander.IsExpanded == true)
            {
                diagnosticexpander.IsExpanded = false;
                specialfunctionsexpander.IsExpanded = false;
                settingspexpander.IsExpanded = false;
            }
        }
        private void settingspexpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (diagnosticexpander.IsExpanded == true || specialfunctionsexpander.IsExpanded == true || documentspexpander.IsExpanded == true)
            {
                diagnosticexpander.IsExpanded = false;
                specialfunctionsexpander.IsExpanded = false;
                documentspexpander.IsExpanded = false;
            }
        }

    }
}
