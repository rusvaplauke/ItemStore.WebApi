using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemRepository
    {
        IEnumerable<ItemEntity> Get();
        ItemEntity? Get(ItemEntity item);
        int Create(ItemEntity item);
        int Edit(ItemEntity item);
        int Delete(ItemEntity item); 
    }
}
