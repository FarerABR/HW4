using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DAL.Entity.Product;

namespace UI.Views
{
	public partial class ProductView : UserControl
	{
		public ProductView(Product product)
		{
			InitializeComponent();
			NameTextBlock.Text = product.Name;
			NameTextBlock.ToolTip = product.Name;
			RatingBar.Value = product.Rating;
			PriceTextBlock.Text = product.Price;
			OffTextBlock.Text = product.Discount;
			SetImageSource(product);

			if (product.Discount == "0%")
				OffBorder.Visibility = Visibility.Collapsed;
			else if (product.Discount == "100%")
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

		}
	}
}
