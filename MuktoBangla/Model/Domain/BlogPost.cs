namespace MuktoBangla.Model.Domain
{
    public class BlogPost
    {
        public Guid ID { get; set; }
        public string Heading { get; set; }
        public string? ShortDescription { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string? PageTitle { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public bool Visible { get; set; }

        //Navigation Property
        public ICollection<Tag> Tags { get; set; }
    }
}
