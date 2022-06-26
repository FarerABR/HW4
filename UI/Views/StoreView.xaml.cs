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
		public readonly User CurrentUser;
		private ProductView? NewProductPreview;
		private string? ImagePath = null;
		private Product? CurrentDetailingProduct = null;
		private decimal TotalCartCharge;
		#endregion

		public StoreView(User currentUser)
		{
			InitializeComponent();
			CurrentUser = currentUser;
		}

		private void AddProducts(string types, string search)
		{
			ProductsWrapPanel.Children.Clear();

			if (types.Contains('1'))
				foreach (var x in ProductsRepository.Processor_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && !ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('2'))
				foreach (var x in ProductsRepository.GraphicsCard_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && !ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('3'))
				foreach (var x in ProductsRepository.Ram_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && !ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('4'))
				foreach (var x in ProductsRepository.Motherboard_List)
				{
					if (x.ViewStatus != ViewStatus.deleted && !ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
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
				AddNewProductBtn.Visibility = Visibility.Hidden;
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
		}

		#region Main Buttons
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
			AccountPage.Visibility = Visibility.Hidden;
			ManagePage.Visibility = Visibility.Hidden;
		}

		private void ProductsBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			ProductsBtn.Background = Brushes.DeepSkyBlue;
			ProductsBtn.IsEnabled = false;
			Temp_Click(sender, e);
			ProductsPage.Visibility = Visibility.Visible;
			SearchTextBox.Focus();
		}

		private void CartBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			CartBtn.Background = Brushes.DeepSkyBlue;
			CartBtn.IsEnabled = false;
			RefreshCartPanel();
			CartPage.Visibility = Visibility.Visible;
			ConfirmPurchaseBtn.Focus();
		}

		private void AccountBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			AccountBtn.Background = Brushes.DeepSkyBlue;
			AccountBtn.IsEnabled = false;
			AccountPage.Visibility = Visibility.Visible;
		}

		private void ManageBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			ManageBtn.Background = Brushes.DeepSkyBlue;
			ManageBtn.IsEnabled = false;
			ManagePage.Visibility = Visibility.Visible;
		}

		private void AboutBtn_Click(object sender, RoutedEventArgs e)
		{
			Temp.IsDefault = false;
			ConfirmPurchaseBtn.IsDefault = false;
			LeftSidePanel.IsEnabled = false;
			AboutGrid.Visibility = Visibility.Visible;
		}

		private void AboutOk_Click(object sender, RoutedEventArgs e)
		{
			Temp.IsDefault = true;
			ConfirmPurchaseBtn.IsDefault = true;
			LeftSidePanel.IsEnabled = true;
			AboutGrid.Visibility = Visibility.Collapsed;
		}

		private void LogOutBtn_Click(object sender, RoutedEventArgs e)
		{
			if (UserRepository.StayLoggedInUser() != null)
			{
				UserRepository.ClearStayLoggedIn();
			}
			Data_Access.WriteAllData();
			ShellView WelcomeWindow = new();
			Close();
			WelcomeWindow.Show();
		}
		#endregion

		#region Products
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

		#region Product details
		public void ShowProductDetails(Product product)
		{
			CurrentDetailingProduct = product;
			ProductsPanel.Visibility = Visibility.Collapsed;
			SearchPanel.IsEnabled = false;
			DetailsEditError.Visibility = Visibility.Collapsed;
			DetailsEditSuccess.Visibility = Visibility.Collapsed;

			DetailsProductName.Text = " " + product.Name;
			DetailsProductPrice.Text = " " + product.Price.ToString();
			DetailsProductDiscount.Text = " " + product.Discount.ToString();
			DetailsRating.Value = product.Rating;
			CreationDateTextBlock.Text = product.Date_Of_Registration.ToShortDateString();
			CreationDateTextBlock.Text += "\n" + product.Date_Of_Registration.ToShortTimeString();

			FourthDetail.Visibility = Visibility.Collapsed;
			FifthDetail.Visibility = Visibility.Collapsed;
			SixthDetail.Visibility = Visibility.Collapsed;
			FourthDetailAnswer.Visibility = Visibility.Collapsed;
			FifthDetailAnswer.Visibility = Visibility.Collapsed;
			SixthDetailAnswer.Visibility = Visibility.Collapsed;

			switch (ProductsRepository.GetType(product))
			{
				case "CPU":
					{
						Processor temp = (Processor)product;
						FirstDetail.Text = "Series:";
						SecondDetail.Text = "Cores:";
						SecondDetail.ToolTip = "Total number of cores:";
						ThirdDetail.Text = "Brand:";
						FirstDetailAnswer.Text = temp.Series.ToString();
						SecondDetailAnswer.Text = temp.CoreCount.ToString();
						ThirdDetailAnswer.Text = temp.Brand.ToString();
						break;
					}
				case "GPU":
					{
						GraphicsCard temp = (GraphicsCard)product;
						FirstDetail.Text = "Memory type:";
						SecondDetail.Text = "HDMI ports:";
						SecondDetail.ToolTip = "Total number of HDMI ports:";
						ThirdDetail.Text = "Brand:";
						FirstDetailAnswer.Text = temp.MemoryType.ToString();
						SecondDetailAnswer.Text = temp.HDMICount.ToString();
						ThirdDetailAnswer.Text = temp.Brand.ToString();
						break;
					}
				case "RAM":
					{
						Ram temp = (Ram)product;
						FirstDetail.Text = "Memory type:";
						SecondDetail.Text = "Modules:";
						SecondDetail.ToolTip = "Total number of Modules:";
						ThirdDetail.Text = "Module cap:";
						ThirdDetail.ToolTip = "Module capacity:";
						FourthDetail.Text = "Brand:";
						FourthDetail.Visibility = Visibility.Visible;
						FirstDetailAnswer.Text = temp.MemoryType.ToString();
						SecondDetailAnswer.Text = temp.ModuleCount.ToString();
						ThirdDetailAnswer.Text = temp.ModuleCapacity.ToString();
						FourthDetailAnswer.Text = temp.Brand.ToString();
						FourthDetailAnswer.Visibility = Visibility.Visible;
						break;
					}
				case "Motherboard":
					{
						Motherboard temp = (Motherboard)product;
						FirstDetail.Text = "Based on:";
						SecondDetail.Text = "Ram slots:";
						SecondDetail.ToolTip = "Total number of slots dedictaed to RAM:";
						ThirdDetail.Text = "PCI slots:";
						ThirdDetail.ToolTip = "Total number of slots dedictaed to PCI:";
						FifthDetail.Text = "Raid:";
						FifthDetail.ToolTip = "Supported type of raid:";
						SixthDetail.Text = "Brand:";
						FifthDetail.Visibility = Visibility.Visible;
						SixthDetail.Visibility = Visibility.Visible;
						FirstDetailAnswer.Text = temp.BasedOn.ToString();
						SecondDetailAnswer.Text = temp.RAMCount.ToString();
						ThirdDetailAnswer.Text = temp.PCICount.ToString();
						FifthDetailAnswer.Text = temp.RAIDSupport.ToString();
						SixthDetailAnswer.Text = temp.Brand.ToString();
						FifthDetailAnswer.Visibility = Visibility.Visible;
						SixthDetailAnswer.Visibility = Visibility.Visible;
						break;
					}
			}

			DetailsPanel.Visibility = Visibility.Visible;
		}

		private void MakeDetailsReadOnly()
		{
			DetailsProductName.IsReadOnly = true;
			DetailsProductPrice.IsReadOnly = true;
			DetailsProductDiscount.IsReadOnly = true;
			DetailsRating.IsReadOnly = true;
			FirstDetailAnswer.IsReadOnly = true;
			SecondDetailAnswer.IsReadOnly = true;
			ThirdDetailAnswer.IsReadOnly = true;
			FourthDetailAnswer.IsReadOnly = true;
			FifthDetailAnswer.IsReadOnly = true;
			SixthDetailAnswer.IsReadOnly = true;
		}

		private void AddToCartBtn_Click(object sender, RoutedEventArgs e)
		{
			CurrentDetailingProduct.AddedToCartIds_List.Add(CurrentUser.Id);
			ProductsRepository.UpdateProduct(CurrentDetailingProduct);
			DetailsBack_Click(sender, e);
			Temp_Click(sender, e);
		}

		private void DetailsBack_Click(object sender, RoutedEventArgs e)
		{
			DetailsEditError.Visibility = Visibility.Collapsed;
			DetailsPanel.Visibility = Visibility.Collapsed;
			ProductsPanel.Visibility = Visibility.Visible;
			SearchPanel.IsEnabled = true;
			if (DetailsSaveBtn.Visibility == Visibility.Visible)
			{
				MakeDetailsReadOnly();
				AddToCartBtn.IsEnabled = true;
				DetailsEditBtn.Visibility = Visibility.Visible;
				DetailsSaveBtn.Visibility = Visibility.Collapsed;
			}
			else { DetailsEditSuccess.Visibility = Visibility.Collapsed; }
		}

		private void DetailsEditBtn_Click(object sender, RoutedEventArgs e)
		{
			AddToCartBtn.IsEnabled = false;
			DetailsSaveBtn.Visibility = Visibility.Visible;
			DetailsEditBtn.Visibility = Visibility.Collapsed;
			DetailsEditSuccess.Visibility = Visibility.Collapsed;

			DetailsProductName.IsReadOnly = false;
			DetailsProductPrice.IsReadOnly = false;
			DetailsProductDiscount.IsReadOnly = false;
			DetailsRating.IsReadOnly = false;
			FirstDetailAnswer.IsReadOnly = false;
			SecondDetailAnswer.IsReadOnly = false;
			ThirdDetailAnswer.IsReadOnly = false;
			FourthDetailAnswer.IsReadOnly = false;
			FifthDetailAnswer.IsReadOnly = false;
			SixthDetailAnswer.IsReadOnly = false;
		}

		private void DetailsSaveBtn_Click(object sender, RoutedEventArgs e)
		{
			MakeDetailsReadOnly();
			try
			{
				CurrentDetailingProduct.Name = DetailsProductName.Text;
				CurrentDetailingProduct.Price = decimal.Parse(DetailsProductPrice.Text);
				CurrentDetailingProduct.Discount = int.Parse(DetailsProductDiscount.Text);
				CurrentDetailingProduct.Rating = DetailsRating.Value;
				switch(ProductsRepository.GetType(CurrentDetailingProduct))
				{
					case "CPU":
						{
							var t = (Processor)CurrentDetailingProduct;
							t.Series = (ProcessorType)Enum.Parse(typeof(ProcessorType), FirstDetailAnswer.Text);
							t.CoreCount = int.Parse(SecondDetailAnswer.Text);
							t.Brand = (Brand)Enum.Parse(typeof(Brand), ThirdDetailAnswer.Text);
							break;
						}
					case "GPU":
						{
							var t = (GraphicsCard)CurrentDetailingProduct;
							t.MemoryType = (GraphMemType)Enum.Parse(typeof(GraphMemType), FirstDetailAnswer.Text);
							t.HDMICount = int.Parse(SecondDetailAnswer.Text);
							t.Brand = (Brand)Enum.Parse(typeof(Brand), ThirdDetailAnswer.Text);
							break;
						}
					case "RAM":
						{
							var t = (Ram)CurrentDetailingProduct;
							t.MemoryType = (RamMemType)Enum.Parse(typeof(RamMemType), FirstDetailAnswer.Text);
							t.ModuleCount = int.Parse(SecondDetailAnswer.Text);
							t.ModuleCapacity = int.Parse(ThirdDetailAnswer.Text);
							t.Brand = (Brand)Enum.Parse(typeof(Brand), FourthDetailAnswer.Text);
							break;
						}
					case "Motherboard":
						{
							var t = (Motherboard)CurrentDetailingProduct;
							t.BasedOn = (MotherBased)Enum.Parse(typeof(MotherBased), FirstDetailAnswer.Text);
							t.RAMCount = int.Parse(SecondDetailAnswer.Text);
							t.PCICount = int.Parse(ThirdDetailAnswer.Text);
							t.RAIDSupport = (RAID)Enum.Parse(typeof(RAID), FifthDetailAnswer.Text);
							t.Brand = (Brand)Enum.Parse(typeof(Brand), SixthDetailAnswer.Text);
							break;
						}
				}
			}
			catch { DetailsEditError.Visibility = Visibility.Visible; return; }

			AddToCartBtn.IsEnabled = true;
			DetailsEditError.Visibility = Visibility.Collapsed;
			DetailsEditSuccess.Visibility = Visibility.Visible;
			DetailsEditBtn.Visibility = Visibility.Visible;
			DetailsSaveBtn.Visibility = Visibility.Collapsed;
		}
		#endregion

		#region Add new product
		private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
		{
			SearchPanel.IsEnabled = false;
			ProductsPanel.Visibility = Visibility.Collapsed;

			Preview.Children.Clear();

			Product temp = new("name", 0, 10, 0, CurrentUser.Id, 0, Brand.Intel);
			{
				temp.Image = ImagePath;
			}
			NewProductPreview = new(temp, this);
			{
				NewProductPreview.DetailsBtn.IsEnabled = false;
			}
			NewProductPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFFAFF"));
			Preview.Children.Add(NewProductPreview);

			ProductsPanel.Visibility = Visibility.Collapsed;

			AddProductPage.Visibility = Visibility.Visible;
			AddedProductType.Focus();
		}

		private void CheckKeyNum(object sender, System.Windows.Input.KeyEventArgs e)
		{
			e.Handled = !((new System.Text.RegularExpressions.Regex("[0-9]").IsMatch(e.Key.ToString())
				|| e.Key.ToString() == "Escape" || e.Key.ToString() == "Return"));
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

				Product temp = new(name, decimal.Parse(price), int.Parse(discount), RatingBar.Value, CurrentUser.Id, 0, Brand.Intel);
				{
					temp.Image = ImagePath;
				}
				NewProductPreview.SetProperties(temp);
			}
			catch { }
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

		private void ZeroRatingBtn_Click(object sender, RoutedEventArgs e)
		{
			RatingBar.Value = 0;
		}

		private void RatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
		{
			RefreshPreview();
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

			if (key)
			{
				try
				{
					switch (productMode)
					{
						case "CPU":
							{
								if (ImagePath != null)
									ProductsRepository.CreateProcessor(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									int.Parse(CoreCountTextBox.Text), (ProcessorType)Enum.Parse(typeof(ProcessorType), CPUSeries.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), CPUBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateProcessor(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									int.Parse(CoreCountTextBox.Text), (ProcessorType)Enum.Parse(typeof(ProcessorType), CPUSeries.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), CPUBrand.SelectedItem.ToString()));
								break;
							}
						case "GPU":
							{
								if (ImagePath != null)
									ProductsRepository.CreateGraphicsCard(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									int.Parse(GPUHDMICount.Text), (GraphMemType)Enum.Parse(typeof(GraphMemType), GPUMemoryType.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), GPUBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateGraphicsCard(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									int.Parse(GPUHDMICount.Text), (GraphMemType)Enum.Parse(typeof(GraphMemType), GPUMemoryType.SelectedItem.ToString()), (Brand)Enum.Parse(typeof(Brand), GPUBrand.SelectedItem.ToString()));
								break;
							}
						case "RAM":
							{
								if (ImagePath != null)
									ProductsRepository.CreateRam(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									int.Parse(RAMModuleCount.Text), (RamMemType)Enum.Parse(typeof(RamMemType), RAMMemoryType.SelectedItem.ToString()), int.Parse(RAMModuleCopacity.Text), (Brand)Enum.Parse(typeof(Brand), RAMBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateRam(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
									int.Parse(RAMModuleCount.Text), (RamMemType)Enum.Parse(typeof(RamMemType), RAMMemoryType.SelectedItem.ToString()), int.Parse(RAMModuleCopacity.Text), (Brand)Enum.Parse(typeof(Brand), RAMBrand.SelectedItem.ToString()));
								break;
							}
						case "Motherboard":
							{
								if (ImagePath != null)
									ProductsRepository.CreateMotherboard(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, ImagePath, CurrentUser,
									(MotherBased)Enum.Parse(typeof(MotherBased), MotherBasedOn.SelectedItem.ToString()), (RAID)Enum.Parse(typeof(RAID), MotherRaid.SelectedItem.ToString()), int.Parse(RAMSlots.Text), int.Parse(PCISlots.Text), (Brand)Enum.Parse(typeof(Brand), MotherBrand.SelectedItem.ToString()));
								else
									ProductsRepository.CreateMotherboard(NameTextBox.Text, decimal.Parse(PriceTextBox.Text), int.Parse(DiscountTextBox.Text), RatingBar.Value, CurrentUser,
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
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Adding product failed", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void CancelAddProductBtn_Click(object sender, RoutedEventArgs e)
		{
			AddError.Text = "";
			AddProductPage.Visibility = Visibility.Hidden;
			SearchPanel.IsEnabled = true;
			ProductsPanel.Visibility = Visibility.Visible;
		}
		#endregion

		#endregion

		#region Cart
		private void RefreshCartPanel()
		{
			decimal totalPrice = 0;
			decimal totalDiscount = 0;
			decimal finalTotalCharge = 0;

			CartWrapPanel.Children.Clear();
			BalanceError.Visibility = Visibility.Collapsed;

			if (CurrentUser.Balance < 0)
			{
				BalanceTextBlock.Text = "∞";
				BalanceTextBlock.FontSize = 15;
				BalanceTextBlock.Margin = new Thickness(0, 2.5, 10, 0);
			}
			else
				BalanceTextBlock.Text = CurrentUser.Balance.ToString() + "$";

			foreach (var x in ProductsRepository.Processor_List)
			{
				if (ProductsRepository.IsAddedToCart(x, CurrentUser))
				{
					CartWrapPanel.Children.Add(new ProductView(x, this));
					totalPrice += x.Price;
					totalDiscount += x.Price * x.Discount / 100;
				}
			}
			foreach (var x in ProductsRepository.GraphicsCard_List)
			{
				if (ProductsRepository.IsAddedToCart(x, CurrentUser))
				{
					CartWrapPanel.Children.Add(new ProductView(x, this));
					totalPrice += x.Price;
					totalDiscount += x.Price * x.Discount / 100;
				}
			}
			foreach (var x in ProductsRepository.Ram_List)
			{
				if (ProductsRepository.IsAddedToCart(x, CurrentUser))
				{
					CartWrapPanel.Children.Add(new ProductView(x, this));
					totalPrice += x.Price;
					totalDiscount += x.Price * x.Discount / 100;
				}
			}
			foreach (var x in ProductsRepository.Motherboard_List)
			{
				if (ProductsRepository.IsAddedToCart(x, CurrentUser))
				{
					CartWrapPanel.Children.Add(new ProductView(x, this));
					totalPrice += x.Price;
					totalDiscount += x.Price * x.Discount / 100;
				}
			}
			if (CartWrapPanel.Children.Count == 0)
				ConfirmPurchaseBtn.IsEnabled = false;
			else
				ConfirmPurchaseBtn.IsEnabled = true;

			finalTotalCharge = totalPrice - totalDiscount;
			TotalCartCharge = finalTotalCharge;
			TotalPriceTextBlock.Text = Math.Round(totalPrice, 2) + "$";
			TotalPriceTextBlock.ToolTip = totalPrice.ToString() + "$";
			DiscountTextBlock.Text = Math.Round(totalDiscount, 2).ToString() + "$";
			DiscountTextBlock.ToolTip = totalDiscount.ToString() + "$";
			FinalChargeTextBlock.Text = Math.Round(finalTotalCharge, 2).ToString() + "$";
			FinalChargeTextBlock.ToolTip = finalTotalCharge.ToString() + "$";
		}

		public void RemoveProductFromCart(Product product)
		{
			product.AddedToCartIds_List.Remove(CurrentUser.Id);
			ProductsRepository.UpdateProduct(product);
			RefreshCartPanel();
		}

		private void ConfirmPurchaseBtn_Click(object sender, RoutedEventArgs e)
		{
			if (CurrentUser.Balance >= 0 && CurrentUser.Balance < TotalCartCharge)
				BalanceError.Visibility = Visibility.Visible;
			else
			{
				ProductsRepository.ClearUserCart(CurrentUser);
				if (CurrentUser.Balance >= 0)
					CurrentUser.Balance -= TotalCartCharge;
				RefreshCartPanel();
				MessageBox.Show("Purchase successful", "Successful purchase", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
			}
		}
		#endregion

		#region Account

		#endregion

		#region Manage

		#endregion
	}
}