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
	/// StartupWindow.xaml 的交互逻辑
	/// </summary>
	public partial class StartupWindow : Window
	{
		public StartupWindow()
		{
			InitializeComponent();
		}

		private void Showup_Completed(object sender, EventArgs e)
		{
			App_Sign.Visibility = Visibility.Hidden;
		}

		private void DragWindow(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void Button_Login_Click(object sender, RoutedEventArgs e)
		{
			// Save the information
			
			// To main window
			this.Close();
		}

		private void Nickname_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (TextBox_Nickname.Text == "")
			{
				TextBox_Nickname_Bg.Text = "Your Name";
			}
			else
			{
				TextBox_Nickname_Bg.Text = "";
			}
		}

	}
}
