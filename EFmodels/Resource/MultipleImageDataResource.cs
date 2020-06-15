namespace EFmodels.Resource
{
    public class MultipleImageDataResource
    {
        public int? MultipleImageDataID { get; set; }
        public string ImagePathData { get; set; }
        public virtual APostWithMultipleImageModel APost { get; set; }
    }
}