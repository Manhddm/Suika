using System;
namespace Suika.Scripts
{
    public static class GameEvent
    {
        public static event Action GameOver;

        public static void OnGameOver()
        {
            GameOver?.Invoke();
        }
    }
}