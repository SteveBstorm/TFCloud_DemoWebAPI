using DemoWebAPI.Models;

namespace DemoWebAPI.Tools
{
    public static class MesMappers
    {
        public static Player Mapper(this PlayerFormDTO form)
        {
            return new Player
            {
                Email = form.Email,
                Nickname = form.Nickname
            };
        }
    }
}
