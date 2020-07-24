namespace Entities.DataTransferObjects.Order
{
   public class OrderItemsForCreationDto
    {        
        public long OrderID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
    }
}
