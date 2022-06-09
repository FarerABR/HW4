using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DAL.Entity.Product;

namespace UI.Views
{
	public partial class ProductView : UserControl
	{
		private readonly Product _product;

		public ProductView(Product product)
		{
			InitializeComponent();
			_product = product;
			NameTextBlock.Text = product.Name;
			NameTextBlock.ToolTip = product.Name;
			RatingBar.Value = product.Rating;
			PriceTextBlock.Text = product.Price.ToString() + "$";
			OffTextBlock.Text = product.Discount.ToString() + "%";
			SetImageSource(product);

			if (product.Discount == 0)
				OffBorder.Visibility = Visibility.Collapsed;
			else if (product.Discount == 100)
				OffTextBlock.Text = "Free";
		}

		private void SetImageSource(Product product)
		{
			var uriSource = new Uri(product.Image, UriKind.Relative);
			BitmapImage Path = new(uriSource);
			Image.Source = Path;
		}

		private void DetailsBtn_Click(object sender, RoutedEventArgs e)
		{
			StoreView.ShowProductDetails(_product);
		}
	}
}
