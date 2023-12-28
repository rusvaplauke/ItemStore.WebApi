using ItemStore.WebApi.Contexts;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.Repositories
{
    public class EFItemRepository : IItemRepository
    {
        private readonly PostgreContext _dataContext;

        public EFItemRepository(PostgreContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> Create(ItemEntity item)
        {
            _dataContext.Items.Add(item);
            await _dataContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var itemToDelete = _dataContext.Items.FirstOrDefault(i => i.Id == id);

            itemToDelete.IsDeleted = true;

            return await _dataContext.SaveChangesAsync();  
        }

        public async Task<ItemEntity> Edit(ItemEntity item)
        {
            var itemToEdit = _dataContext.Items.FirstOrDefault(i => i.Id == item.Id);

            itemToEdit.Name = item.Name;
            itemToEdit.Price = item.Price;

            await _dataContext.SaveChangesAsync();

            return itemToEdit;
        }

        public async Task<List<ItemEntity>> Get()
        {
            return await _dataContext.Items.Where(i => i.IsDeleted == false).ToListAsync();
        }

        public async Task<ItemEntity?> Get(int id)
        {
            return await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
        }
    }
}
