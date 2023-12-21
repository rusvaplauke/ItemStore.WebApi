using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemService
    {
        Task<List<GetItemDto>> Get();
        Task<GetItemDto> Get(int id);
        Task<GetItemDto> Create(PostItemDto item);
        Task<GetItemDto> Edit(PutItemDto item);
        Task Delete(int id);
    }
}
