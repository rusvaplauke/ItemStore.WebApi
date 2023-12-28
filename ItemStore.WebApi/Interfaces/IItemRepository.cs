using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces;

public interface IItemRepository
{
    Task<List<ItemEntity>> GetAsync();

    Task<ItemEntity?> GetAsync(int id); 

    Task<int> CreateAsync(ItemEntity item);

    Task<ItemEntity?> EditAsync(ItemEntity item);

    Task<int> DeleteAsync(int id); 
}
