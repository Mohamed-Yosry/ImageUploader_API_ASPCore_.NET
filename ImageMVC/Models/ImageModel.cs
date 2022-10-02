namespace ImageMVC.Models
{
    public class ImageModel
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Metatag { get; set; }
        public DateTime InsertionDate { get; set; }
    }
}
