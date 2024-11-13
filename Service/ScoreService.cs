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
            if (IsContextNull() == true) return null;
            return _context.Scores.ToList();
        } 


        public async Task<Scores?> GetMyScoresAsync(int id)

        {

            if  (IsContextNull() == true) return null;


            return await _context.Scores.FindAsync(id);


        }





    }

}
