namespace ItemStore.WebApi.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int id) : base($"Item with id {id} not found.")
        {
            
        }
    }
}
