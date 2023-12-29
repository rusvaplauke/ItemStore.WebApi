using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Exceptions;

public class PurchaseExistsException : Exception
{
    public PurchaseExistsException(PurchaseDto purchase) : base($"User with id {purchase.UserId} has already purchased product with id {purchase.ItemId}.")
    {
        
    }
}
