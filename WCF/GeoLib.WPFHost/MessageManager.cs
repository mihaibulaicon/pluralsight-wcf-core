namespace GeoLib.WPFHost.Services
{
	public class MessageManager : IMessageService
	{
		public void ShowMessage(string message)
		{
			MainWindow.MainUI.ShowMessage(message);
		}
	}
}
