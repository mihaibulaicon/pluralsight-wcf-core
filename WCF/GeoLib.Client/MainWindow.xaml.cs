using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeoLib.Client.Services;
using GeoLib.Contracts;
using GeoLib.Proxies;

namespace GeoLib.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnGetInfo_Click(object sender, RoutedEventArgs e)
		{
			if (zipCodeTxt.Text != "")
			{
				GeoClient proxy = new GeoClient("tcpEP");
				ZipCodeData data = proxy.GetZipInfo(zipCodeTxt.Text);
				if (data != null)
				{
					lblCity.Content = data.City;
					lblState.Content = data.State;
				}
				proxy.Close();
			}
		}

		private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
		{
			if (stateTxt.Text != null)
			{
				var binding = new NetTcpBinding();
				EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
				GeoClient proxy = new GeoClient(binding, address);
				var data = proxy.GetZips(stateTxt.Text);
				if (data != null)
					lstZips.ItemsSource = data;
			}
		}

		private void btnMakeCall_Click(object sender, RoutedEventArgs e)
		{
			var binding = new NetTcpBinding();
			EndpointAddress address = new EndpointAddress("net.tcp://localhost:8008/MessageService");

			var factory = new ChannelFactory<IMessageService>(binding, address);
			IMessageService proxy = factory.CreateChannel();
			proxy.ShowMsg(txtToShow.Text);
			factory.Close();
		}
	}
}
