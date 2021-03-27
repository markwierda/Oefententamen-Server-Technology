using Oefententamen.Data;
using System.Linq;

namespace Oefententamen.Models
{
    public static class SeedData
    {
        public static void Init(TheaterDbContext context)
        {
            context.Database.EnsureCreated();

            AddKlant(context, "Pieter Baan", "Dorpstraat 1", "Nudorp", "pieterbaan@centrum.nl");
            AddKlant(context, "Sjoukje van Leeuwen", "Brink 32", "Dwingeloo", "leewerik@live.nl");
            AddKlant(context, "Maria van der Voorst", "Energieweg 23", "Eindhoven", "maria@ikke.com");
            AddKlant(context, "Ad Veenstra", "Amersfoortseweg 12A", "Hilversum", "venie@utc.org");

            string[] rows = { "A", "B", "C", "D", "E" };
            int chairsPerRow = 6;
            int id = 1;
            foreach (string row in rows)
            {
                for (int i = 1; i <= chairsPerRow; i++)
                {
                    AddReservering(context, $"{row}{i}");
                    id++;
                }
            }

            context.SaveChanges();
        }

        private static void AddKlant(TheaterDbContext context, string naam, string adres, string woonplaats, string email)
        {
            Klant klant = context.Klant.FirstOrDefault(k => k.Naam.Equals(naam));

            if (klant == null)
            {
                context.Klant.Add(new Klant
                {
                    Naam = naam,
                    Adres = adres,
                    Woonplaats = woonplaats,
                    Email = email
                });
            }
        }

        private static void AddReservering(TheaterDbContext context, string naam)
        {
            Reservering reservering = context.Reservering.FirstOrDefault(r => r.Naam.Equals(naam));

            if (reservering == null)
            {
                context.Reservering.Add(new Reservering
                {
                    Naam = naam
                });
            }
        }
    }
}