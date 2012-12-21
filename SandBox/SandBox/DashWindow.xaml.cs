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
using System.Windows.Shapes;

namespace SandBox
{
	/// <summary>
	/// DashWindow.xaml 的交互逻辑
	/// </summary>
	public partial class DashWindow : Window
	{
		public DashWindow()
		{
			InitializeComponent();
		}

		public void DragWindow(object sender, MouseButtonEventArgs args)
		{
			this.DragMove();
		}

		public void Button_Close_Click(object sender, RoutedEventArgs args)
		{
			this.Close();
		}

	}
}
