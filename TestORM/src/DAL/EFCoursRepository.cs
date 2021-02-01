using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;

namespace TestORMCodeFirst.DAL
{
    public class EFCoursRepository
    {
        private CegepContext contexte;

        public EFCoursRepository(CegepContext ctx)
        {
            contexte = ctx;
        }

        public void AjouterCours(string codeCours, string nomCours)
        {
            Cours cours = new Cours { CodeCours = codeCours, NomCours = nomCours };

            contexte.Cours.Add(cours);
            contexte.SaveChanges();
        }

        public List<Cours> ObtenirListeCours()
        {
            return contexte.Cours.ToList();
        }
    }
}
