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
			//屏蔽中文输入和非法字符粘贴输入
			TextBox textBox = sender as TextBox;
			TextChange[] change = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(change, 0);

			int offset = change[0].Offset;
			if (change[0].AddedLength > 0)
			{
				int num = 0;
				if (!Int32.TryParse(TextBox_Amount.Text, out num))
				{
					textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}

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
			if (TextBox_Amount.Text == "")
			{
				return;
			}

			TextBox_Amount.IsReadOnly = true;
			TextBox_Amount.Foreground = FindResource("SBBrush_Red") as Brush;
			TextBox_Amount.BorderThickness = new Thickness(0);
			TextBox_Amount.FontWeight = FontWeights.Bold;
			ConfirmAmount.Visibility = Visibility.Hidden;
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			if (!TextBox_Amount.IsReadOnly)
			{ 
				(App.Current as App).action.WarningBox("请输入金额");
				return;
			}
			
			(App.Current as App).action.Update();
		}

	}
}
