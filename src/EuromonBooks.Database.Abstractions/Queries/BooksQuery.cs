namespace EuromonBooks.Database.Abstractions.Queries
{
    public class BooksQuery
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int TotalItems { get; set; }
    }
}