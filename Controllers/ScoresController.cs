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
        [HttpGet("{userName}")]
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

            var result = await _scoreService.ChangeScoreVisibilityAsync(id, scores);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Scores>> PostScore(Scores scores)
        {
            var createdScore = await _scoreService.AddScoreAsync(scores);
            if (createdScore == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetMyScores), new { id = createdScore.Id }, createdScore);
        }

        //private bool ScoresExists(int id)
        //{
        //    return _scoreService.sc.Any(e => e.Id == id);
        //}

    }
}
