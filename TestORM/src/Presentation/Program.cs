using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.DAL;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;

namespace TestORMCodeFirst.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                
                //Création du contexte
                var optionsBuilder = new DbContextOptionsBuilder<CegepContext>();
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["bdCegepConnectionString"].ConnectionString);
                CegepContext contexte = new CegepContext(optionsBuilder.Options);

                //Instanciation des repositories
                EFEtudiantRepository etudiantRepo = new EFEtudiantRepository(contexte);
                EFInscCoursRepository inscCoursRepo = new EFInscCoursRepository(contexte);

                Console.WriteLine("Démarrage...");

                Etudiant marc = new Etudiant() { Nom = "Dubé", Prenom = "Marc", DateNaissance = Convert.ToDateTime("1977-10-10") };
                Etudiant jean = new Etudiant() { Nom = "Simard", Prenom = "Jean", DateNaissance = Convert.ToDateTime("1981-10-10"), Tuteur = marc};

                //Ajout d'un étudiant
                Console.WriteLine("Ajout de 2 étudiants");
                etudiantRepo.AjouterEtudiant(marc);
                etudiantRepo.AjouterEtudiant(jean);
                AfficherListeEtudiants(etudiantRepo);
                Console.ReadKey();
        

                //Supprimer tous les étudiants
                Console.WriteLine("");
                Console.WriteLine("Supprimer tous les étudiants");
                AfficherListeEtudiants(etudiantRepo);
                etudiantRepo.SupprimerTousEtudiants();
                AfficherListeEtudiants(etudiantRepo);

                //Modifier le premier étudiant
                Console.WriteLine("");
                Console.WriteLine("Modifier le premier étudiant");
                marc = new Etudiant() { Nom = "Dubé", Prenom = "Marc", DateNaissance = Convert.ToDateTime("1977-10-10") };
                jean = new Etudiant() { Nom = "Simard", Prenom = "Jean", DateNaissance = Convert.ToDateTime("1981-10-10"), Tuteur = marc };
                etudiantRepo.AjouterEtudiant(marc);
                etudiantRepo.AjouterEtudiant(jean);
                AfficherListeEtudiants(etudiantRepo);
                Console.WriteLine("");
                marc.DateNaissance = Convert.ToDateTime("1999-10-10");
                
                /*********************************************************
                //Ajouter la méthode ModifierEtudiant dans le repository
                *********************************************************/
                //etudiantRepo.ModifierEtudiant(etud);
                AfficherListeEtudiants(etudiantRepo);

                //Trouver le premier étudiant "Simard"
                Console.WriteLine("");
                Console.WriteLine("Trouver le premier étudiant \"Simard\"");
                Etudiant mathieu = new Etudiant() { Nom = "Simard", Prenom = "Mathieu", DateNaissance = Convert.ToDateTime("1979-10-10"), Tuteur = jean };
                Etudiant toto = new Etudiant() { Nom = "Simard", Prenom = "Toto", DateNaissance = Convert.ToDateTime("1987-10-10"), Tuteur = mathieu };
                etudiantRepo.AjouterEtudiant(mathieu);
                etudiantRepo.AjouterEtudiant(toto);
                AfficherListeEtudiants(etudiantRepo);
                Etudiant etudiantSimard = etudiantRepo.TrouverEtudiantParNom("Simard");
                Console.WriteLine("");
                Console.WriteLine(etudiantSimard);
                Console.ReadLine();
                Etudiant etudiantTata = etudiantRepo.TrouverEtudiantParNom("Tata");
                Console.WriteLine("");
                Console.WriteLine(etudiantTata);
                Console.ReadLine();


                //Afficher liste des cours
                Console.WriteLine("");
                Console.WriteLine("Afficher liste des cours");
                
                Cours coursBD = new Cours() { NomCours = "BD", CodeCours = "420-V40" };
                Cours coursWeb = new Cours() { NomCours = "Web", CodeCours = "420-V10" };
                
                InscriptionCours cours = new InscriptionCours() { Etudiant = jean, Cours = coursBD, CodeSession = "H2021" };
                InscriptionCours cours2 = new InscriptionCours() { Etudiant = jean, Cours = coursWeb, CodeSession = "H2021" };
                
                /**************************************************
                //Ajouter la méthode AjouterCours dans le bon repository
                ***************************************************/
                inscCoursRepo.AjouterCours(cours);
                inscCoursRepo.AjouterCours(cours2);
                
                Console.WriteLine("Liste des cours pour l'étudiant " + jean.Nom + ", " + jean.Prenom);
                etudiantRepo.ObtenirListeCours(jean.EtudiantID).ForEach(c => Console.WriteLine("Nom cours: " + c.Cours.NomCours + ", Session: " + c.CodeSession));
                
                
                Console.ReadLine();

                //Liste des étudiants par ordre date naissance
                Console.WriteLine("");
                Console.WriteLine("Afficher étudiants ordre DDN");

                Console.WriteLine("----------------");
                etudiantRepo.ClasserEtudiantsDDN().ForEach(e => Console.WriteLine(e));
                
              foreach (Etudiant e in etudiantRepo.ClasserEtudiantsDDN())
               {
                   Console.WriteLine(e);
               }
                
                Console.ReadKey();
                
            }
        }

        private static void AfficherListeEtudiants(EFEtudiantRepository etudiantRepo)
        {
            //Console.WriteLine("Nombre d'étudiants: " + etudiantRepo.NombreEtudiants());
            etudiantRepo.ObtenirListeEtudiants().ForEach(e => Console.WriteLine(e));

            Console.ReadKey();
        }
        

    }

}

