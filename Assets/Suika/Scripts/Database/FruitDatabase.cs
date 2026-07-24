using System;
using System.Collections.Generic;
using Suika.Scripts.Gameplay;
using UnityEngine;

namespace Suika.Scripts.Database
{
    [CreateAssetMenu(fileName = "FruitDatabase", menuName = "Suika/Fruit Database", order = 0)]
    public class FruitDatabase : ScriptableObject
    {
        [SerializeField] private List<FruitType> keys = new();
        [SerializeField] private List<FruitMetaData> values = new();
        private Dictionary<FruitType, FruitMetaData> _fruitDictionary = new();

        private FruitType[] _fruitTypesRandom = new[]
        {
            FruitType.Blueberry,
            FruitType.Cherry,
            FruitType.Lime,
            FruitType.Plum,
            FruitType.Lemon
        };

        private void OnEnable()
        {
            GenerateDictionary();
        }
        public FruitType GetNextFruitType(FruitType currentFruitType)
        {
            if (_fruitDictionary.TryGetValue(currentFruitType, out var fruitData))
            {
                return fruitData.NextFruitType;
            }
            return FruitType.Blueberry;
        }
        public FruitMetaData GetFruitData(FruitType fruitType)
        {
            if (_fruitDictionary.TryGetValue(fruitType, out var fruitData))
            {
                return fruitData;
            }
            throw new Exception($"Fruit type {fruitType} not found in the database.");
        }

        public FruitType GetRandomFruitType()
        {
            return _fruitTypesRandom[UnityEngine.Random.Range(0, _fruitTypesRandom.Length)];
        }
        private void GenerateDictionary()
        {
            _fruitDictionary.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == FruitType.None) continue;
                _fruitDictionary[keys[i]] = values[i];
            }
        }
    }
}
