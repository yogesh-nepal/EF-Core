using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_UserMenu")]
    public class UserMenuModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int? MUID { get; set; }
        public int? UserID { get; set; }
        public int? MenuID { get; set; }
    }
}