using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EFmodels
{
    [Table("tbl_Menu")]
    public class MenuModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int? MenuID { get; set; }

        [Column(TypeName="nvarchar(max)")]
        public string MenuName { get; set; }

        [Column(TypeName="nvarchar(max)")]
        public string MenuURL { get; set; }

        [Column(TypeName="nvarchar(max)")]
        public string MenuUnder { get; set; }

        [Column(TypeName="bit")]
        public bool IsActive { get; set; } 
    }
}