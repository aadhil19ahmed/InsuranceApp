﻿using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Simple image section.
    /// </summary>
	public class SimpleImageSection:BaseSection
    {
        
        private static Page page;
        //private DataLayer.Section _section;
        public override void CreateSection()
        {
            this.CreateSimpleImageSection();
        }
        private void CreateSimpleImageSection()
		{
            var StackLayout = new StackLayout()
            {
                BackgroundColor = Color.White,
            };

			//var sectionTitleLabel = new Label()
			//{
   //             Text = this.title,
   //             Style = Application.Current.Resources["SectionTitleLabel"] as Style
			//};
			
            for (int index = 0; index < this.Views.Count; index++)
            {
                TapGestureRecognizer imageTap = new TapGestureRecognizer();
                imageTap.Tapped += OnItemSelected;
                this.Views[index].GestureRecognizers.Add(imageTap);
                this.Views[index].ClassId = index.ToString();
                //StackLayout.Children.Add(sectionTitleLabel);
    //            sectionTitleLabel.SetBinding(BackgroundColorProperty, "AppThemeColorValue");
				//sectionTitleLabel.BindingContext = App.appThemeViewModel;
                StackLayout.Children.Add(this.Views[index]);

            }
			Content = StackLayout;
        }

		async void OnItemSelected(Object sender, EventArgs e)
		{
            
		}
    }
}
                                