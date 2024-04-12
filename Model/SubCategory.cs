namespace Product.Model
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set;}

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
