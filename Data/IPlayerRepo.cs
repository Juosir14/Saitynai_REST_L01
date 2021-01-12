using System.Collections.Generic;
using Saitynai_REST_L01.Models;

namespace Saitynai_REST_L01.Data
{
    public interface IPlayerRepo
    {
        bool SaveChanges();

        IEnumerable<Player> GetAllPlayers();
        Player GetPlayerById(int id);

        void CreatePlayer(Player cmd);
        void UpdatePlayer(Player cmd);
        void DeletePlayer(Player cmd);
    }
}