using System.ComponentModel.DataAnnotations;

namespace Model.Tables;

public class BorrowHistoryDetail
{
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public Item Item { get; set; }

    public Guid BorrowHistoryId { get; set; }

    public BorrowHistory BorrowHistory { get; set; }

    public bool HasReturn { get; set; }

    public BorrowHistoryDetail(){}
    public BorrowHistoryDetail(Guid itemId, Guid borrowHistoryId)
    {
        this.Id = Guid.NewGuid();
        this.ItemId = itemId;
        this.BorrowHistoryId = borrowHistoryId;
    }

    public BorrowHistoryDetail(Guid id, Guid itemId, Guid borrowHistoryId)
    {
        this.Id = id;
        this.ItemId = itemId;
        this.BorrowHistoryId = borrowHistoryId;
    }

    public BorrowHistoryDetail(Guid id, Guid itemId, Guid borrowHistoryId, bool hasReturn)
    {
        this.Id = id;
        this.ItemId = itemId;
        this.BorrowHistoryId = borrowHistoryId;
        this.HasReturn = hasReturn;
    }

    public void GetInfo()
    {
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("Item Title: " + this.Item.Title);
        Console.WriteLine("Is return: " + this.HasReturn);
        Console.WriteLine("----------------------------------------------------------------");
    }
}