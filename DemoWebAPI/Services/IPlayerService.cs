using DemoWebAPI.Models;

namespace DemoWebAPI.Services
{
    public interface IPlayerService
    {
        bool Create(Player player);
        void Delete(int id);
        List<Player> GetAll();
        Player? GetById(int id);
        void Update(Player player);
    }
}