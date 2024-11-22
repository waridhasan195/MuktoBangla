namespace MuktoBangla.Model.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? TagDescription { get; set; }

        //Navigation Property
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
