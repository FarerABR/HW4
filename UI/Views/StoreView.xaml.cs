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
using System.Windows.Controls;

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
		private string? ActionTypeOfFind = null;
		#endregion

		public StoreView(User currentUser)
		{
			InitializeComponent();
			CurrentUser = currentUser;
		}

		private void StoreWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (CurrentUser.Role == UserRole.customer)
			{
				ManageSep.Visibility = Visibility.Collapsed;
				ManageBtn.Visibility = Visibility.Collapsed;
				AddNewProductBtn.Visibility = Visibility.Hidden;
				DetailsEditBtn.Visibility = Visibility.Collapsed;
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

			AccountGenderSelect.ItemsSource = Enum.GetValues(typeof(UserGender));
		}

		private void StoreWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Data_Access.WriteAllData();
		}

		private void CheckKeyNum(object? sender, TextChangedEventArgs e)
		{
			var ThisTextBox = sender as TextBox;
			if (!string.IsNullOrEmpty(ThisTextBox.Text))
			{
				int CaretIndex = ThisTextBox.CaretIndex;
				for (int i = 0; i < ThisTextBox.Text.Length; i++)
				{
					if (ThisTextBox.Text[i] is not (>= '0' and <= '9'))
					{
						ThisTextBox.Text = ThisTextBox.Text.Remove(i, 1);
						if (i <= CaretIndex)
							CaretIndex--;
					}
				}
				ThisTextBox.CaretIndex = CaretIndex;
			}
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
			ChangePassBtn.Visibility = AccountEditBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			AccountEditBack.Visibility = AccountEditBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			AccountEditCorrect.Visibility = Visibility.Collapsed;
			AccountEditWrong.Visibility = Visibility.Collapsed;
			RefreshAccountPage();
			AccountPage.Visibility = Visibility.Visible;
		}

		private void ManageBtn_Click(object sender, RoutedEventArgs e)
		{
			ButtonsRefresh();
			PagesRefresh();
			ManageBtn.Background = Brushes.DeepSkyBlue;
			ManageBtn.IsEnabled = false;
			ManageText.Visibility = Visibility.Collapsed;
			RefreshManagePage();
			ManagePage.Visibility = Visibility.Visible;
			ManageAction.Focus();
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
		private void AddProducts(string types, string search)
		{
			ProductsWrapPanel.Children.Clear();

			if (types.Contains('1'))
				foreach (var x in ProductsRepository.Processor_List)
				{
					if ((CurrentUser.Role != UserRole.customer || x.ViewStatus != ViewStatus.deleted) &&
					!ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('2'))
				foreach (var x in ProductsRepository.GraphicsCard_List)
				{
					if ((CurrentUser.Role != UserRole.customer || x.ViewStatus != ViewStatus.deleted) &&
					!ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('3'))
				foreach (var x in ProductsRepository.Ram_List)
				{
					if ((CurrentUser.Role != UserRole.customer || x.ViewStatus != ViewStatus.deleted) &&
					!ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}

			if (types.Contains('4'))
				foreach (var x in ProductsRepository.Motherboard_List)
				{
					if ((CurrentUser.Role != UserRole.customer || x.ViewStatus != ViewStatus.deleted) &&
					!ProductsRepository.IsAddedToCart(x, CurrentUser) && x.Name.ToLower().Contains(search.ToLower()))
					{
						ProductView productView = new(x, this);
						ProductsWrapPanel.Children.Add(productView);
					}
				}
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

		#region Product details
		private void CheckEmptyTextBox(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(DetailsProductName.Text) || DetailsProductName.Text[0] != ' ')
			{
				DetailsProductName.Text = " " + DetailsProductName.Text;
				DetailsProductName.Focus();
				DetailsProductName.CaretIndex = DetailsProductName.Text.Length;
			}
			if (string.IsNullOrEmpty(DetailsProductPrice.Text) || DetailsProductPrice.Text[0] != ' ')
			{
				DetailsProductPrice.Text = " " + DetailsProductPrice.Text;
				DetailsProductPrice.Focus();
				DetailsProductPrice.CaretIndex = DetailsProductPrice.Text.Length;
			}
			if (string.IsNullOrEmpty(DetailsProductDiscount.Text) || DetailsProductDiscount.Text[0] != ' ')
			{
				DetailsProductDiscount.Text = " " + DetailsProductDiscount.Text;
				DetailsProductDiscount.Focus();
				DetailsProductDiscount.CaretIndex = DetailsProductDiscount.Text.Length;
			}
			if (string.IsNullOrEmpty(FirstDetailAnswer.Text) || FirstDetailAnswer.Text[0] != ' ')
			{
				FirstDetailAnswer.Text = " " + FirstDetailAnswer.Text;
				FirstDetailAnswer.Focus();
				FirstDetailAnswer.CaretIndex = FirstDetailAnswer.Text.Length;
			}
			if (string.IsNullOrEmpty(SecondDetailAnswer.Text) || SecondDetailAnswer.Text[0] != ' ')
			{
				SecondDetailAnswer.Text = " " + SecondDetailAnswer.Text;
				SecondDetailAnswer.Focus();
				SecondDetailAnswer.CaretIndex = SecondDetailAnswer.Text.Length;
			}
			if (string.IsNullOrEmpty(ThirdDetailAnswer.Text) || ThirdDetailAnswer.Text[0] != ' ')
			{
				ThirdDetailAnswer.Text = " " + ThirdDetailAnswer.Text;
				ThirdDetailAnswer.Focus();
				ThirdDetailAnswer.CaretIndex = ThirdDetailAnswer.Text.Length;
			}
			if (string.IsNullOrEmpty(FourthDetailAnswer.Text) || FourthDetailAnswer.Text[0] != ' ')
			{
				FourthDetailAnswer.Text = " " + FourthDetailAnswer.Text;
				FourthDetailAnswer.Focus();
				FourthDetailAnswer.CaretIndex = FourthDetailAnswer.Text.Length;
			}
			if (string.IsNullOrEmpty(FifthDetailAnswer.Text) || FifthDetailAnswer.Text[0] != ' ')
			{
				FifthDetailAnswer.Text = " " + FifthDetailAnswer.Text;
				FifthDetailAnswer.Focus();
				FifthDetailAnswer.CaretIndex = FifthDetailAnswer.Text.Length;
			}
			if (string.IsNullOrEmpty(SixthDetailAnswer.Text) || SixthDetailAnswer.Text[0] != ' ')
			{
				SixthDetailAnswer.Text = " " + SixthDetailAnswer.Text;
				SixthDetailAnswer.Focus();
				SixthDetailAnswer.CaretIndex = SixthDetailAnswer.Text.Length;
			}
		}

		public void ShowProductDetails(Product product)
		{
			CurrentDetailingProduct = product;
			ProductsPanel.Visibility = Visibility.Collapsed;
			SearchPanel.IsEnabled = false;
			DetailsEditError.Visibility = Visibility.Collapsed;
			DetailsEditSuccess.Visibility = Visibility.Collapsed;
			DetailsDeleteBtn.Visibility = Visibility.Collapsed;
			DetailsProductVisibility.Visibility = Visibility.Collapsed;

			DetailsDeleteBtn.IsChecked = (product.ViewStatus == ViewStatus.deleted)? true: false;
			DetailsDeleteBtn_Click(new object(), new RoutedEventArgs());

			DetailsProductName.Text = " " + product.Name;
			DetailsProductName.ToolTip = product.Name;
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
						FirstDetailAnswer.Text = " " + temp.Series.ToString();
						SecondDetailAnswer.Text = " " + temp.CoreCount.ToString();
						ThirdDetailAnswer.Text = " " + temp.Brand.ToString();
						break;
					}
				case "GPU":
					{
						GraphicsCard temp = (GraphicsCard)product;
						FirstDetail.Text = "Memory type:";
						SecondDetail.Text = "HDMI ports:";
						SecondDetail.ToolTip = "Total number of HDMI ports:";
						ThirdDetail.Text = "Brand:";
						FirstDetailAnswer.Text = " " + temp.MemoryType.ToString();
						SecondDetailAnswer.Text = " " + temp.HDMICount.ToString();
						ThirdDetailAnswer.Text = " " + temp.Brand.ToString();
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
						FirstDetailAnswer.Text = " " + temp.MemoryType.ToString();
						SecondDetailAnswer.Text = " " + temp.ModuleCount.ToString();
						ThirdDetailAnswer.Text = " " + temp.ModuleCapacity.ToString();
						FourthDetailAnswer.Text = " " + temp.Brand.ToString();
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
						FirstDetailAnswer.Text = " " + temp.BasedOn.ToString();
						SecondDetailAnswer.Text = " " + temp.RAMCount.ToString();
						ThirdDetailAnswer.Text = " " + temp.PCICount.ToString();
						FifthDetailAnswer.Text = " " + temp.RAIDSupport.ToString();
						SixthDetailAnswer.Text = " " + temp.Brand.ToString();
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

		private void DetailsEditBtn_Click(object sender, RoutedEventArgs e)
		{
			AddToCartBtn.IsEnabled = false;
			DetailsSaveBtn.Visibility = Visibility.Visible;
			DetailsEditBtn.Visibility = Visibility.Collapsed;
			DetailsEditSuccess.Visibility = Visibility.Collapsed;
			DetailsDeleteBtn.Visibility = Visibility.Visible;
			DetailsProductVisibility.Visibility = Visibility.Visible;

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

		private void DetailsDeleteBtn_Click(object sender, RoutedEventArgs e)
		{
			if ((bool)DetailsDeleteBtn.IsChecked)
			{
				DetailsProductVisibility.Text = "deleted";
				DetailsProductVisibility.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF6B00"));
			}
			else
			{
				DetailsProductVisibility.Text = "visible";
				DetailsProductVisibility.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00C018"));
			}
		}

		private void DetailsSaveBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				switch (ProductsRepository.GetType(CurrentDetailingProduct))
				{
					case "CPU":
						{
							var t = CurrentDetailingProduct as Processor;
							t.Series = (ProcessorType)Enum.Parse(typeof(ProcessorType), FirstDetailAnswer.Text.Trim());
							t.CoreCount = int.Parse(SecondDetailAnswer.Text.Trim());
							t.Brand = (Brand)Enum.Parse(typeof(Brand), ThirdDetailAnswer.Text.Trim());
							break;
						}
					case "GPU":
						{
							var t = CurrentDetailingProduct as GraphicsCard;
							t.MemoryType = (GraphMemType)Enum.Parse(typeof(GraphMemType), FirstDetailAnswer.Text.Trim());
							t.HDMICount = int.Parse(SecondDetailAnswer.Text.Trim());
							t.Brand = (Brand)Enum.Parse(typeof(Brand), ThirdDetailAnswer.Text.Trim());
							break;
						}
					case "RAM":
						{
							var t = CurrentDetailingProduct as Ram;
							t.MemoryType = (RamMemType)Enum.Parse(typeof(RamMemType), FirstDetailAnswer.Text.Trim());
							t.ModuleCount = int.Parse(SecondDetailAnswer.Text.Trim());
							t.ModuleCapacity = int.Parse(ThirdDetailAnswer.Text.Trim());
							t.Brand = (Brand)Enum.Parse(typeof(Brand), FourthDetailAnswer.Text.Trim());
							break;
						}
					case "Motherboard":
						{
							var t = CurrentDetailingProduct as Motherboard;
							t.BasedOn = (MotherBased)Enum.Parse(typeof(MotherBased), FirstDetailAnswer.Text.Trim());
							t.RAMCount = int.Parse(SecondDetailAnswer.Text.Trim());
							t.PCICount = int.Parse(ThirdDetailAnswer.Text.Trim());
							t.RAIDSupport = (RAID)Enum.Parse(typeof(RAID), FifthDetailAnswer.Text.Trim());
							t.Brand = (Brand)Enum.Parse(typeof(Brand), SixthDetailAnswer.Text.Trim());
							break;
						}
				}
				MakeDetailsReadOnly();
				CurrentDetailingProduct.Name = DetailsProductName.Text.Trim();
				CurrentDetailingProduct.Price = decimal.Parse(DetailsProductPrice.Text.Trim());
				CurrentDetailingProduct.Discount = int.Parse(DetailsProductDiscount.Text.Trim());
				CurrentDetailingProduct.Rating = DetailsRating.Value;
				CurrentDetailingProduct.ViewStatus = ((bool)DetailsDeleteBtn.IsChecked) ?
				ViewStatus.deleted : ViewStatus.visible;
			}
			catch { DetailsEditError.Visibility = Visibility.Visible; return; }

			AddToCartBtn.IsEnabled = true;
			DetailsEditError.Visibility = Visibility.Collapsed;
			DetailsEditSuccess.Visibility = Visibility.Visible;
			DetailsDeleteBtn.Visibility = Visibility.Collapsed;
			DetailsProductVisibility.Visibility = Visibility.Collapsed;
			DetailsEditBtn.Visibility = Visibility.Visible;
			DetailsSaveBtn.Visibility = Visibility.Collapsed;
		}

		private void DetailsBack_Click(object sender, RoutedEventArgs e)
		{
			DetailsEditError.Visibility = Visibility.Collapsed;
			DetailsPanel.Visibility = Visibility.Collapsed;
			Temp_Click(sender, e);
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
		#endregion

		#region Add new product
		private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
		{
			if(CPUParticularInfo.Visibility != Visibility.Visible && GPUParticularInfo.Visibility != Visibility.Visible && RAMParticularInfo.Visibility != Visibility.Visible && MotherParticularInfo.Visibility != Visibility.Visible)
			{
				AddedProductType.SelectedIndex = 0;
				CPUParticularInfo.Visibility = Visibility.Visible;
			}
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
			MotherRaid.SelectedItem = null;

			AddedProductType.SelectedIndex = 0;
			CPUParticularInfo.Visibility = Visibility.Visible;
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
			if (DiscountError.Visibility != Visibility.Visible)
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
					ZeroRatingBtn_Click(sender, e);
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
			decimal finalTotalCharge;

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

			double t = (int)(totalPrice * 100); t /= 100;
			TotalPriceTextBlock.Text = t.ToString() + "$";
			TotalPriceTextBlock.ToolTip = totalPrice.ToString() + "$";

			t = (int)(totalDiscount * 100); t /= 100;
			DiscountTextBlock.Text = t.ToString() + "$";
			DiscountTextBlock.ToolTip = totalDiscount.ToString() + "$";

			t = (int)(finalTotalCharge * 100); t /= 100;
			FinalChargeTextBlock.Text = t.ToString() + "$";
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
		private void RefreshAccountPage()
		{
			AccountUsername.Text = CurrentUser.Username;
			AccountUsername.ToolTip = AccountUsername.Text;
			AccountEmail.Text = CurrentUser.Email;
			AccountEmail.ToolTip = CurrentUser.Email;
			AccountRole.Text = CurrentUser.Role.ToString();
			AccountID.Text = CurrentUser.Id.ToString();
			AccountFirstName.Text = CurrentUser.FirstName;
			AccountFirstName.ToolTip = AccountFirstName.Text;
			AccountLastName.Text = CurrentUser.LastName;
			AccountLastName.ToolTip = AccountLastName.Text;
			AccountGender.Text = CurrentUser.Gender.ToString();
			double x = (int)(CurrentUser.Balance * 100); x /= 100;
			AccountBalance.Text = x.ToString();
			AccountBalance.ToolTip = CurrentUser.Balance.ToString();
			AccountCreatedOn.Text = CurrentUser.Date_Created.ToLongDateString()
			+ "\n" + CurrentUser.Date_Created.ToLongTimeString();
			AccountCreatedOn.ToolTip = AccountCreatedOn.Text;

			AccountUsernameError.Visibility = Visibility.Collapsed;
			AccountEmailError.Visibility = Visibility.Collapsed;
		}

		private void AccountEditBtn_Click(object sender, RoutedEventArgs e)
		{
			AccountUsername.IsReadOnly = false;
			AccountEmail.IsReadOnly = false;
			AccountFirstName.IsReadOnly = false;
			AccountLastName.IsReadOnly = false;

			AccountEditCorrect.Visibility = Visibility.Collapsed;
			AccountGender.Visibility = Visibility.Collapsed;
			AccountGenderSelect.SelectedItem = CurrentUser.Gender;
			AccountGenderSelect.Visibility = Visibility.Visible;
			ChangePassBtn.Visibility = Visibility.Visible;
			AccountSaveBtn.Visibility = Visibility.Visible;
			AccountEditBtn.Visibility = Visibility.Collapsed;
			AccountEditBack.Visibility = Visibility.Visible;
		}

		private void ChangePassBtn_Click(object sender, RoutedEventArgs e)
		{
			CurrPassError.Visibility = Visibility.Collapsed;
			NewPassError.Visibility = Visibility.Collapsed;
			NewPassConfirmError.Visibility = Visibility.Collapsed;
			MainAccountGrid.IsEnabled = false;
			MainAccountGrid.Background = Brushes.Silver;
			ChangePasswordPage.Visibility = Visibility.Visible;
			CurrPassBox.Focus();
		}

		private void ChPassShowHideBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowHideIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff;
			CurrPassBlock.Text = CurrPassBox.Password;
			if (CurrPassBlock.Text == string.Empty)
				CurrPassBlock.Opacity = 0;
			CurrPassBlock.Visibility = Visibility.Visible;
		}

		private void ChPassShowHideBtn_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ShowHideIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
			CurrPassBlock.Opacity = 1;
			CurrPassBlock.Visibility = Visibility.Hidden;
		}

		private void ChPassBtn_Click(object sender, RoutedEventArgs e)
		{
			bool result = true;
			if (!PasswordRepository.CheckPassword(CurrentUser.Username, CurrPassBox.Password))
			{
				CurrPassError.Visibility = Visibility.Visible;
				result = false;
			}
			else CurrPassError.Visibility = Visibility.Collapsed;

			if (NewPassBox.Password.Length < 5)
			{
				NewPassError.Text = "* at least 5 characters";
				NewPassError.Foreground = Brushes.Red;
				NewPassError.Opacity = 0.5;
				result = false;
			}
			else
			{
				NewPassError.Text = "at least 5 characters";
				NewPassError.Foreground = Brushes.Black;
				NewPassError.Opacity = 0.5;
			}

			if (NewPassBox.Password != NewPassConfirmBox.Password)
			{
				NewPassConfirmError.Visibility = Visibility.Visible;
				result = false;
			}
			else NewPassConfirmError.Visibility = Visibility.Collapsed;

			if (result)
			{
				UserRepository.UpdatePassword(CurrentUser, NewPassBox.Password);
				ChPassCancelBtn_Click(sender, e);
			}
		}

		private void ChPassCancelBtn_Click(object sender, RoutedEventArgs e)
		{
			MainAccountGrid.IsEnabled = true;
			MainAccountGrid.Background = null;
			ChangePasswordPage.Visibility = Visibility.Hidden;
			CurrPassError.Visibility = Visibility.Collapsed;
			NewPassError.Visibility = Visibility.Collapsed;
			NewPassConfirmError.Visibility = Visibility.Collapsed;
			CurrPassBox.Password = "";
			CurrPassBlock.Text = "";
			NewPassBox.Password = "";
			NewPassConfirmBox.Password = "";
		}

		private void AccountEditBack_Click(object sender, RoutedEventArgs e)
		{
			AccountUsername.IsReadOnly = true;
			AccountEmail.IsReadOnly = true;
			AccountFirstName.IsReadOnly = true;
			AccountLastName.IsReadOnly = true;

			AccountEditWrong.Visibility = Visibility.Collapsed;
			AccountGenderSelect.Visibility = Visibility.Collapsed;
			AccountGender.Visibility = Visibility.Visible;
			ChangePassBtn.Visibility = Visibility.Collapsed;
			AccountSaveBtn.Visibility = Visibility.Collapsed;
			AccountEditBtn.Visibility = Visibility.Visible;
			AccountEditBack.Visibility = Visibility.Collapsed;
		}

		private void AccountSaveBtn_Click(object sender, RoutedEventArgs e)
		{
			bool res = true;
			try
			{
				if (AccountUsername.Text.Length < 3 || AccountUsername.Text.Length > 15)
				{
					AccountUsernameError.Visibility = Visibility.Visible;
					res = false;
				}
				else AccountUsernameError.Visibility = Visibility.Collapsed;
				CurrentUser.Username = AccountUsername.Text;

				if (!ShellView.ValidEmail(AccountEmail.Text.Trim()))
				{
					AccountEmailError.Visibility = Visibility.Visible;
					res = false;
				}
				else AccountEmailError.Visibility = Visibility.Collapsed;

				if(!res)
					throw new Exception();

				CurrentUser.Email = AccountEmail.Text;

				if (AccountFirstName.Text.Length == 0)
					AccountFirstName.Text = "not set";
				CurrentUser.FirstName = AccountFirstName.Text;

				if (AccountLastName.Text.Length == 0)
					AccountLastName.Text = "not set";
				CurrentUser.LastName = AccountLastName.Text;
				CurrentUser.Gender = (UserGender)Enum.Parse(typeof(UserGender), AccountGenderSelect.SelectedItem.ToString());

				AccountEditWrong.Visibility = Visibility.Collapsed;
				AccountEditCorrect.Visibility = Visibility.Visible;
			}
			catch { AccountEditWrong.Visibility = Visibility.Visible; return; }

			AccountUsername.IsReadOnly = true;
			AccountEmail.IsReadOnly = true;
			AccountFirstName.IsReadOnly = true;
			AccountLastName.IsReadOnly = true;

			AccountGenderSelect.Visibility = Visibility.Collapsed;
			AccountGender.Visibility = Visibility.Visible;
			ChangePassBtn.Visibility = Visibility.Collapsed;
			AccountEditBack.Visibility = Visibility.Collapsed;
			AccountSaveBtn.Visibility = Visibility.Collapsed;
			AccountEditBtn.Visibility = Visibility.Visible;
			AccountEditBtn.Focus();
			UsernameTextBlock.Text = CurrentUser.Username;
			UsernameTextBlock.ToolTip = CurrentUser.Username;

			RefreshAccountPage();
		}

		private void AccountFirstName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(AccountFirstName.Text) && AccountFirstName.Text != "not set" && AccountFirstName.Text != "First Name here")
			{
				int CaretIndex = AccountFirstName.CaretIndex;
				for (int i = 0; i < AccountFirstName.Text.Length; i++)
				{
					if (AccountFirstName.Text[i] is not (>= 'a' and <= 'z' or >= 'A' and <= 'Z'))
					{
						AccountFirstName.Text = AccountFirstName.Text.Remove(i, 1);
						if(i <= CaretIndex)
							CaretIndex--;
					}
				}
				AccountFirstName.CaretIndex = CaretIndex;
			}
		}

		private void AccountLastName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(AccountLastName.Text) && AccountLastName.Text != "not set" && AccountLastName.Text != "First Name here")
			{
				int CaretIndex = AccountLastName.CaretIndex;
				for (int i = 0; i < AccountLastName.Text.Length; i++)
				{
					if (AccountLastName.Text[i] is not (>= 'a' and <= 'z' or >= 'A' and <= 'Z'))
					{
						AccountLastName.Text = AccountLastName.Text.Remove(i, 1);
						if (i <= CaretIndex)
							CaretIndex--;
					}
				}
				AccountLastName.CaretIndex = CaretIndex;
			}
		}
		#endregion

		#region Manage
		private void RefreshManagePage()
		{
			ManageUsersWrapPanel.Children.Clear();
			foreach(var x in UserRepository.User_List)
			{
				if(x.Role == UserRole.admin)
				{
					UserView temp = new(x);
					ManageUsersWrapPanel.Children.Add(temp);
				}

				else if(x.Role == UserRole.moderator)
				{
					UserView temp = new(x);
					ManageUsersWrapPanel.Children.Add(temp);
				}

				else
				{
					UserView temp = new(x);
					ManageUsersWrapPanel.Children.Add(temp);
				}
			}
		}

		private void ManageUsernameOrId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			try
			{
				switch (ManageUsernameOrId.SelectedItem.ToString().Contains("ID"))
				{
					case true:
						{
							if(ManageUsername != null)
							{
								ManageUsername.Visibility = Visibility.Collapsed;
								ManageUserId.Visibility = Visibility.Visible;
							}
							ActionTypeOfFind = "ID";
							break;
						}
					case false:
						{
							if (ManageUsername != null)
							{
								ManageUserId.Visibility = Visibility.Collapsed;
								ManageUsername.Visibility = Visibility.Visible;
							}
							ActionTypeOfFind = "Username";
							break;
						}
				}
			}
			catch { }
		}

		private void ManageOkBtn_Click(object sender, RoutedEventArgs e)
		{
			bool res = true;
			if(ManageAction.SelectedIndex == -1)
			{
				ManageText.Text = "* select action";
				ManageText.Foreground = Brushes.Red;
				ManageText.Visibility = Visibility.Visible;
				return;
			}
			if (ManageUsername.Text == null)
				ManageUsername.Text = "";

			User? temp = null;
			switch(ActionTypeOfFind)
			{
				case "Username":
					{
						var x = UserRepository.SearchUser(ManageUsername.Text);
						if (x == null)
						{
							ManageText.Text = "* user not found";
							ManageText.Foreground = Brushes.Red;
							ManageText.Visibility = Visibility.Visible;
							return;
						}
						else
							temp = x;
						break;
					}
				case "ID":
					{
						User? x = null;
						if(!(string.IsNullOrEmpty(ManageUserId.Text) || int.Parse(ManageUserId.Text) > 65535))
							x = UserRepository.GetUserById(ushort.Parse(ManageUserId.Text));

						if (x == null)
						{
							ManageText.Text = "* user not found";
							ManageText.Foreground = Brushes.Red;
							ManageText.Visibility = Visibility.Visible;
							return;
						}
						else
							temp = x;
						break;
					}
			}

			try
			{
				switch (ManageAction.SelectedIndex.ToString())
				{
					case "0":
						{
							if(temp.IsDeleted)
							{
								ManageText.Text = "* this user is deleted";
								ManageText.Foreground = Brushes.Red;
								ManageText.Visibility = Visibility.Visible;
								res = false;
								break;
							}

							if (!UserRepository.PromoteUser(CurrentUser, temp))
							{
								var Result = MessageBox.Show("You are about to transfer the adminship to another user!\nContinue?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
								switch (Result)
								{
									case MessageBoxResult.Yes:
										{
											UserRepository.TransferAdmin(CurrentUser, temp);
											LogOutBtn_Click(sender, e);
											break;
										}
									default:
										{
											ManageText.Visibility = Visibility.Collapsed;
											res = false;
											break;
										}
								}
							}
							break;
						}

					case "1":
						{
							if (temp.IsDeleted)
							{
								ManageText.Text = "* this user is deleted";
								ManageText.Foreground = Brushes.Red;
								ManageText.Visibility = Visibility.Visible;
								res = false;
							}
							else
								UserRepository.DemoteUser(CurrentUser, temp);
							break;
						}

					case "2":
						{
							if (temp.IsDeleted)
							{
								ManageText.Text = "* this user is already deleted";
								ManageText.Foreground = Brushes.Red;
								ManageText.Visibility = Visibility.Visible;
								res = false;
							}

							if (!UserRepository.DeleteUser(CurrentUser, temp.Id))
							{
								var Result = MessageBox.Show("You are about to delete yuor account!\nContinue?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
								switch (Result)
								{
									case MessageBoxResult.Yes:
										{
											UserRepository.SelfDelete(temp);
											LogOutBtn_Click(sender, e);
											break;
										}
									default:
										{
											ManageText.Visibility = Visibility.Collapsed;
											res = false;
											break;
										}
								}
							}
							break;
						}

					case "3":
						{
							if (!UserRepository.RestoreUser(CurrentUser, temp.Id))
							{
								ManageText.Text = "* this user is not deleted";
								ManageText.Foreground = Brushes.Red;
								ManageText.Visibility = Visibility.Visible;
								res = false;
							}
							break;
						}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Invalid action", MessageBoxButton.OK, MessageBoxImage.Error);
				res = false;
			}

			if (res)
			{
				ManageText.Text = "🗸 action completed";
				ManageText.Foreground = Brushes.Green;
				ManageText.Visibility = Visibility.Visible;

				RefreshManagePage();
			}
		}
		#endregion
	}
}