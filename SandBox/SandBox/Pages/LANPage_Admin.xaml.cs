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
	/// LANPage_Admin.xaml 的交互逻辑
	/// </summary>
	public partial class LANPage_Admin : Page
	{
		public LANPage_Admin()
		{
			InitializeComponent();
		}

		private void SetupServer_Click(object sender, RoutedEventArgs e)
		{

		}

		private void StartGame_Click(object sender, RoutedEventArgs e)
		{

		}

		private void HostAddress_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (TextBox_HostAddress.Text == "")
			{
				TextBox_HostAddress_Bg.Visibility = Visibility.Visible;
			}
			else
			{
				TextBox_HostAddress_Bg.Visibility = Visibility.Hidden;
			}
		}
	}
}
