using System;
using System.Collections.Generic;
using Suika.Scripts.Gameplay;
using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.Gameplay.Pool;
using Suika.Scripts.Pool;
using UnityEngine;

namespace Suika.Scripts.Factory
{
    public class FruitFactory : MonoBehaviour, IFactory<FruitType, BaseFruit>
    {
        private Dictionary<FruitType, FruitPool> _fruitPools = new Dictionary<FruitType, FruitPool>();
        public void Init(List<BaseFruit> fruitPrefabs)
        {
            foreach (var fruitPrefab in fruitPrefabs)
            {
                var pool = new FruitPool(fruitPrefab);
                _fruitPools.TryAdd(fruitPrefab.FruitType, pool);
            }
        }
        public BaseFruit Create(FruitType type)
        {
            var fruit = _fruitPools[type].Rent();
            return fruit;
        }
    }
}