using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestORMCodeFirst.Entities
{
    [Table("INSCRIPTION_COURS")]
    public class InscriptionCours
    {
        //Propriétés
        [Required]
        public short EtudiantID { get; set; }

        [ForeignKey("CodeCours")]
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string CodeCours { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string CodeSession { get; set; }

        public short? NoteFinale { get; set; }

        //Propriétés de navigation
        [ForeignKey("EtudiantID")]
        public virtual Etudiant Etudiant { get; set; }

        public virtual ICollection<Cours> Cours { get; set; }

    }
}
