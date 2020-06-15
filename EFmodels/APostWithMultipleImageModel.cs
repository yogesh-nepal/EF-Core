using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_MultipleImagePost")]
    public class APostWithMultipleImageModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? MultipleImagePostID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ImageTitle { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ImageDescription { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        public string FullDescription { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? PublishDate { get; set; } = DateTime.Now;

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string AuthorName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Tag { get; set; }

        [NotMapped]
        public CategoryModel catagories { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        public virtual ICollection<MultipleImageData> MultipleImageData { get; set; }
    }
}
