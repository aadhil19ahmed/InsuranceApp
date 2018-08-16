using System;
using System.Diagnostics;
using ACIA.Views.UILayer;
using Xamarin.Forms;
using ACIA.Model;

namespace ACIA.Factory
{
	public class ViewFactory
	{
        /// <summary>
        /// Gets the section for view.
        /// </summary>
        /// <returns>The section for view.</returns>
        /// <param name="view">View.</param>
		public static BaseSectionView GetSectionForView(Model.View view)
		{
			switch (view.viewtype.ToUpper())
			{
                case "BUTTONVIEW":

                    return new DUIButtonView(view);

				case "IMAGEVIEW":

					return new ImageSectionView(view.image);

				case "FORMSUBVIEW":

                    return new FormSubView(view);					

				default:

					return new BaseSectionView();
			}

		}		
	}
}