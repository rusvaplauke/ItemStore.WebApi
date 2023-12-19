using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemRepository
    {
        IEnumerable<GetItemDto> GetItems();
        GetItemDto GetItem(int id);
        GetItemDto CreateItem(PostItemDto item);
        GetItemDto EditItem(PutItemDto item);
        int DeleteItem(int id);
        bool ItemExists(int id);
    }
}
