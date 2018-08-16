using System;
using System.Collections.Generic;
using System.IO;
using ACIA.Model;
using ACIA.Views.UILayer;

namespace ACIA.Factory
{
	public static class SectionFactory
	{
        /// <summary>
        /// Gets the type of the section for.
        /// </summary>
        /// <returns>The section for type.</returns>
        /// <param name="section">Section.</param>
        /// <param name="isViewAll">If set to <c>true</c> is view all.</param>
        public static BaseSection GetSectionForType(Section section, bool isViewAll)
		{

			switch (section.type.ToUpper())
			{
				case "CAROUSEL":
					return new CarouselSection();

				case "SIMPLESECTION":
					return new SimpleImageSection();

                case "CMSGRID":
                    return new CmsGrid(section);

				case "FORMS":
                    return new FormSection(section);

				default:
					return new BaseSection();
			}

		}
	}
}