using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Data.Entities
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Thread { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Level { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Logger { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(4000)]
        public string Message { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(2000)]
        public string Exception { get; set; }
    }
}
