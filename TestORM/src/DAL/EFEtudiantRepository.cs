using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;

namespace TestORMCodeFirst.DAL
{
    public class EFEtudiantRepository
    {
        //Champs
        private CegepContext contexte;

        //Constructeur
        public EFEtudiantRepository(CegepContext ctx)
        {
            contexte = ctx;
        }

        public int? NombreCoursInscrits(short etudiantId)
        {
            Etudiant etud = contexte.Etudiants.Find(etudiantId);
            if (etud != null)
            {
                return etud.Cours.Count();
            }
            else
            {
                return null;
            }
        }

        public List<InscriptionCours> ObtenirListeCours(short etudiantId)
        {
            Etudiant etud = contexte.Etudiants.Find(etudiantId);
            if (etud != null)
            {
                return etud.Cours.ToList();
            }
            else
            {
                return null;
            }
        }

        public void AjouterEtudiant(Etudiant etudiant)
        {
            contexte.Etudiants.Add(etudiant);
            contexte.SaveChanges();
        }

        public void SupprimerTousEtudiants()
        {
            contexte.Etudiants.RemoveRange(contexte.Etudiants);
            contexte.SaveChanges();
        }

        public void UpdateStudent(Etudiant etudiant)
        {
            contexte.Etudiants.Update(etudiant);
            contexte.SaveChanges();
        }

        public Etudiant TrouverEtudiantParNom(string nomEtudiant)
        {
            IEnumerable<Etudiant> etudiants = this.ObtenirListeEtudiants().Where(e => e.Nom == nomEtudiant);
            if (etudiants.Count() > 0)
            {
                return etudiants.First();
            }
            else
            {
                return null;
            }

        }

        public List<Etudiant> ClasserEtudiantsDDN()
        {
            var selectEtudiants = contexte.Etudiants.OrderBy(e => e.DateNaissance).ToList();
            return selectEtudiants;
        }

        public List<Etudiant> ObtenirListeEtudiants()
        {
            return contexte.Etudiants.ToList();
        }

        public int NombreEtudiants()
        {
            return contexte.Etudiants.Count();
        }

        public int? NombreInscriptionsCours(short etudiantId, string session)
        {
            Etudiant etudiant = contexte.Etudiants.Find(etudiantId);
            if(etudiant != null)
            {
                return etudiant.Cours.Where(insc => insc.CodeSession == session).Count();
            }

            return null;
        }
    }
}
