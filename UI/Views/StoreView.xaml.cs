using Caliburn.Micro;
using DAL.Entity.User;
using DAL.Entity.Product;
using UI.Views;

namespace UI.Views
{
	/// <summary>
	/// Interaction logic for Window.xaml
	/// </summary>
	public partial class StoreView : Window
	{
		public StoreView(User CurrentUser)
		{
			InitializeComponent();
		}
	}
}
