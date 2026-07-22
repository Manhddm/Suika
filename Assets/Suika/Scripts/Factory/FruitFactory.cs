using System.Collections.Generic;
using Suika.Scripts.Gameplay;
using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.Pool;
using UnityEngine;

namespace Suika.Scripts.Factory
{
    public class FruitFactory : MonoBehaviour, IFactory<FruitType, BaseFruit>
    {
        private Dictionary<FruitType, BaseObjectPool<BaseFruit>> fruitPools = new Dictionary<FruitType, BaseObjectPool<BaseFruit>>();
        public void Init(List<BaseFruit> fruitPrefabs)
        {
            foreach (var fruitPrefab in fruitPrefabs)
            {
                var pool = new BaseObjectPool<BaseFruit>(fruitPrefab);
                fruitPools.Add(fruitPrefab.FruitType, pool);
            }
        }
        public BaseFruit Create(FruitType type)
        {
            var fruit = fruitPools[type].Rent();
            return fruit;
        }
    }
}