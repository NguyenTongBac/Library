using Model.Tables;
using Controller.IService;
using Model.DataSeed;

namespace Controller.Service;

public class ItemService : IItemService
{
    public bool DeleteItemById(Guid id)
    {
        var item = Data.items.FirstOrDefault(x => x.Id == id);
        
        if(item == null)
            return false;

        Data.items.Remove(item);
        return true;
    }

    public Item GetItemById(Guid id)
    {
        return Data.items.FirstOrDefault(x => x.Id == id);
    }

    public List<Item> GetListItemByName(string name)
    {
        return Data.items.Where(x => x.Title.Contains(name)).ToList();
    }

    public bool UpdateItemById(Guid id, Item itemUpdate)
    {
        var item = Data.items.FirstOrDefault(x => x.Id == id);
        
        if(item == null)
            return false;
            
        item = itemUpdate;

        return true;
    }

    public bool InsertItem(Item item)
    {
        Data.items.Add(item);

        return true;
    }

    public List<Item> GetListItemNotBorrow()
    {
        return Data.items.Where(x => x.IsBorrowed == false).ToList();
    }

    public List<Book> GetListBookWithSearch(string name)
    {
        return Data.items.OfType<Book>().Where(x => x.Title.Contains(name)).OrderBy(x => x.Title).ToList();
    }

    public int CountDVD(int year)
    {
        return Data.items.OfType<DVD>().Where(x => x.Published.Year == year).Count();
    }

    public bool ReturnItem(List<Guid> ids)
    {
        var items = Data.items.Where(x => ids.Any(y => y == x.Id)).ToList();
        var borrowHistoryDetails = Data.borrowHistoryDetails.Where(x => !x.HasReturn && items.Any(y => y.Id == x.ItemId)).ToList();

        if(ids.Count > items.Count)
        {
            return false;
        }

        items.ForEach(x => x.IsBorrowed = false);
        borrowHistoryDetails.ForEach(x => x.HasReturn = true);
        
        return true;
    }
}