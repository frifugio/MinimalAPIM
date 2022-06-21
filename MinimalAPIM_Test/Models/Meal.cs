namespace MinimalAPIM_Test.Models
{
    public class Meal
    {
        public Meal()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}