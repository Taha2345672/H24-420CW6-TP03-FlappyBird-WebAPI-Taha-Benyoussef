using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoresController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scores>>> GetPublicMyScores()
        {
            IEnumerable<Scores>? scores = await _scoreService.GetPublicScoresAsync();

            if (scores == null) { return StatusCode(StatusCodes.Status500InternalServerError); }

            return Ok(await _scoreService.GetPublicScoresAsync());

        }

        // GET: api/Scores/5
        [HttpGet]
        public async Task<ActionResult<Scores>> GetMyScores(int id)
        {
            Scores? scores = await _scoreService.GetMyScoresAsync(id);
            return scores == null ? NotFound() : scores;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeScoreVisibility(int id, Scores scores)
        {
            if (id != scores.Id)
            {
                return BadRequest();
            }

          //  _scoreService.Entry(scores).State = EntityState.Modified;

            try
            {
               // await _scoreService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               //// if (!ScoresExists(id))
               // {
               //     return NotFound();
               // }
               // else
               // {
               //     throw;
               // }
            }

            return NoContent();
        }

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Scores>> PostScore(Scores scores)
        {
         //  _scoreService.Scores.Add(scores);
           // await _scoreService.SaveChangesAsync();

            return CreatedAtAction("GetScores", new { id = scores.Id }, scores);
        }

       //private bool ScoresExists(int id)
       // {
           // return _scoreService.Scores.Any(e => e.Id == id);
        }
    }
//}
