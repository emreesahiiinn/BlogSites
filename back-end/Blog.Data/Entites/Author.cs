using Blog.Data.Entites;

public class Author : EntityBase
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public ICollection<Article>? Articles { get; set; }
}

