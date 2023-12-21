﻿using ItemStore.WebApi.Contexts;
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
            _dataContext.Items.Add(item);
            _dataContext.SaveChanges();

            return item.Id;
        }

        public int Delete(ItemEntity item)
        {
            var itemToDelete = _dataContext.Items.FirstOrDefault(i => i.Id == item.Id);

            itemToDelete.IsDeleted = true;

            return _dataContext.SaveChanges();  
        }

        public int Edit(ItemEntity item)
        {
            var itemToEdit = _dataContext.Items.FirstOrDefault(i => i.Id == item.Id);

            itemToEdit.Name = item.Name;
            itemToEdit.Price = item.Price;

            _dataContext.SaveChanges();

            return itemToEdit.Id;
        }

        public IEnumerable<ItemEntity> Get()
        {
            return _dataContext.Items.Where(i => i.IsDeleted == false).ToList();
        }

        public ItemEntity? Get(ItemEntity item)
        {
            return _dataContext.Items.FirstOrDefault(i => i.Id == item.Id && i.IsDeleted == false);
        }
    }
}
