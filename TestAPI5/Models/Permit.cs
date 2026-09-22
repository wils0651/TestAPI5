using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("permits")]
    public class Permit
    {
        [Key]
        [Column("permit_id")]
        public int PermitId { get; set; }

        [Column("division_id")]
        public int DivisionId { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }
    }
}
