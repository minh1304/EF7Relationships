﻿using EF7Relationships.Data;
using EF7Relationships.DTOs;
using EF7Relationships.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF7Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TlouController : ControllerBase
    {
        private readonly DataContext _context;

        public TlouController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var character = await _context.Characters
               .Include(c => c.Backpack)
               .Include(c => c.Weapons)
               .Include(c => c.Factions)
               .FirstOrDefaultAsync(c => c.Id == id);
            return Ok(character);
        }


        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDTO request)
        {
            var newCharacter = new Character
            {
                Name = request.Name
            };
            var backpack = new Backpack
            {
                Decription = request.Backpack.Decription,
                Character = newCharacter
            };
            var weaspons = request.Weapons.Select(w => new Weapon
            {
                Name = w.Name,
                Character = newCharacter
            }).ToList();
            var factions = request.Factions.Select(f => new Faction
            {
                Name = f.Name,
                Characters = new List<Character>
                {
                    newCharacter
                }
            }).ToList();
            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weaspons;
            newCharacter.Factions = factions;
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            return Ok(await _context.Characters.Include(c => c.Backpack).Include(c => c.Weapons).Include(c => c.Factions).ToListAsync());

        }
    }
}
