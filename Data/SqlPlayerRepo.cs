using System.Collections.Generic;
using System.Linq;
using System;
using Saitynai_REST_L01.Models;
using Saitynai_REST_L01.Data;

namespace Saitynai_REST_L01.Data
{
    public class SqlPlayerRepo : IPlayerRepo
    {
        private readonly PlayerContext _context;

        public SqlPlayerRepo(PlayerContext context)
        {
             _context = context;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Player.ToList();
        }

        public Player GetPlayerById(int id)
        {
            return _context.Player.FirstOrDefault(p => p.id == id);
        }

        public void CreatePlayer(Player cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Player.Add(cmd);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0 );
        }

        public void UpdatePlayer(Player cmd)
        {
            //Nothing
        }

        public void DeletePlayer(Player cmd)
        {
             if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Player.Remove(cmd);
        }
    }
}