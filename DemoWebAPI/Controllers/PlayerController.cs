using DemoWebAPI.Models;
using DemoWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        //public List<Player> Liste { get; set; }

        public PlayerController()
        {
            //Liste = new List<Player>();
            //Liste.Add(new Player
            //{
            //    Id = 1,
            //    Nickname = "Elean",
            //    Email = "steve.lorent@bstorm.be"
            //});
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return StatusCode(200, "Arthur");
            //return NotFound();
            return Ok(FakePlayerService.Liste);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Player p = FakePlayerService.Liste.FirstOrDefault(p => p.Id == id);
            if (p is null) return NotFound("Joueur introuvable");
            return Ok(p);
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            FakePlayerService.Liste.Add(player);
            return Ok("joueur créé avec succès");
        }
    }
}
