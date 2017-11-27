namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LibraryProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryProject.Models._Models>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryProject.Models._Models context)
        {
            if(context.Autors.Count() > 0)
            {
                return;
            }
            dynamic table = new object[]
            {
                new {Name = "Adam", Surname = "Malec" },
                new {Name = "Arek", Surname = "T³ustowski" },
                new {Name = "Artur", Surname = "Kasprzyk" },
                new {Name = "Bartek", Surname = "Czarnota" },
                new {Name = "Bartosz", Surname = "Krawczyk" },
                new {Name = "Bruno", Surname = "Dzikowski" },
                new {Name = "Damian", Surname = "Zborowski" },
                new {Name = "Daniel", Surname = "Talarek" },
                new {Name = "Dominik", Surname = "Liszka" },
                new {Name = "Filip", Surname = "Bañdur" },
                new {Name = "Kacper", Surname = "Hankiewicz" },
                new {Name = "Kacper", Surname = "Zagmunt" },
                new {Name = "Kamil", Surname = "Bu³at" },
                new {Name = "Kamil", Surname = "Jamróz" },
                new {Name = "Kamil", Surname = "Korczyñski" },
                new {Name = "Karen", Surname = "Barseghyan "},
                new {Name = "Karol", Surname = "Pieprzak" },
                new {Name = "Karol", Surname = "Szarek" },
                new {Name = "Krzysztof", Surname = "Ostachowski" },
                new {Name = "Krzysztof", Surname = "Soczyñski" },
                new {Name = "Kuba", Surname = "Górka" },
                new {Name = "Kuba", Surname = "Piec" },
                new {Name = "Mateusz", Surname = "Borzêcki" },
                new {Name = "Mateusz", Surname = "Witkowski" },
                new {Name = "Miko³aj", Surname = "Baran" },
                new {Name = "Pawe³", Surname = "Dobija" },
                new {Name = "Przemek", Surname = "Rzepecki" },
                new {Name = "Rafa³", Surname = "Mardy³a" },
                new {Name = "Rafa³", Surname = "Kurek" },
                new {Name = "Rafa³", Surname = "Pokrywka" },
                new {Name = "Szymon", Surname = "Byk" },
                new {Name = "Szymon", Surname = "Lorenc" },
                new {Name = "Szymon", Surname = "Szpaczek" },
                new {Name = "Wojciech", Surname = "Nowicki" },
                new {Name = "Wojciech", Surname = "Windak" }
            };
            foreach(dynamic obj in table)
            {
                Author newAut = new Author();
                newAut.Name = obj.Name;
                newAut.Surname = obj.Surname;
                context.Autors.Add(newAut);
            }
            try
            {
                context.SaveChanges();
            }
            catch
            {
                
            }
        }
    }
}
