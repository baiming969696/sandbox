using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.ComponentModel; 
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
using SandBox.Actions;

namespace SandBox
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		// Controller
		private MainAction action;

		// BgmPlayer
		private int		bgmIndex;
		private bool	isBgmMute;

		// Frame Fade In / Fade Out
		private bool _allowDirectNavigation = false;
		private NavigatingCancelEventArgs _navArgs = null;

		private void Startup(object sender, EventArgs e)
		{
			StartupWindow win = new StartupWindow();
			win.Closing += new CancelEventHandler(Startup_Returning);
			win.ShowDialog();

			// Title & SandGlass
			TextBox_Year.Text = this.FindResource("String_" + action.year.ToString()) as String;
			TextBox_Season.Text = (action.year == MainAction.Year.BeginningYear) ? "" : (String)this.FindResource("String_" + action.season.ToString());
			SandGlass_Update(action.GetPhase());

			// Other items in LeftBar
			for (int i = 1; i < LeftBar.Children.Count; i++ )
			{
				TextBlock box = LeftBar.Children[i] as TextBlock;

				if (i <= action.siblingPages.Count)
				{
					box.Text = (String)this.FindResource("String_" + action.siblingPages[i-1].ToString() + "_Name");
					if (action.siblingPages[i-1].Equals(action.page))
					{
						box.FontWeight = FontWeights.Bold;
						box.Foreground = FindResource("SBBrush_Red") as Brush;
					}
					else 
					{
						box.FontWeight = FontWeights.Normal;
						box.Foreground = FindResource("SBBrush_Gray") as Brush;
					}
				}
				else
				{
					box.Text = "";
				}
			}

			// Navigate to the Page
			PageFrame.Navigate(new Uri("Pages/"+action.page.ToString()+".xaml", UriKind.Relative));
		}

		private void Startup_Returning(object sender, CancelEventArgs e)
		{
			action = new MainAction();
			action.name = (sender as StartupWindow).TextBox_Nickname.Text;
			action.role = (sender as StartupWindow).Button_Role.IsChecked.Value ? MainAction.Role.Admin : MainAction.Role.Player;
			action.mode = (sender as StartupWindow).Button_Mode.IsChecked.Value ? MainAction.Mode.LAN : MainAction.Mode.Local;
			action.Initialize();
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

		private bool Bgm_Mute()
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

		private double Bgm_GetVolume() 
		{
			return BgmPlayer.Volume; 
		}

		private void Bgm_Volume(double value) 
		{
			BgmPlayer.Volume = value * value / 100; 
		}

		private void SandGlass_Update(double value)
		{
			SandGlass_Upside.Height = 325 - 270 * value;
			SandGlass_Downside.Height = 175 + 270 * value;
		}

		private void PageFrame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
		{
			if (Content != null && !_allowDirectNavigation)
			{
				e.Cancel = true;
				_navArgs = e;
				this.IsHitTestVisible = false;
				DoubleAnimation da = new DoubleAnimation(0.3d, new Duration(TimeSpan.FromMilliseconds(300)));
				da.Completed += PageFrame_FadeOutCompleted;
				PageFrame.BeginAnimation(OpacityProperty, da);
			}
			_allowDirectNavigation = false;
		}

		private void PageFrame_FadeOutCompleted(object sender, EventArgs e)
		{
			(sender as AnimationClock).Completed -= PageFrame_FadeOutCompleted;

			this.IsHitTestVisible = true;

			_allowDirectNavigation = true;
			switch (_navArgs.NavigationMode)
			{
				case NavigationMode.New:
					if (_navArgs.Uri == null)
					{
						PageFrame.Navigate(_navArgs.Content);
					}
					else
					{
						PageFrame.Navigate(_navArgs.Uri);
					}
					break;
				case NavigationMode.Back:
					PageFrame.GoBack();
					break;

				case NavigationMode.Forward:
					PageFrame.GoForward();
					break;
				case NavigationMode.Refresh:
					PageFrame.Refresh();
					break;
			}

			Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
				(ThreadStart)delegate() {
				DoubleAnimation da = new DoubleAnimation(1.0d, new Duration(TimeSpan.FromMilliseconds(200)));
				PageFrame.BeginAnimation(OpacityProperty, da);
			});
		}

	}
}