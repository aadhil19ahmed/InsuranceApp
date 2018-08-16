using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Base section view.
    /// </summary>
	public class BaseSectionView : ContentView
	{
        public string reg_ex { get; set;  }
		public string viewtype { get; set; }
		public string name { get; set; }
		public string price { get; set; }
		public string description { get; set; }
		public string url { get; set; }
		public string dataurl { get; set; }
		public Color color { get; set; }
		public int fontSizeAttribute { get; set; }
		public virtual void CreateView() { }

		public static explicit operator Image(BaseSectionView v)
		{
			throw new NotImplementedException();
		}
	}
}