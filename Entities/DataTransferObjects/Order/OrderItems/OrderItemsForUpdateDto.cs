namespace Entities.DataTransferObjects.Order
{
   public class OrderItemsForUpdateDto
    {
        public long OrderItemID { get; set; }
        public long OrderID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
    }
}
