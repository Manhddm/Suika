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
        [SerializeField] private List<FruitType> values = new();
        private Dictionary<FruitType, FruitType> _fruitDictionary = new();

        private void OnEnable()
        {
            GenerateDictionary();
        }
        public FruitType GetNextFruitType(FruitType currentFruitType)
        {
            if (_fruitDictionary.TryGetValue(currentFruitType, out var nextFruitType))
            {
                return nextFruitType;
            }
            return FruitType.None;
        }
        private void GenerateDictionary()
        {
            _fruitDictionary.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == FruitType.Watermelon)
                {
                    _fruitDictionary[keys[i]] = FruitType.None;
                    continue;
                }
                _fruitDictionary[keys[i]] = values[i];
            }
        }
        [ContextMenu("Auto-Generate")]
        private void AutoGen()
        {
            FruitType[] fruitTypes =
                (FruitType[])Enum.GetValues(typeof(FruitType));
            for (int i = 1; i < fruitTypes.Length; i++)
            {
                if (!keys.Contains(fruitTypes[i]))
                {
                    keys.Add(fruitTypes[i]);
                }
                if (i+1 < fruitTypes.Length && !values.Contains(fruitTypes[i + 1]))
                {
                    values.Add(fruitTypes[i + 1]);
                }
            }
            GenerateDictionary();
        }
    }
}
