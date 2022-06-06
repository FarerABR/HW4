using DAL.Entity.User;
using DAL.Entity.Product;
using BLL.Repository;
using DAL.Enum.User;
using UI.Views;
using System.Windows;
using System.Windows.Media;

namespace UI.Views
{
	public partial class StoreView : Window
	{
		#region Properties
		private readonly User? CurrentUser;
		#endregion

		public StoreView(User currentUser)
		{
			InitializeComponent();
			ProductsRepository.CreateGraphicsCard("DDR4 16G", "20%", "100$", 4, "../Images/GPU.jpg");
			AddProducts();
			CurrentUser = currentUser;
		}

		private void StoreWindow_Loaded(object sender, RoutedEventArgs e)
		{
			ProductsBtn.Background = Brushes.DeepSkyBlue;
			ProductsBtn.IsEnabled = false;
		}

		private void ProductsBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void CartBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AccountBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ManageBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AboutBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void LogOutBtn_Click(object sender, RoutedEventArgs e)
		{
			ShellView WelcomeWindow = new();
			Close();
			WelcomeWindow.Show();
		}

		private void Temp_Click(object sender, RoutedEventArgs e)
		{
			//start search
		}

		private void AddProducts()
		{
			foreach (var x in ProductsRepository.Ram_List)
			{
				ProductView productView = new(x);
				ProductsWrapPanel.Children.Add(productView);
			}
			foreach (var x in ProductsRepository.Processor_List)
			{
				ProductView productView = new(x);
				ProductsWrapPanel.Children.Add(productView);
			}
			foreach (var x in ProductsRepository.GraphicsCard_List)
			{
				ProductView productView = new(x);
				ProductsWrapPanel.Children.Add(productView);
			}
			foreach (var x in ProductsRepository.Motherboard_List)
			{
				ProductView productView = new(x);
				ProductsWrapPanel.Children.Add(productView);
			}
		}

		private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
