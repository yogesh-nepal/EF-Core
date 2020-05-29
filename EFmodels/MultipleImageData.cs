using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFmodels
{
    [Table("tbl_MultipleImageData")]
    public class MultipleImageData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? MultipleImageDataID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ImagePathData { get; set; }
    }
}