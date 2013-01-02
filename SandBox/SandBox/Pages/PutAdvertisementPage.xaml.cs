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
	/// PutAdvertisementPage.xaml 的交互逻辑
	/// </summary>
	public partial class PutAdvertisementPage : Page
	{
		public PutAdvertisementPage()
		{
			InitializeComponent();
		}

		private void Amount_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (TextBox_Amount.Text == "")
			{
				TextBox_Amount_Bg.Visibility = Visibility.Visible;
			}
			else
			{
				TextBox_Amount_Bg.Visibility = Visibility.Hidden;
			}
		}

		private void ConfirmAmount_Click(object sender, RoutedEventArgs e)
		{
			char[] chrs = TextBox_Amount.Text.ToCharArray();
			foreach(char chr in chrs)
			{
				if (!char.IsDigit(chr))
				{
					return;
				}
			}
			TextBox_Amount.IsReadOnly = true;
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			if (!TextBox_Amount.IsReadOnly)
			{ 
				return;
			}
			
			// To add
		}
	}
}
