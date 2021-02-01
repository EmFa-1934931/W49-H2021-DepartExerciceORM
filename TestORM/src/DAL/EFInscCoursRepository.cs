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
    public class EFInscCoursRepository
    {
        private CegepContext contexte;
        private const short NOTE_PASSAGE = 60;

        public EFInscCoursRepository(CegepContext ctx)
        {
            contexte = ctx;
        }

        public void AjouterInscription(short etudiantId, string codeCours, string session)
        {
            InscriptionCours inscription = new InscriptionCours { EtudiantID = etudiantId, CodeCours = codeCours, CodeSession = session };

            contexte.InscCours.Add(inscription);
            contexte.SaveChanges();
        }

        public ICollection<InscriptionCours> ObtenirInscriptions()
        {
            return contexte.InscCours.ToList();
        }

        public InscriptionCours ObtenirInscription(short etudiantID, string codeCours, string session)   //TO DO: à tester
        {
            return contexte.InscCours.Find(etudiantID, codeCours, session);
        }

        public void SupprimerToutesLesInscriptions()
        {
            contexte.InscCours.RemoveRange(contexte.InscCours);
            contexte.SaveChanges();
        }

        public int NombreEtudiantsInscritsAuCegep(string session)
        {
            return contexte.InscCours.Where(insc => insc.CodeSession == session)
                                            .GroupBy(insc => insc.EtudiantID)
                                            .Select(groupe => new { groupe.Key })
                                            .Count();
        }

        public void AjouterCours(InscriptionCours cours)
        {
            contexte.InscCours.Add(cours);
            contexte.SaveChanges();
        }

        public int NombreEtudiantsInscrits(string codeCours, string session)
        {
            return contexte.InscCours.Where(insc => insc.CodeCours == codeCours && insc.CodeSession == session).Count();
        }

        public int? NombreInscriptionsCours(short etudiantId, string session)
        {
            return contexte.InscCours.Where(insc => insc.CodeSession == session && insc.EtudiantID == etudiantId).Count();
        }

        public void MettreAJourNoteFinale(short etudiantID, string codeCours, string session, short note)
        {
            InscriptionCours insc = contexte.InscCours.Find(etudiantID, codeCours, session);
            insc.NoteFinale = note;
        }

        public double ObtenirPourUneClasseLaMoyenne(string codeCours, string session)
        {
            List<InscriptionCours> inscCoursList = GetInscList(codeCours, session);
            short sum = 0;

            foreach (InscriptionCours insc in inscCoursList)
            {
                sum += (short)insc.NoteFinale;
            }

            double average = (double)(sum / inscCoursList.Count());

            return average;
        }

        public int ObtenirPourUneClasseNombreEchecs(string codeCours, string session)
        {
            List<InscriptionCours> inscCoursList = GetInscList(codeCours, session);
            int count = 0;

            foreach(InscriptionCours insc in inscCoursList)
            {
                if(insc.NoteFinale < NOTE_PASSAGE)
                {
                    count++;
                }
            }
            return count++;
        }

        private List<InscriptionCours> GetInscList(string codeCours, string session)
        {
            List<InscriptionCours> inscCoursList = contexte.InscCours.Where(insc => insc.CodeCours == codeCours && insc.CodeSession == session).ToList();
            return inscCoursList;
        }

    }
}
