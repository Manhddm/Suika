using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.Pool;
using UnityEngine;

namespace Suika.Scripts.Gameplay.Pool
{
    public class FruitPool : BaseObjectPool<BaseFruit>
    {
        public FruitPool(BaseFruit prefab) : base(prefab)
        {
        }

        protected override void OnRent(BaseFruit instance)
        {
            instance.IsMerging = false;
        }

        protected override void OnReturn(BaseFruit instance)
        {
            instance.IsMerging = false;
            instance.transform.position = Vector3.zero;
        }
    }
}