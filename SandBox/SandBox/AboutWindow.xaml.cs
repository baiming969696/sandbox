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
	/// AboutWindow.xaml 的交互逻辑
	/// </summary>
	public partial class AboutWindow : Window
	{
		public delegate bool IsBgmMuteDelegate();
		public delegate bool BgmMuteDelegate();
		public delegate double BgmGetVolumeDelegate();
		public delegate void BgmVolumeDelegate(double value);

		public event IsBgmMuteDelegate IsBgmMute_Event;
		public event BgmMuteDelegate BgmMute_Event;
		public event BgmGetVolumeDelegate BgmGetVolume_Event;
		public event BgmVolumeDelegate BgmVolume_Event;

		public AboutWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// To set Icon of Button_Mute right
			if (IsBgmMute_Event())
			{
				Button_Mute_Forbid.Visibility = Visibility.Visible;
			}

			// To set the value of Slide_Volume right
			Slide_Volume.Value = Math.Sqrt(BgmGetVolume_Event())*10;
		}

		private void Button_Close_Click(object sender, RoutedEventArgs e)
		{ 
			this.Close();
		}

		private void Button_Mute_MouseEnter(object sender, MouseEventArgs e) 
		{
			Button_Mute_Forbid.Visibility = Visibility.Visible; 
		}

		private void Button_Mute_MouseLeave(object sender, MouseEventArgs e) 
		{
			Button_Mute_Forbid.Visibility = Visibility.Hidden; 
		}

		public void Button_Mute_Click(object sender, RoutedEventArgs args)
		{
			if (BgmMute_Event())
			{
				// Now is stopped
				Button_Mute_Forbid.Visibility = Visibility.Visible;
			}
			else
			{
				// Now is playing
				Button_Mute_Forbid.Visibility = Visibility.Hidden;
			}
		}

		private void Slide_Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) 
		{
			BgmVolume_Event(e.NewValue); 
		}


	}
}
