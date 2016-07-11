using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using GeoLib.Services;
using GeoLib.WPFHost.Services;

namespace GeoLib.WPFHost
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ServiceHost _HostgeoManager = null;
		private ServiceHost _HostMessageManager = null;

		public static MainWindow MainUI { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			btnStart.IsEnabled = true;
			btnStop.IsEnabled = false;

			MainUI = this;

			this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			_HostgeoManager = new ServiceHost(typeof(GeoManager));
			_HostMessageManager = new ServiceHost(typeof(MessageManager));
			_HostgeoManager.Open();
			_HostMessageManager.Open();

			btnStart.IsEnabled = false;
			btnStop.IsEnabled = true;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			_HostgeoManager.Close();
			_HostMessageManager.Close();
			btnStart.IsEnabled = true;
			btnStop.IsEnabled = false;
		}

		public void ShowMessage(string message)
		{
			int threadId = Thread.CurrentThread.ManagedThreadId;
			lblMessage.Content = message + Environment.NewLine + "Thread Id " + threadId + " | Process " +
			                     Process.GetCurrentProcess().Id;
		}
	}
}
