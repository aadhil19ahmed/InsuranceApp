using System;
using System.Collections.Generic;

namespace ACIA.Model
{
    public class GridSize
    {
        public int row { get; set; }
        public int column { get; set; }
    }

    public class Options
    {
        public int count { get; set; }
        public List<string> visibility { get; set; }
        public string dataURL { get; set; }
        public GridSize GridSize { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class DependentView
    {
        public int qid { get; set; }
        public IList<bool> selected_value { get; set; }
        public bool visibility { get; set; }
    }

    public class View
    {
        public bool default_visibility { get; set; }
        public string viewtype { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string InputType { get; set; }
        public int shouldGoToCMS { get; set; }
        public string url { get; set; }
        public string regex { get; set; }
        public int q_id { get; set; }
        public string columnName { get; set; }
        //public List<object> InputOptions { get; set; }
        public List<InputOption> InputOptions { get; set; }
        public int min_value { get; set; }
        public int max_value { get; set; }
        public IList<DependentView> dependent_views { get; set; }

    }

    public class InputOption
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Section
    {
        public string type { get; set; }
        public string title { get; set; }
        public Options options { get; set; }
        public List<View> Views { get; set; }
        public string table_name { get; set; }
    }

    public class Configuration
    {
        public string NavigationBarColor { get; set; }
        public string NavigationTextColor { get; set; }
    }

    public class DynamicContentPages
    {
        public string Pagetype { get; set; }
        public string Screenname { get; set; }
        public Configuration Configuration { get; set; }
        public List<Section> Section { get; set; }
        public string Error { get; set; }
    }

    public class DynamicModel
    {
        public DynamicContentPages dynamicContentPages { get; set; }
    }
}
