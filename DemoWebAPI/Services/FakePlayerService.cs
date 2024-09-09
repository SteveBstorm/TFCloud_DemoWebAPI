using DemoWebAPI.Models;

namespace DemoWebAPI.Services
{
    public static class FakePlayerService
    {
        public static List<Player> Liste { get; set; } = new List<Player>()
        {
            new Player
            {
                Id = 1,
                Nickname = "Elean",
                Email = "steve.lorent@bstorm.be"
            }
        };

    }
}
