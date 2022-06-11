using DAL.Entity;
using DAL.Entity.Product;
using DAL.Enum.User;
using DAL.Enum.Product;
using BLL.Repository;
using BLL.data_base;
using System.Windows;
using System.Windows.Media;
using System;
using Microsoft.Win32;

namespace UI.Views
{
	public partial class StoreView : Window
	{
		#region Properties
		private readonly User CurrentUser;
		private ProductView? NewProductPreview;
		string? ImagePath = null;
		#endregion

		public StoreView(User currentUser)
		{
			InitializeComponent();
			CurrentUser = currentUser;
		}

		private void AddProducts(string types, string search)
		{
			ProductsWrapPanel.Children.Clear();

			if(types.Contains('1'))
				foreach (var x in ProductsRepository.Processor_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && x.Name.Contains(search))
					{
						ProductView productView = new(x);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('2'))
				foreach (var x in ProductsRepository.GraphicsCard_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && x.Name.Contains(search))
					{
						ProductView productView = new(x);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('3'))
				foreach (var x in ProductsRepository.Ram_List)
				{
					if(x.ViewStatus != ViewStatus.deleted && x.Name.Contains(search))
					{
						ProductView productView = new(x);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('4'))
				foreach (var x in ProductsRepository.Motherboard_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && x.Name.Contains(search))
					{
						ProductView productView = new(x);
						ProductsWrapPanel.Children.Add(productView);
					}
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
			UsernameTextBlock.ToolTip = CurrentUser.Username;
			IDTextBlock.Text = "#" + CurrentUser.Id.ToString();

			CPUCheck.IsChecked = true;
			GPUCheck.IsChecked = true;
			RAMCheck.IsChecked = true;
			MotherboardCheck.IsChecked = true;
			Temp_Click(sender, e);

			ProductsBtn_Click(sender, e);

			CPUSeries.ItemsSource = Enum.GetValues(typeof(ProcessorType));
			CPUBrand.ItemsSource = Enum.GetValues(typeof(Brand));

			GPUMemoryType.ItemsSource = Enum.GetValues(typeof(GraphMemType));
			GPUBrand.ItemsSource = Enum.GetValues(typeof(Brand));

			RAMMemoryType.ItemsSource = Enum.GetValues(typeof(RamMemType));
			RAMBrand.ItemsSource = Enum.GetValues(typeof(Brand));

			MotherBasedOn.ItemsSource = Enum.GetValues(typeof(MotherBased));
			MotherBrand.ItemsSource = Enum.GetValues(typeof(Brand));
			MotherRaid.ItemsSource = Enum.GetValues(typeof(RAID));
		}

		private void StoreWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Data_Access.WriteAllData();
			Application.Current.Shutdown();
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
			CartPage.Visibility = Visibility.Hidden;
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
			CartPage.Visibility = Visibility.Visible;
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
			string mode = "";

			if (string.IsNullOrEmpty(SearchTextBox.Text))
				SearchTextBox.Text = "";
			if ((bool)CPUCheck.IsChecked) mode += "1";
			if ((bool)GPUCheck.IsChecked) mode += "2";
			if ((bool)RAMCheck.IsChecked) mode += "3";
			if ((bool)MotherboardCheck.IsChecked) mode += "4";

			AddProducts(mode, SearchTextBox.Text);
		}

		private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
		{
			LeftSidePanel.IsEnabled = false;
			SearchPanel.IsEnabled = false;
			ProductsPanel.Visibility = Visibility.Collapsed;

			Preview.Children.Clear();

			Product temp = new("name", 0, 10, 0, CurrentUser, 0, Brand.Intel);
			{
				temp.Image = ImagePath;
			}
			NewProductPreview = new(temp);
			NewProductPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFFAFF"));
			Preview.Children.Add(NewProductPreview);
			
			ProductsPanel.Visibility = Visibility.Collapsed;

			AddProductPage.Visibility = Visibility.Visible;
			AddedProductType.Focus();
		}

		private void AddedProductType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			switch (AddedProductType.SelectedItem.ToString())
			{
				case "System.Windows.Controls.ComboBoxItem: CPU":
					{
						RefreshParticularInfoGrids();
						CPUParticularInfo.Visibility = Visibility.Visible;
						break;
					}
				case "System.Windows.Controls.ComboBoxItem: GPU":
					{
						RefreshParticularInfoGrids();
						GPUParticularInfo.Visibility = Visibility.Visible;
						break;
					}
				case "System.Windows.Controls.ComboBoxItem: RAM":
					{
						RefreshParticularInfoGrids();
						RAMParticularInfo.Visibility = Visibility.Visible;
						break;
					}
				case "System.Windows.Controls.ComboBoxItem: Motherboard":
					{
						RefreshParticularInfoGrids();
						MotherParticularInfo.Visibility = Visibility.Visible;
						break;
					}
				default: { break; }
			}
		}

		private void RefreshParticularInfoGrids()
		{
			CPUParticularInfo.Visibility = Visibility.Collapsed;
			GPUParticularInfo.Visibility = Visibility.Collapsed;
			RAMParticularInfo.Visibility = Visibility.Collapsed;
			MotherParticularInfo.Visibility = Visibility.Collapsed;
		}

		private void RefreshPreview()
		{
			string? name = NameTextBox.Text;
			string? price = PriceTextBox.Text;
			string? discount = DiscountTextBox.Text;

			try
			{
				if (string.IsNullOrEmpty(name))
					name = "";
				if (string.IsNullOrEmpty(price))
					price = "0";
				if (string.IsNullOrEmpty(discount))
					discount = "0";

				Product temp = new(name, double.Parse(price), int.Parse(discount), RatingBar.Value, CurrentUser, 0, Brand.Intel);
				{
					temp.Image = ImagePath;
				}
				NewProductPreview.SetProperties(temp);
			}
			catch { }
		}

		public static void ShowProductDetails(Product product)
		{

		}

		private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			RefreshPreview();
		}

		private void PriceTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				double.Parse(PriceTextBox.Text);
				PriceError.Visibility = Visibility.Collapsed;
				if (DiscountError.Visibility != Visibility.Visible)
					RefreshPreview();
			}
			catch
			{
				PriceError.Visibility = Visibility.Visible;
			}
		}

		private void DiscountTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				int t = int.Parse(DiscountTextBox.Text);
				if (0 <= t && t <= 100)
				{
					DiscountError.Visibility = Visibility.Collapsed;
					if (PriceError.Visibility != Visibility.Visible)
						RefreshPreview();
				}
				else throw new Exception();
			}
			catch
			{
				DiscountError.Visibility = Visibility.Visible;
			}
		}

		private void RatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
		{
			RefreshPreview();
		}

		private void CheckKeyNum(object sender, System.Windows.Input.KeyEventArgs e)
		{
			e.Handled = !((new System.Text.RegularExpressions.Regex("[0-9]").IsMatch(e.Key.ToString())
				|| e.Key.ToString() == "Escape" || e.Key.ToString() == "Return"));
		}

		private void AddImage_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog fileOpen = new();
			fileOpen.Multiselect = false;
			fileOpen.Title = "Choose your image";
			fileOpen.Filter = "JPG Files (*.jpg)| *.jpg|PNG Files (*.png)| *.png";

			if ((bool)fileOpen.ShowDialog())
				ImagePath = fileOpen.FileName;

			RefreshPreview();
		}

		private void RemoveImage_Click(object sender, RoutedEventArgs e)
		{
			ImagePath = "";
			RefreshPreview();
		}

		private void RefreshNewProductPage()
		{
			NameTextBox.Text = null;
			PriceTextBox.Text = null;
			DiscountTextBox.Text = null;

			CPUSeries.SelectedItem = null;
			CPUBrand.SelectedItem = null;
			CoreCountTextBox.Text = null;

			GPUMemoryType.SelectedItem = null;
			GPUHDMICount.Text = null;
			GPUBrand.SelectedItem = null;

			RAMMemoryType.SelectedItem = null;
			RAMBrand.SelectedItem = null;
			RAMModuleCount.Text = null;
			RAMModuleCopacity.Text = null;

			MotherBasedOn.SelectedItem = null;
			MotherBrand.SelectedItem = null;
			RAMSlots.Text = null;
			PCISlots.Text = null;
		}

		private void AddProductBtn_Click(object sender, RoutedEventArgs e)
		{
			bool key = false;
			string productMode = "";
			if (!string.IsNullOrEmpty(NameTextBox.Text) && PriceError.Visibility != Visibility.Visible && DiscountError.Visibility != Visibility.Visible)
			{
				switch (AddedProductType.SelectedItem.ToString())
				{
					case "System.Windows.Controls.ComboBoxItem: CPU":
						{
							productMode = "CPU";
							if (CPUSeries.SelectedIndex == -1 || string.IsNullOrEmpty(CoreCountTextBox.Text) || CPUBrand.SelectedIndex == -1)
								AddError.Text = "* fill the required fields";
							else
							{
								key = true;
							}
							break;
						}
					case "System.Windows.Controls.ComboBoxItem: GPU":
						{
							productMode = "GPU";
							if (GPUMemoryType.SelectedIndex == -1 || string.IsNullOrEmpty(GPUHDMICount.Text) || GPUBrand.SelectedIndex == -1)
								AddError.Text = "* fill the required fields";
							else
							{
								key = true;
							}
							break;
						}
					case "System.Windows.Controls.ComboBoxItem: RAM":
						{
							productMode = "RAM";
							if (RAMMemoryType.SelectedIndex == -1 || RAMBrand.SelectedIndex == -1 || string.IsNullOrEmpty(RAMModuleCount.Text)
								|| string.IsNullOrEmpty(RAMModuleCopacity.Text))
								AddError.Text = "* fill the required fields";
							else
							{
								key = true;
							}
							break;
						}
					case "System.Windows.Controls.ComboBoxItem: Motherboard":
						{
							productMode = "Motherboard";
							if (MotherBasedOn.SelectedIndex == -1 || MotherBrand.SelectedIndex == -1 || string.IsNullOrEmpty(RAMSlots.Text)
								|| string.IsNullOrEmpty(PCISlots.Text) || MotherRaid.SelectedIndex == -1)
								AddError.Text = "* fill the required fields";
							else
							{
								key = true;
							}
							break;
						}
					default: { break; }
				}
			}
			else
			{
				AddError.Text = "* inputs invalid";
			}

			if(key)
			{
				try
				{
					switch(productMode)
					{
						case "CPU":
							{
								if (ImagePath != null)
									ProductsRepository.CreateProcessor(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath,CurrentUser,
									int.Parse(CoreCountTextBox.Text), (ProcessorType)Enum.Parse(typeof(ProcessorType), CPUSeries.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), CPUBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateProcessor(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									int.Parse(CoreCountTextBox.Text), (ProcessorType)Enum.Parse(typeof(ProcessorType), CPUSeries.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), CPUBrand.SelectedItem.ToString()));
								break;
							}
						case "GPU":
							{
								if (ImagePath != null)
									ProductsRepository.CreateGraphicsCard(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									int.Parse(GPUHDMICount.Text), (GraphMemType)Enum.Parse(typeof(GraphMemType), GPUMemoryType.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), GPUBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateGraphicsCard(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									int.Parse(GPUHDMICount.Text), (GraphMemType)Enum.Parse(typeof(GraphMemType), GPUMemoryType.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), GPUBrand.SelectedItem.ToString()));
								break;
							}
						case "RAM":
							{
								if (ImagePath != null)
									ProductsRepository.CreateRam(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									int.Parse(RAMModuleCount.Text), (RamMemType)Enum.Parse(typeof(RamMemType), RAMMemoryType.SelectedItem.ToString()), int.Parse(RAMModuleCopacity.Text), (Brand)Enum.Parse(typeof(Brand), RAMBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateRam(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									int.Parse(RAMModuleCount.Text), (RamMemType)Enum.Parse(typeof(RamMemType), RAMMemoryType.SelectedItem.ToString()), int.Parse(RAMModuleCopacity.Text), (Brand)Enum.Parse(typeof(Brand), RAMBrand.SelectedItem.ToString()));
								break;
							}
						case "Motherboard":
							{
								if (ImagePath != null)
									ProductsRepository.CreateMotherboard(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									(MotherBased)Enum.Parse(typeof(MotherBased), MotherBasedOn.SelectedItem.ToString()), (RAID)Enum.Parse(typeof(RAID), MotherRaid.SelectedItem.ToString()), int.Parse(RAMSlots.Text), int.Parse(PCISlots.Text), (Brand)Enum.Parse(typeof(Brand), MotherBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateMotherboard(NameTextBox.Text, double.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									(MotherBased)Enum.Parse(typeof(MotherBased), MotherBasedOn.SelectedItem.ToString()), (RAID)Enum.Parse(typeof(RAID), MotherRaid.SelectedItem.ToString()), int.Parse(RAMSlots.Text), int.Parse(PCISlots.Text), (Brand)Enum.Parse(typeof(Brand), MotherBrand.SelectedItem.ToString()));
								break;
							}
						default: { break; }
					}
					CancelAddProductBtn_Click(sender, e);
					ImagePath = null;
					RefreshNewProductPage();
					Temp_Click(sender, e);
					MessageBox.Show("Product successfully added", "Product added", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message, "Adding product failed", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void CancelAddProductBtn_Click(object sender, RoutedEventArgs e)
		{
			AddError.Text = "";
			AddProductPage.Visibility = Visibility.Hidden;
			LeftSidePanel.IsEnabled = true;
			SearchPanel.IsEnabled = true;
			ProductsPanel.Visibility = Visibility.Visible;
		}
	}
}