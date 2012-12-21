using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.IO;

namespace SandBox
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		//BgmPlayer
		private int		bgmIndex;
		private bool	isBgmMute;

		private void Startup(object sender, EventArgs e)
		{
			StartupWindow win = new StartupWindow();
			win.ShowDialog();
			/*
			 *	 Be careful to add code here in case win return by
			 *	 Application.Current.Shutdown()
			 */
		}

		public void DragWindow(object sender, MouseButtonEventArgs args) 
		{
			this.DragMove(); 
		}

		public void Button_Close_Click(object sender, RoutedEventArgs args)
		{
			App.Current.Shutdown();
		}

		private void Button_Min_Click(object sender, RoutedEventArgs e) 
		{
			this.WindowState = WindowState.Minimized; 
		}

		private void Button_About_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow win = new AboutWindow();
			win.Owner = this;

			// Register Delegate Event Handler
			win.IsBgmMute_Event += new AboutWindow.IsBgmMuteDelegate(Bgm_IsMute);
			win.BgmMute_Event += new AboutWindow.BgmMuteDelegate(Bgm_Mute);
			win.BgmGetVolume_Event += new AboutWindow.BgmGetVolumeDelegate(Bgm_GetVolume);
			win.BgmVolume_Event += new AboutWindow.BgmVolumeDelegate(Bgm_Volume);

			win.ShowDialog();
		}

		private void Button_Dash_Click(object sender, RoutedEventArgs e)
		{
			DashWindow win = new DashWindow();
			win.Show();
		}

		private void Bgm_Initialize(object sender, EventArgs e)
		{
			bgmIndex = 0;
			isBgmMute = false;

			if (Directory.Exists("bgm"))
			{
				string[] bgmFiles = Directory.GetFiles("bgm", "*.mp3");

				if (bgmFiles.Length > 0)
				{
					BgmPlayer.Source = new Uri(bgmFiles[bgmIndex], UriKind.RelativeOrAbsolute);
					BgmPlayer.Play();
				}
			}
		}

		public void Bgm_Change(object sender, RoutedEventArgs args)
		{
			string[] bgmFiles = Directory.GetFiles("bgm", "*.mp3"); 
			if (bgmFiles.Length > 0)
			{
				BgmPlayer.Source = new Uri(bgmFiles[(++bgmIndex) % bgmFiles.Length], UriKind.RelativeOrAbsolute);
				BgmPlayer.Play();
			}
		}

		public bool Bgm_IsMute() { return isBgmMute; }

		public bool Bgm_Mute()
		{
			if (isBgmMute)
			{
				BgmPlayer.Play();
				isBgmMute = false;
				return false;
			}
			else
			{
				BgmPlayer.Pause();
				isBgmMute = true;
				return true;
			}
		}

		public double Bgm_GetVolume() 
		{
			return BgmPlayer.Volume; 
		}

		public void Bgm_Volume(double value) 
		{
			BgmPlayer.Volume = value * value / 100; 
		}

	}
}