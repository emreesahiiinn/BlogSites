using Blog.Data.Entites;

public class Comment : EntityBase
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Content { get; set; }

    public int ArticleId { get; set; }
    public Article? Article { get; set; }
}
