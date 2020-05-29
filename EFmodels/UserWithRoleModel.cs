using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_UserRole")]
    public class UserWithRoleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int? RUID { get; set; }
        public int? UserID { get; set; }
        public int? RoleID { get; set; }
    }
}