using DAL.Entity;
using DAL.Enum.User;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Views
{
	public partial class UserView : UserControl
	{
		public UserView(User user)
		{
			InitializeComponent();

			switch(user.Role)
			{
				case UserRole.admin:
					{
						MainGrid.Background = Brushes.Red;
						break;
					}
				case UserRole.moderator:
					{
						MainGrid.Background = Brushes.Blue;
						break;
					}
				case UserRole.customer:
					{
						MainGrid.Background = Brushes.Green;
						break;
					}
			}

			Username.Text = user.Username;
			Username.ToolTip = user.Username;
			Id.Text = user.Id.ToString();
			Role.Text = user.Role.ToString();

			if(user.IsDeleted)
			{
				IsDeletedIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountCancel;
				IsDeletedTextBox.Text = "Deleted";
			}
			else
			{
				IsDeletedIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountBadge;
				IsDeletedTextBox.Text = "Active";
			}
		}
	}
}
