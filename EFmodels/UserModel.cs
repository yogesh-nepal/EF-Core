using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EFmodels
{
    [Table("tbl_User")]
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? UserID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserFullName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserEmailID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserPassword { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserAddress { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserGender { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserPhone { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        [NotMapped]
        public string RoleName { get; set; }

        [NotMapped]
        public IList<RoleModel> ListOfRoles { get; set; }

        [NotMapped] 
        public RoleModel roles { get; set; }

        [NotMapped] 
        public MenuModel menus { get; set; }
    }
}