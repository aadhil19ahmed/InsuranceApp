using System;
using System.Collections.Generic;
using ACIA.Factory;
using ACIA.Model;
using ACIA.Services.RequestProvider;
using ACIA.Views;
using ACIA.Views.UILayer;
using Xamarin.Forms;

namespace ACIA.Services.GetDynamicPage
{
    public class GetDynamicPageService : IGetDynamicPageService
    {
        private readonly IRequestProvider _requestProvider;

        public GetDynamicPageService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public DynamicUIPage GetDynamicPage(string formID)
        {
            return new DynamicUIPage(Convert(_requestProvider.GetDynamicPage(formID)));
        }

        private DynamicPageModel Convert(DynamicModel dynamicModel)
        {
            DynamicPageModel dynamicPageModel = new DynamicPageModel();

            dynamicPageModel.Pagetype = dynamicModel.dynamicContentPages.Pagetype;
            dynamicPageModel.Screenname = dynamicModel.dynamicContentPages.Screenname;
            //dynamicPageModel.Error = plainJsonObject.dynamicContentPages.Error;

            foreach (Section section in dynamicModel.dynamicContentPages.Section)
            {
                BaseSection baseSection;
                var BaseSectionView = new List<BaseSectionView>();
                baseSection = SectionFactory.GetSectionForType(section, false);
                baseSection.type = section.type;
                baseSection.title = section.title;
                if (section.Views != null)
                {
                    foreach (Model.View view in section.Views)
                    {
                        BaseSectionView baseSectionView = ViewFactory.GetSectionForView(view);
                        baseSectionView.reg_ex = view.regex;
                        baseSectionView.name = view.name;
                        baseSectionView.url = view.url;
                        baseSectionView.viewtype = view.viewtype;
                        baseSectionView.fontSizeAttribute = 10;
                        baseSectionView.color = Color.Black;
                        baseSectionView.CreateView();
                        BaseSectionView.Add(baseSectionView);
                    }
                }
                baseSection.Views = BaseSectionView;
                baseSection.CreateSection();
                dynamicPageModel.Sections.Add(baseSection);
            }
            return dynamicPageModel;
        }
    }
}
