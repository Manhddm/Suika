using R3;

namespace Suika.Scripts.Gameplay
{
    public class GameplayContext
    {
        public ReactiveProperty<int> Score { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> HighScore { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<FruitType> NextFruitType { get; } = new ReactiveProperty<FruitType>(FruitType.None);
        public ReactiveProperty<bool> IsGameOver { get; } = new ReactiveProperty<bool>(false);
    }
}