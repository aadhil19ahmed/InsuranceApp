using System;
using System.Collections.Generic;

namespace ACIA.Model
{
    public class GetCarSuggestedQuotesModel
    {
        public int BasicAmount { get; set; }
        public int ChoiceAmount { get; set; }
        public string PropertyName { get; set; }
        public int RecommendedAmount { get; set; }
        public string SectionName { get; set; }
    }
}
