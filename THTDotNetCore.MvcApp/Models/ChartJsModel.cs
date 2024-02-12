namespace THTDotNetCore.MvcApp.Models
{
    public class ChartJsModel
    {
        public string[] labels { get; set; }

        public List<datasetsModel> datasets { get; set; }
    }

    public class datasetsModel
    {
        public string label { get; set; }
        public int[] data { get; set; }

    public string backgroundColor { get; set; }

        public string stack {  get; set; }
    }
    
}
