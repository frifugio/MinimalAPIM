namespace MinimalAPIM_Test.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; }
        public List<Meal> Meals { get; set; }
        public double Total { get => Meals.Sum(m => m.Price); }

        public Order()
        {
            Id = Guid.NewGuid();
            Meals = new List<Meal>();
            TableNumber = String.Empty;
        }
    }
}
