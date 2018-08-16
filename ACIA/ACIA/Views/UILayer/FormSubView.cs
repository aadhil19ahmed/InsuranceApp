using System;
using System.Collections.Generic;
using ACIA.Factory;
using ACIA.Model;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Form sub view.
    /// </summary>
	public class FormSubView : BaseSectionView
	{
		public DUInputView inputView;
		public Model.View qView;
        public LabelSectionView questionLabel;
		StackLayout QAstack;

		public FormSubView(Model.View view)
		{
			QAstack = new StackLayout()
			{
				Spacing = 10,
				Margin = new Thickness(10, 10, 10, 10),
				//BackgroundColor = Color.Green
			};
			qView = view;
		}

		public override void CreateView()
		{
			this.CreateQAView();
		}

        /// <summary>
        /// Creates the QAV iew.
        /// </summary>
		private void CreateQAView()
		{
            questionLabel = new LabelSectionView(qView, qView.name, Color.Black, Color.Transparent, LineBreakMode.WordWrap);
			questionLabel.CreateView();
            questionLabel.IsVisible = qView.default_visibility;
			inputView = InputViewFactory.GetInputView(qView);
            inputView.IsVisible = qView.default_visibility;
			QAstack.Children.Add(questionLabel);
			QAstack.Children.Add(inputView);
			Content = QAstack;
		}
	}
}