using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_MediaPost")]
    public class MediaPostModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? MediaPostID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string MediaTitle { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string MediaPostPath { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        public string MediaSource { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string MediaPostDescription { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string MediaTags { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string MediaThumbnail { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? PublishDate { get; set; } = DateTime.Now;

        [Column(TypeName = "int")]
        public int? MediaViews { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string MediaAuthor { get; set; }

    }
}