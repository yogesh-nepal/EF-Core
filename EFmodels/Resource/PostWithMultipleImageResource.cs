using System;
using System.Collections.Generic;

namespace EFmodels.Resource
{
    public class PostWithMultipleImageResource
    {
        public int? MultipleImagePostID { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDescription { get; set; }
        public string FullDescription { get; set; }
        public DateTime? PublishDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public string AuthorName { get; set; }
        public string Tag { get; set; }
        public virtual ICollection<MultipleImageDataResource> MultipleImageData { get; set; }

    }
}