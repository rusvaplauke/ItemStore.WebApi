using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemRepository
    {
        Task<List<ItemEntity>> Get();
        Task<ItemEntity?> Get(int id); 
        Task<int> Create(ItemEntity item);
        Task<int> Edit(ItemEntity item);
        Task<int> Delete(int id); 
    }
}
