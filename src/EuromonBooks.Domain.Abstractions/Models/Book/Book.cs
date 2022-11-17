namespace EuromonBooks.Abstractions.Models
{
    public class BookModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string PurchasePrice { get; set; }
        public bool? IsActive { get; set; }
    }

    public class BookWithDates : BookModel
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class AllBooks
    {
        public int TotalItems { get; set; }
        public List<BookWithDates> Books { get; set; }
    }
}