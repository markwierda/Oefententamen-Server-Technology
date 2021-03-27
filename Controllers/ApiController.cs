using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oefententamen.Data;
using Oefententamen.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oefententamen.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly TheaterDbContext _context;

        public ApiController(TheaterDbContext context)
        {
            _context = context;
        }

        // GET: api/5
        [HttpGet("{id}")]
        public int BerekenPrijs(int id)
        {
            var klant = _context.Klant.Include(k => k.Reserveringen).FirstOrDefault(k => k.Id == id);
            int prijs = 0;

            foreach (Reservering reservering in klant.Reserveringen)
            {
                if (reservering.Naam.StartsWith("E")) prijs += 20;
                else if (reservering.Naam.StartsWith("D")) prijs += 25;
                else if (reservering.Naam.StartsWith("C")) prijs += 30;
                else if (reservering.Naam.StartsWith("B")) prijs += 35;
                else if (reservering.Naam.StartsWith("A")) prijs += 40;

                if (reservering.Naam.EndsWith("3") || reservering.Naam.EndsWith("4")) prijs += 5;
            }

            return prijs;
        }
    }
}