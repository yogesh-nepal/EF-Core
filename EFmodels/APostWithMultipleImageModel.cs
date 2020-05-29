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

        public virtual ICollection<MultipleImageData> MultipleImageData { get; set; }
    }
}