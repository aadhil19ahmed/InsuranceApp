using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using ACIA.Model;

namespace ACIA.Views.UILayer
{
	/// <summary>
	/// Form section.
	/// </summary>

	public class FormSection : BaseSection
	{
		private Section dataLayerSection;

		public FormSection(Section section)
		{
			dataLayerSection = section;
		}

		public override void CreateSection()
		{
			this.CreateFormSection();
		}
		/// <summary>
		/// Creates the form section.
		/// </summary>

		public void CreateFormSection()
		{
			var masterStack = new StackLayout()
			{
				Spacing = 25,
                BackgroundColor = Color.White
			};

			var sectionTitleLabel = new Label()
			{
				Text = this.title,
                Style = Application.Current.Resources["SectionTitleLabel"] as Style
			};

			masterStack.Children.Add(sectionTitleLabel);

			foreach (var item in this.Views)
			{
				masterStack.Children.Add(item);
			}
			Content = masterStack;
		}

	}
}
