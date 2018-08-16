using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Image section view.
    /// </summary>
    public class ImageSectionView : BaseSectionView
    {
        public String _imageName;
        public Image image;

        public ImageSectionView()
        {

        }

        public ImageSectionView(String ImageName)
        {
            this._imageName = ImageName;
            this.ClassId = ImageName;
        }
        /// <summary>
        /// Creates the image view.
        /// </summary>

        private void CreateImageView()
        {
            var stackLayout = new StackLayout();
            image = new Image
            {
                Source = this._imageName,
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
            };
            image.Aspect = Aspect.AspectFit;
            //if (_imageName.Contains("http"))
            //{
            //    image.Source = this._imageName;
            //}
            //else
            //{
            //    image.Source = ImageSource.FromResource(this._imageName);
            //}
            stackLayout.Children.Add(image);
            Content = stackLayout;
        } 

        public override void CreateView()
        {
            base.CreateView();
            this.CreateImageView();
        }
    }
}