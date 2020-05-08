using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EFmodels
{
    [Table("tbl_Role")]
    public class RoleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int? RoleID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string RoleName { get; set; }
    }
}