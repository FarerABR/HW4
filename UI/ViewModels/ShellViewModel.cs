using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
	public class ShellViewModel : Conductor<object>
	{
		private static string _textContent = "";
		public static string TextContent
		{
			get { return _textContent; }
			set { _textContent = value; }
		}
	}
}