using ItemStore.WebApi.Contexts;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Repositories
{
    public class EFItemRepository : IItemRepository
    {
        private readonly PostgreContext _dataContext;

        public EFItemRepository(PostgreContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int Create(ItemEntity item)
        {
            _dataContext.items.Add(item);
            _dataContext.SaveChanges();

            return item.id;
        }

        public int Delete(ItemEntity item)
        {
            var itemToDelete = _dataContext.items.FirstOrDefault(i => i.id == item.id);

            itemToDelete.is_deleted = true;

            return _dataContext.SaveChanges();  
        }

        public int Edit(ItemEntity item)
        {
            var itemToEdit = _dataContext.items.FirstOrDefault(i => i.id == item.id);

            itemToEdit.name = item.name;
            itemToEdit.price = item.price;

            _dataContext.SaveChanges();

            return itemToEdit.id;
        }

        public IEnumerable<ItemEntity> Get()
        {
            return _dataContext.items.Where(i => i.is_deleted == false).ToList();
        }

        public ItemEntity? Get(ItemEntity item)
        {
            return _dataContext.items.FirstOrDefault(i => i.id == item.id && i.is_deleted == false);
        }
    }
}
