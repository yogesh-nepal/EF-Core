using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_Postcategory")]
    public class APostCategoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? PostCategoryID { get; set; }

        [Column(TypeName="int")]
        public int? PostID { get; set; }

        [Column(TypeName="int")]
        public int? CategoryID { get; set; }
    }
}