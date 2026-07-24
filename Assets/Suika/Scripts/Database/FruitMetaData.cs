using Suika.Scripts.Gameplay;
using UnityEngine;

namespace Suika.Scripts.Database
{
    [System.Serializable]
    public class FruitMetaData
    {
        public Sprite Sprite;
        public FruitType FruitType;
        public FruitType NextFruitType;
    }
}