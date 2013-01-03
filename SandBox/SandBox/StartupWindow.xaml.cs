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
			if (TextBox_Nickname.Text == "")
			{
				TextBox_Nickname.Text = "Unnamed";
			}

			// Back to main window
			// It will activate the "Closing " event and call MainAction to gather the information here
			this.Close();
		}

		private void Nickname_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			TextBox_Nickname_Bg.Visibility = (TextBox_Nickname.Text == "") ? Visibility.Visible : Visibility.Hidden;
		}

	}
}
