using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EFmodels
{
    [Table("tbl_Category")]
    public class CategoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? CategoryID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string CategoryName { get; set; }

        // [NotMapped]
        // public APostCategoryModel aPost { get; set; }
    }
}