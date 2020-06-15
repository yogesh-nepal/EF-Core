using System;

namespace EFmodels.Resource
{
    public class MediaPostResource
    {
        public int? MediaPostID { get; set; }
        public string MediaTitle { get; set; }
        public string MediaPostPath { get; set; }
        public string MediaSource { get; set; }
        public string MediaPostDescription { get; set; }
        public string MediaTags { get; set; }
        public string MediaThumbnail { get; set; }
        public DateTime? PublishDate { get; set; } = DateTime.Now;
        public int? MediaViews { get; set; }
        public string MediaAuthor { get; set; }
    }
}