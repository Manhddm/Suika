using System;
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
            base.OnRent(instance);
            instance.IsMerging = false;
            instance.PhysicsBody.simulated = false;
        }

        protected override void OnReturn(BaseFruit instance)
        {
            instance.transform.position = Vector3.zero;
            base.OnReturn(instance);
        }
    }
}
