namespace MuktoBangla.Model.ViewModels
{
    public class UpdateTagRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? TagDescription { get; set; }
    }
}
