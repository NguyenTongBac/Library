namespace Model.Tables;

public class DVD : Item
{
    public DVD(){}

    public DVD(Guid id, string title, string author, DateTime published, TimeSpan runTime, bool isBorrowed = false): base(id, title, author, published, isBorrowed)
    {
        this.RunTime = runTime;
    }

    public DVD(string title, string author, DateTime published, TimeSpan runTime, bool isBorrowed = false): base(title, author, published, isBorrowed)
    {
        this.RunTime = runTime;
    }

    public TimeSpan RunTime { get; set; }

    public override DVD Update()
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

        return this;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine("RunTime: " + this.RunTime);
    }
}