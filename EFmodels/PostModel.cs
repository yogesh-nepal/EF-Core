using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_Post")]
    public class PostModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int PostID { get; set; }

        [Column(TypeName = "int")]
        public int CategoryID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string FullDescription { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string AuthorName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Tag { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ImageURL { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedTime { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(max)")]
        public string UpdatedBy { get; set; }

        [NotMapped]
        public CategoryModel catagories { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
    }
}