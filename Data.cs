using System;

public class Rootobject
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string[] Options { get; set; }
}
public class Options
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Votes { get; set; }
}
