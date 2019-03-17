namespace SimpleESTest.Api.Helpers
{
    public class ElasticConnectionSettings
    {
        public string ClusterUrl { get; set; }

    public string DefaultIndex
    {
        get
        {
            return this.defaultIndex;
        }
        set
        {
            this.defaultIndex = value.ToLower();
        }
    }

    private string defaultIndex;

    private string defaultType;
    public string DefaultType
    {
        get { return defaultType;}
        set { defaultType = value.ToLower();}
    }
    
    }
}