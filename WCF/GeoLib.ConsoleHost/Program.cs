using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.ConsoleHost
{
	class Program
	{
		static void Main(string[] args)
		{
			ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));

			//configuration
			string address = "net.tcp://localhost:8009/GeoService";
			Binding binding = new NetTcpBinding();
			Type contract = typeof (IGeoService);
			//hostGeoManager.AddServiceEndpoint(contract, binding, address).EndpointBehaviors.Add();

			hostGeoManager.Open();


			Console.WriteLine("Service started. Press [Enter] to exit");
			Console.ReadLine();

			hostGeoManager.Close();
		}
	}
}
