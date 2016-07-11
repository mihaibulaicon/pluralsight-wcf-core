using System.ServiceModel;

namespace GeoLib.WPFHost.Services
{
	[ServiceContract]
	public interface IMessageService
	{
		[OperationContract]
		void ShowMessage(string message);
	}
}
