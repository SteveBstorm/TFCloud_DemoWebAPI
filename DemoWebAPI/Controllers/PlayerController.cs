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
            if (p is null)
                return NotFound("Joueur introuvable");
            return Ok(p);
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            FakePlayerService.Liste.Add(player);
            return Ok("joueur créé avec succès");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Player playerToDelete = FakePlayerService.Liste.FirstOrDefault(x => x.Id == id);

            if (playerToDelete is null)
                return NotFound("Joueur non trouvé");

            FakePlayerService.Liste.Remove(playerToDelete);
            return Ok("Suppression OK");
        }


        //Identifier la provenance de l'info
        //Header => Info sur l'envoyeur ET sur le contenu de la requête
        //Body => Contenu de la requête (Objet / Fichier / autre)
        //Route => Info contenue dans l'url 
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Player player)
        {
            try
            {
                FakePlayerService.Liste.First(x => x.Id == id).Email = player.Email;
                FakePlayerService.Liste.First(x => x.Id == id).Nickname = player.Nickname;
                return Ok("Update effectué");
            }
            catch (Exception ex)
            {
                return BadRequest("Joueur inéxistant");
            }
        }
    }
}
