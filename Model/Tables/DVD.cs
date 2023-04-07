namespace Model.Tables;

public class DVD : Item
{
    public DVD(Guid id, string title, string author, DateTime published, bool isBorrowed, TimeSpan runTime): base(id, title, author, published, isBorrowed)
    {
        this.RunTime = runTime;
    }

    public DVD(Guid id, string title, string author, DateTime published, TimeSpan runTime) : base(id, title, author, published)
    {
        this.RunTime = runTime;
    }

    public DVD(string title, string author, DateTime published, TimeSpan runTime) : base(title, author, published)
    {
        this.RunTime = runTime;
    }

    public DVD(string title, string author, DateTime published, TimeSpan runTime, bool isBorrowed) : base(title, author, published, isBorrowed)
    {
        this.RunTime = runTime;
    }

    public TimeSpan RunTime { get; set; }

    public override void Update()
    {
        base.Update();
        var timeSpan = new TimeSpan();

        Console.Write("Runtime: " + this.RunTime + " -(Format: hh:mm:ss)> ");
        var runtime = Console.ReadLine();

        while(runtime != "" && !TimeSpan.TryParse(runtime, out timeSpan))
        {
            Console.WriteLine("Please enter right format day: ");
            runtime = Console.ReadLine();
        }

        this.RunTime = timeSpan;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine("RunTime: " + this.RunTime);
    }
}