namespace Blog.Data.Entites
{
    public class Article : EntityBase
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}