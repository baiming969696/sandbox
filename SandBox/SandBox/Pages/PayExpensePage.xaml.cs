using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SandBox.Pages
{
    /// <summary>
    /// PayExpensePage.xaml 的交互逻辑
    /// </summary>
    public partial class PayExpensePage : Page
    {
        public PayExpensePage()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).action.Update();
        }
    }
}
