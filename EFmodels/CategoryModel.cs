using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EFmodels
{
    [Table("tbl_Category")]
    public class CategoryModel
    {
<<<<<<< HEAD
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? CategoryID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string CategoryName { get; set; }

        // [NotMapped]
        // public APostCategoryModel aPost { get; set; }
=======
            [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
            public int? CategoryID { get; set; }
            
            [Column(TypeName = "nvarchar(max)")]
            public string CategoryName { get; set; }
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
    }
}