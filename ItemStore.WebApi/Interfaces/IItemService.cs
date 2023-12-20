using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemService
    {
        List<GetItemDto> Get();
        GetItemDto Get(int id);
        GetItemDto Create(PostItemDto item);
        GetItemDto Edit(PutItemDto item);
        void Delete(int id);
    }
}
