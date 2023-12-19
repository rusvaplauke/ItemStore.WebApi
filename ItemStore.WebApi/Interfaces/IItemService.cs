using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemService
    {
        List<GetItemDto> GetItems();
        GetItemDto GetItem(int id);
        GetItemDto PostItem(PostItemDto item);
        GetItemDto PutItem(PutItemDto item);
        int DeleteItem(int id);
    }
}
