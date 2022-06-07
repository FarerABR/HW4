using DAL.Entity.User;
using DAL.Entity.Product;
using BLL.Repository;
using DAL.Enum.User;
using UI.Views;
using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

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
			try
			{
				ProductsRepository.CreateGraphicsCard("RTX 3060", "20%", "500$", 3, "../Images/GPU.jpg");
				ProductsRepository.CreateRam("DDR4 16G", "50%", "120$", 2, "../Images/RAM.jpg");
				ProductsRepository.CreateProcessor("Core i9-11370", "15%", "2000$", 5, "../Images/CPU.jpg");
				ProductsRepository.CreateMotherboard("BTX", "100%", "200$", 1, "../Images/Motherboard.jpg");
				ProductsRepository.CreateRam("DDR6 64G", "0%", "1000$", 4, "../Images/RAM.jpg");
			}
			catch { }
			AddProducts();
			CurrentUser = currentUser;
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
			if (CurrentUser.Role == UserRole.admin)
			{
				ManageSep.Visibility = Visibility.Visible;
				ManageBtn.Visibility = Visibility.Visible;
			}
			UsernameTextBlock.Text = CurrentUser.Username;
			IDTextBlock.Text = "#" + CurrentUser.Id.ToString();
			ProductsBtn_Click(sender, e);
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
			ProductsPage.Visibility = Visibility.Collapsed;
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

		private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AboutOk_Click(object sender, RoutedEventArgs e)
		{
			LeftSidePanel.IsEnabled = true;
			AboutGrid.Visibility = Visibility.Collapsed;
			ProductsBtn_Click(sender, e);
		}

		private void AddedProductType_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

		}
	}
}