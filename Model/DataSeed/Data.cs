using Model.Tables;

namespace Model.DataSeed;

public class Data
{
    public static List<Item> items = new List<Item>()
    {
        new Book(new Guid("9da10b15-3a9b-4fe0-9f70-131ee148dfcc"), "c# in depth", "Jon Skeet", Convert.ToDateTime("2015/10/20"), 528),
        new Book(new Guid("be2ab77c-322c-4a60-a9d5-0bcc70a44c22"), "Cake php", "Cake Software Foundation", Convert.ToDateTime("11/1/2023"), 943),
        new DVD(new Guid("96664792-a835-4c60-b8ba-763704154210"), "nhac tre", "xuan mai", Convert.ToDateTime("1991/1/1"), new TimeSpan(1,1,1)),
        new DVD(new Guid("93e22c7a-62bf-4c1e-ad8f-6855b0816cc5"), "nhac chu tinh", "bang kieu", Convert.ToDateTime("2001/12/20"), new TimeSpan(1,5,6)),
    };

    public static List<Borrower> borrowers = new List<Borrower>()
    {
        new Borrower("Kin", "Dung Si Thanh Khe", "1"),
        new Borrower("Bac", "thien cam", "2")
    };

    public static List<BorrowHistory> borrowHistories = new List<BorrowHistory>()
    {
        new BorrowHistory(new Guid("b9287ba5-2ad8-4e65-849c-dee813ddb778"), "1", new DateTime(2022, 12, 22, 12, 00, 00)),
        new BorrowHistory(new Guid("7e08c8ae-0fef-4bdc-9261-46206efad139"), "2", new DateTime(2022, 12, 22, 12, 00, 00))
    };

    public static List<BorrowHistoryDetail> borrowHistoryDetails = new List<BorrowHistoryDetail>()
    {
        new BorrowHistoryDetail(new Guid("38edfb21-0592-4159-81ee-c5199e25b2ba"), new Guid("9da10b15-3a9b-4fe0-9f70-131ee148dfcc"), new Guid("b9287ba5-2ad8-4e65-849c-dee813ddb778"), true),
        new BorrowHistoryDetail(new Guid("6e3fbc60-30a8-444a-98e0-061d3e3cad36"), new Guid("96664792-a835-4c60-b8ba-763704154210"), new Guid("b9287ba5-2ad8-4e65-849c-dee813ddb778"), true),
        new BorrowHistoryDetail(new Guid("e08d8be7-0ab6-4ac7-b811-9f92c3ec98f5"), new Guid("be2ab77c-322c-4a60-a9d5-0bcc70a44c22"), new Guid("7e08c8ae-0fef-4bdc-9261-46206efad139"), true),
        new BorrowHistoryDetail(new Guid("8578ea9b-e1e5-4ccf-b4e4-0746f97b8117"), new Guid("9da10b15-3a9b-4fe0-9f70-131ee148dfcc"), new Guid("7e08c8ae-0fef-4bdc-9261-46206efad139"), true)
    };
}