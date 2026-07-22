using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.Pool;

namespace Suika.Scripts.Gameplay.Pool
{
    public class ApplePool : BaseObjectPool<BaseFruit>
    {
        public ApplePool(BaseFruit prefab) : base(prefab)
        {
        }
    }
}