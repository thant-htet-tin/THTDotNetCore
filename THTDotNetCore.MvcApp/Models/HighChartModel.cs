namespace THTDotNetCore.MvcApp.Models
{
    public class HighChartModel
    {
        public string? name { get; set; }

        public List<Data>? datas { get; set; }

        public List<StackedData>? stackedData { get; set;}

        
    }

    public class Data
    {
        public string name { get; set; }
        public double y { get; set; }

        public Boolean? sliced { get; set; }

        public Boolean? selected { get; set; }

    }


    public class StackedData
    {
        public string name { get; set; }
        public List<int> data { get; set; }

        public string stack { get; set; }
    }
}
