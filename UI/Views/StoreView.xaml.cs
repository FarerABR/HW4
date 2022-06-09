using DAL.Entity;
using DAL.Entity.Product;
using DAL.Enum.User;
using BLL.Repository;
using BLL.data_base;
using System.Windows;
using System.Windows.Media;

namespace UI.Views
{
	public partial class StoreView : Window
	{
		#region Properties
		private readonly User CurrentUser;
		#endregion

		public StoreView(User currentUser)
		{
			InitializeComponent();
			CurrentUser = currentUser;
			try
			{
				ProductsRepository.CreateGraphicsCard("RTX 3060", 500, 20, 3, CurrentUser);
				ProductsRepository.CreateRam("DDR4 16G", 120, 50, 2, CurrentUser);
				ProductsRepository.CreateProcessor("Core i9-11370", 2000, 15, 5, CurrentUser);
				ProductsRepository.CreateMotherboard("BTX", 200, 100, 1, CurrentUser);
				ProductsRepository.CreateRam("DDR6 64G", 1000, 0, 4, CurrentUser);
			}
			catch { }
			AddProducts();
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

		private void StoreWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (CurrentUser.Role == UserRole.customer)
			{
				ManageSep.Visibility = Visibility.Collapsed;
				ManageBtn.Visibility = Visibility.Collapsed;
			}
			UsernameTextBlock.Text = CurrentUser.Username;
			IDTextBlock.Text = "#" + CurrentUser.Id.ToString();
			ProductsBtn_Click(sender, e);
		}

		private void StoreWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Data_Access.WriteAllData();
		}

		private void ButtonsRefresh()
		{
			ProductsBtn.IsEnabled = true;
			ProductsBtn.Background = null;

			CartBtn.IsEnabled = true;
			CartBtn.Background = null;

			AccountBtn.IsEnabled = true;
			AccountBtn.Background = null;

			ManageBtn.IsEnabled = true;
			ManageBtn.Background = null;
		}

		private void PagesRefresh()
		{
			ProductsPage.Visibility = Visibility.Hidden;
		}

		private void ProductsBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			ProductsBtn.Background = Brushes.DeepSkyBlue;
			ProductsBtn.IsEnabled = false;
			ProductsPage.Visibility = Visibility.Visible;
			SearchTextBox.Focus();
		}

		private void CartBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			CartBtn.Background = Brushes.DeepSkyBlue;
			CartBtn.IsEnabled = false;
		}

		private void AccountBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			AccountBtn.Background = Brushes.DeepSkyBlue;
			AccountBtn.IsEnabled = false;
		}

		private void ManageBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			ManageBtn.Background = Brushes.DeepSkyBlue;
			ManageBtn.IsEnabled = false;
		}

		private void AboutBtn_Click(object sender, RoutedEventArgs e)
		{
			PagesRefresh();
			LeftSidePanel.IsEnabled = false;
			AboutGrid.Visibility = Visibility.Visible;
		}

		private void AboutOk_Click(object sender, RoutedEventArgs e)
		{
			LeftSidePanel.IsEnabled = true;
			AboutGrid.Visibility = Visibility.Collapsed;
			ProductsBtn_Click(sender, e);
		}

		private void LogOutBtn_Click(object sender, RoutedEventArgs e)
		{
			if(UserRepository.LastLoggedIn() != null)
			{
				UserRepository.ClearLastLoggedIn();
			}
			Data_Access.WriteAllData();
			ShellView WelcomeWindow = new();
			Close();
			WelcomeWindow.Show();
		}

		private void Temp_Click(object sender, RoutedEventArgs e)
		{
			//start search
		}

		private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
		{
			LeftSidePanel.IsEnabled = false;
			SearchPanel.IsEnabled = false;
			ProductsPanel.Visibility = Visibility.Collapsed;
			AddProductPage.Visibility = Visibility.Visible;
		}

		private void AddedProductType_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

		}

		private void AddProductBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void CancelAddProductBtn_Click(object sender, RoutedEventArgs e)
		{
			AddProductPage.Visibility = Visibility.Collapsed;
			LeftSidePanel.IsEnabled = true;
			SearchPanel.IsEnabled = true;
			ProductsPanel.Visibility = Visibility.Visible;
		}

		public static void ShowProductDetails(Product product)
		{

		}
	}
}