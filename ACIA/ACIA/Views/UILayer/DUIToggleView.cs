using System;
using Xamarin.Forms;
using ACIA.Model;
using ACIA.Services.Navigation;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// DUIToggle view.
    /// </summary>
    public class DUIToggleView : DUInputView
    {
        Model.View _view;
        public DUIToggleView(Model.View view)
        {
            _view = view;
            Switch switchView = new Switch();
            switchView.Toggled += switcher_Toggled;
            var NoLabel = new Label
            {
                Text = "No",
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                FontFamily = "HelveticaNeue-UltraLight",
                FontSize = 14
            };
			var YesLabel = new Label
			{
				Text = "Yes",
				BackgroundColor = Color.White,
                TextColor = Color.Black,
                FontFamily = "HelveticaNeue-UltraLight",
                FontSize = 14
			};
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(8,0,0,0)
            };
            //NoLabel.IsVisible = view.default_visibility;
            //switchView.IsVisible = view.default_visibility;
            //YesLabel.IsVisible = view.default_visibility;
            stackLayout.Children.Add(NoLabel);
            stackLayout.Children.Add(switchView);
            stackLayout.Children.Add(YesLabel);
            Content = stackLayout;
        }

		void switcher_Toggled(object sender, ToggledEventArgs e)
		{

            DynamicUIPage dynamicUIPage = DUINavigationStack.GetCurrentPage();
            CapturedValue = e.Value;
            if (_view.dependent_views!= null && _view.dependent_views.Count > 0)
            {
                foreach (BaseSection section in dynamicUIPage.baseSections)
                {
                    foreach (BaseSectionView subView in section.Views)
                    {
                        if (subView.GetType() == typeof(FormSubView))
                        {
                            var formSubView = subView as FormSubView;
                            foreach (DependentView dependentView in _view.dependent_views)
                            {
                                if (dependentView.qid == formSubView.qView.q_id)
                                {
                                    if(e.Value == dependentView.selected_value[0])
                                    {
                                        formSubView.questionLabel.IsVisible = dependentView.visibility;
                                        formSubView.inputView.IsVisible = dependentView.visibility;
                                    }
                                    else
                                    {
                                        formSubView.questionLabel.IsVisible = !dependentView.visibility;
                                        formSubView.inputView.IsVisible = !dependentView.visibility;
                                    }
                                }
                            }
                        }
                    }
                }
            }
		}
    }
}
