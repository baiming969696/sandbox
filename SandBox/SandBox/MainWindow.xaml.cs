using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Navigation;

namespace SandBox
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		// BgmPlayer
		private int		bgmIndex;
		private bool	isBgmMute;

		// Frame Fade In / Fade Out
		private bool _allowDirectNavigation = false;
		private NavigatingCancelEventArgs _navArgs = null;

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

		private void Frame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
		{
			if (Content != null && !_allowDirectNavigation)
			{
				e.Cancel = true;
				_navArgs = e;
				this.IsHitTestVisible = false;
				DoubleAnimation da = new DoubleAnimation(0.3d, new Duration(TimeSpan.FromMilliseconds(300)));
				da.Completed += Frame_FadeOutCompleted;
				MainFrame.BeginAnimation(OpacityProperty, da);
			}
			_allowDirectNavigation = false;
		}

		private void Frame_FadeOutCompleted(object sender, EventArgs e)
		{
			(sender as AnimationClock).Completed -= Frame_FadeOutCompleted;

			this.IsHitTestVisible = true;

			_allowDirectNavigation = true;
			switch (_navArgs.NavigationMode)
			{
				case NavigationMode.New:
					if (_navArgs.Uri == null)
					{
						MainFrame.Navigate(_navArgs.Content);
					}
					else
					{
						MainFrame.Navigate(_navArgs.Uri);
					}
					break;
				case NavigationMode.Back:
					MainFrame.GoBack();
					break;

				case NavigationMode.Forward:
					MainFrame.GoForward();
					break;
				case NavigationMode.Refresh:
					MainFrame.Refresh();
					break;
			}

			Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
				(ThreadStart)delegate() {
				DoubleAnimation da = new DoubleAnimation(1.0d, new Duration(TimeSpan.FromMilliseconds(200)));
				MainFrame.BeginAnimation(OpacityProperty, da);
			});
		}

		private void MouseDown_Tester(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			// 在此处添加事件处理程序实现。
			// MainFrame.Navigate( new Uri("/Pages/LANPage.xaml", UriKind.Relative));

			//Net.TCPServer net = new Net.TCPServer("127.0.0.1");
			//net.Server_Setup();

		}
	}
}