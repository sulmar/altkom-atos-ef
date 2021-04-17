namespace Models
{
    public abstract class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
