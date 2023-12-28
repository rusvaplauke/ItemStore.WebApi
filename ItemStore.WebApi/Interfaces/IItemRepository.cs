using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces;

public interface IItemRepository
{
    Task<List<ItemEntity>> Get();

    Task<ItemEntity?> Get(int id); 

    Task<int> Create(ItemEntity item);

    Task<ItemEntity?> Edit(ItemEntity item);

    Task<int> Delete(int id); 
}
