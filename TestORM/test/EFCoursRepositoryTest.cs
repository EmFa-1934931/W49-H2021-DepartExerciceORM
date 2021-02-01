using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;
using Xunit;

namespace TestORMCodeFirst.DAL
{
    public class EFCoursRepositoryTest
    {
        private EFEtudiantRepository repoEtu;
        private EFInscCoursRepository repoInsc;
        private EFCoursRepository repoCours;

        private void SetUp()
        {
            var builder = new DbContextOptionsBuilder<CegepContext>();
            builder.UseInMemoryDatabase(databaseName: "testEtudiant_db");   // Database en mémoire
            var contexte = new CegepContext(builder.Options);
            repoEtu = new EFEtudiantRepository(contexte);
            repoInsc = new EFInscCoursRepository(contexte);
            repoCours = new EFCoursRepository(contexte);
        }

        [Fact]
        public void UnCoursEstCorrectementAjoute()
        {
            //Arrange
            SetUp();
            Cours cours = new Cours
            {
                CodeCours = "420-W49-SF",
                NomCours = "Base de Données II"
            };

            repoCours.AjouterCours(cours.CodeCours, cours.NomCours);

            //Act
            var result = repoCours.ObtenirListeCours().Count();

            //Assert
            Assert.Equal(expected: 1, actual: result);
        }

        [Fact]
        public void DeuxCoursSontCorrectementAjoutes()
        {
            //Arrange
            SetUp();
            Cours cours1 = new Cours
            {
                CodeCours = "W49",
                NomCours = "Base de Données II"
            };

            Cours cours2 = new Cours
            {
                CodeCours = "W4A",
                NomCours = "Infrastructures technologiques"
            };

            repoCours.AjouterCours(cours1.CodeCours, cours1.NomCours);
            repoCours.AjouterCours(cours2.CodeCours, cours2.NomCours);

            //Act
            var result = repoCours.ObtenirListeCours().Count();

            //Assert
            Assert.Equal(expected: 2, actual: result);
        }

        [Fact]
        public void LeCoursAjouteALesBonnesInformations()
        {
            //Arrange
            SetUp();
            Cours cours = new Cours
            {
                CodeCours = "420-W49-SF",
                NomCours = "Base de Données II"
            };

            //Act
            repoCours.AjouterCours(cours.CodeCours, cours.NomCours);

            //Assert
            var result = this.repoCours.ObtenirListeCours();
            Assert.Single(result);
            Assert.Same(cours.CodeCours, result.First().CodeCours);
            Assert.Same(cours.NomCours, result.First().NomCours);
            Assert.Same(cours.Inscriptions, result.First().Inscriptions);
        }
    }
}
