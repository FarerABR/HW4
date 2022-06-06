using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Repository;
using DAL.Entity.Product;

namespace UI.Views
{
	public partial class ProductView : UserControl
	{
		public ProductView(Ram ram)
		{
			InitializeComponent();
			NameTextBlock.Text = ram.Name;
			NameTextBlock.ToolTip = ram.Name;
			RatingBar.Value = ram.Rating;
			PriceTextBlock.Text = ram.Price;
			OffTextBlock.Text = ram.Discount;
			SetImageSource(ram);
		}

		private void SetImageSource(Ram ram)
		{
			var uriSource = new Uri(ram.Image, UriKind.Relative);
			BitmapImage Path = new(uriSource);
			Image.Source = Path;
		}

		private void DetailsBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
