using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestORMCodeFirst.Entities
{
    [Table ("COURS")]
    public class Cours
    {
        //propriétés
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string CodeCours { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string NomCours { get; set; }

        //Propriétés de navigation
        public virtual ICollection<InscriptionCours> Inscriprions { get; set; }
    }
}
