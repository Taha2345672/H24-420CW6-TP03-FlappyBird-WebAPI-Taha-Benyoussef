using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class ScoreService
    {
        protected readonly WebAPIContext _context;

        public ScoreService(WebAPIContext context)
        {
            _context = context;
        }

        private bool IsContextNull()
        {
            return _context == null || _context.Scores == null;
        }

        // GET Service
        public async Task<IEnumerable<Scores>?> GetPublicScoresAsync()
        {
            if (IsContextNull()) return null;
            return await _context.Scores.ToListAsync();
        }

        public async Task<Scores?> GetMyScoresAsync(int id)
        {
            if (IsContextNull()) return null;
            return await _context.Scores.FindAsync(id);
        }

        // Change the visibility of a score
        public async Task<bool> ChangeScoreVisibilityAsync(int id, Scores scores)
        {
            if (IsContextNull() || id != scores.Id)
                return false;

            var existingScore = await _context.Scores.FindAsync(id);
            if (existingScore == null)
                return false;

            existingScore.IsPublic = scores.IsPublic; 
            _context.Entry(existingScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        // Add a new score
        public async Task<Scores?> AddScoreAsync(Scores scores)
        {
            if (IsContextNull()) return null;

            _context.Scores.Add(scores);
            await _context.SaveChangesAsync();

            return scores;
        }

        
        public bool ScoresExists(int id)
        {
            return !IsContextNull() && _context.Scores.Any(e => e.Id == id);
        }
    }
}
