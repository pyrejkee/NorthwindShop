namespace NorthwindShop.BLL.EntitiesDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public CategoryDTO Category { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}
