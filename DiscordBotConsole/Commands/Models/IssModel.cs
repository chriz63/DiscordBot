public class IssModel
{
    public string message { get; set; }
    public int timestamp { get; set; }
    public IssPosition iss_position { get; set; }
}

public class IssPosition
{
    public string longitude { get; set; }
    public string latitude { get; set; }
}