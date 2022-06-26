using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DAL.Entity.Product;
using BLL.Repository;

namespace UI.Views
{
	public partial class ProductView : UserControl
	{
		#region Properties
		private readonly StoreView _storeView;
		private readonly Product _product;
		#endregion

		public ProductView(Product product, StoreView storeView)
		{
			InitializeComponent();
			_product = product;
			_storeView = storeView;
			SetProperties(product);
		}

		public void SetProperties(Product product)
		{
			NameTextBlock.Text = product.Name;
			NameTextBlock.ToolTip = product.Name;
			RatingBar.Value = product.Rating;
			RatingBar.ToolTip = "Rating: " + product.Rating.ToString();
			double x = (int)(product.Price * 100); x /= 100;
			PriceTextBlock.Text = x.ToString() + "$";
			PriceTextBlock.ToolTip = product.Price.ToString() + "$";
			OffTextBlock.Text = product.Discount.ToString() + "%";

			if (ProductsRepository.IsAddedToCart(product, _storeView.CurrentUser))
			{
				DetailsBtn.Visibility = Visibility.Collapsed;
				RemoveFromCartBtn.Visibility = Visibility.Visible;
			}

			if (product.Image != null)
				SetImageSource(product.Image);
			else
				SetImageSource("../Images/Product.jpg");

			if (product.Discount == 0)
				OffBorder.Visibility = Visibility.Collapsed;
			else if (product.Discount == 100)
			{
				OffTextBlock.Text = "Free";
				OffBorder.Visibility = Visibility.Visible;
			}
			else
				OffBorder.Visibility = Visibility.Visible;

			if(product.ViewStatus == DAL.Enum.Product.ViewStatus.deleted)
				DeletedBorder.Visibility = Visibility.Visible;
			else
				DeletedBorder.Visibility = Visibility.Collapsed;
		}

		private void SetImageSource(string image)
		{
			Uri uriSource = new(image, UriKind.RelativeOrAbsolute);
			BitmapImage Path = new(uriSource);
			Image.Source = Path;
		}

		private void DetailsBtn_Click(object sender, RoutedEventArgs e)
		{
			_storeView.ShowProductDetails(_product);
		}

		private void RemoveFromCartBtn_Click(object sender, RoutedEventArgs e)
		{
			_storeView.RemoveProductFromCart(_product);
		}
	}
}
