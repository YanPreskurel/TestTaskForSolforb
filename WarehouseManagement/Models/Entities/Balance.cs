namespace WarehouseManagement.Models.Entities
{
    public class Balance
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }
        public Resource? Resource { get; set; }

        public int UnitId { get; set; }
        public Unit? Unit { get; set; }

        public decimal Quantity { get; set; }
    }
}
