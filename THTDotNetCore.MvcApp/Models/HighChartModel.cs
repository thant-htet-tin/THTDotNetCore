namespace THTDotNetCore.MvcApp.Models
{
    public class HighChartModel
    {
        public string name { get; set; }

        public List<Data> datas { get; set; }

    }

    public class Data
    {
        public string name { get; set; }
        public double y { get; set; }

        public Boolean? sliced { get; set; }

        public Boolean? selected { get; set; }

    }

}
