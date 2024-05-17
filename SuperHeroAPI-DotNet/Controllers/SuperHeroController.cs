using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet.Data;
using SuperHeroAPI_DotNet.Entities;

namespace SuperHeroAPI_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _contex;
        public SuperHeroController(DataContext contex)
        {
            _contex = contex;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heroes = await _contex.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var heroes = await _contex.SuperHeroes.FindAsync(id);
            if(heroes == null)
            {
                return BadRequest("Heroi não encontrado.");
            }
            return Ok(heroes);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _contex.SuperHeroes.Add(hero);
            await _contex.SaveChangesAsync();
            return Ok(await _contex.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var dbHero = await _contex.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbHero == null)
            {
                return BadRequest("Heroi não encontrado.");
            }
            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _contex.SaveChangesAsync();

            return Ok(await _contex.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id)
        {
            var heroes = await _contex.SuperHeroes.FindAsync(id);
            if (heroes == null)
            {
                return BadRequest("Heroi não encontrado.");
            }
            _contex.SuperHeroes.Remove(heroes);
            await _contex.SaveChangesAsync();
            return Ok(heroes);
        }
    }
}
