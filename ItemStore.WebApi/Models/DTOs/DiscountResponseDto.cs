namespace ItemStore.WebApi.Models.DTOs
{
    public class DiscountResponseDto
    {
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public decimal TotalPrice { get; set; }

        public DiscountResponseDto()
        { 
        }

        public DiscountResponseDto(DiscountRequestDto request, decimal totalPrice)
        {
            Quantity = request.Quantity;
            ItemId = request.ItemId;
            TotalPrice = totalPrice;
        }
    }
}
