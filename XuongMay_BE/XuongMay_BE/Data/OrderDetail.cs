namespace XuongMay_BE.Data
{
    public class OrderDetail
    {
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public Guid SupervisorID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public Orders? Orders { get; set; }

        public Product? Product { get; set; }

        public Supervisor? Supervisor { get; set; }

    }
}