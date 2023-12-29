namespace ItemStore.WebApi.Exceptions;

public class ShopNotFoundException : Exception
{
    public ShopNotFoundException(int id) : base($"Shop with id {id} not found.")
    {
        
    }
}
