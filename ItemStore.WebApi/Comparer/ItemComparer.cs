using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Comparer
{
    public class ItemComparer : IComparer<ItemEntity>
    {
        public int Compare(ItemEntity? x, ItemEntity? y)
        {
            int _result = x.Id.CompareTo(y.Id);

            if (_result == 0)
                _result = string.Compare(x.Name, y.Name);

            if (_result == 0)
                _result = x.Price.CompareTo(y.Price);

            if (_result == 0)
                _result = x.IsDeleted.CompareTo(y.IsDeleted);

            return _result;
        }
    }
}
