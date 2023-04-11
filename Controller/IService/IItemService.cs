using Model.Tables;

namespace Controller.IService;

public interface IItemService
{
    List<Item> GetListItemByName(string name);

    Item GetItemById(Guid id);

    bool DeleteItemById(Guid id);

    bool UpdateItemById(Guid id, Item item);

    bool InsertItem(Item item);

    List<Item> GetListItemNotBorrow();

    List<Book> GetListBookWithSearch(string name);

    bool ReturnItem(List<Guid> ids);

    int CountDVD(int year);
}