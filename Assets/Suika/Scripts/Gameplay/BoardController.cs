using System;
using Suika.Scripts.Database;
using Suika.Scripts.Factory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Suika.Scripts.Gameplay
{
    public class BoardController : MonoBehaviour
    {
        private FruitFactory _fruitFactory;
        private FruitDatabase _fruitDatabase;
        public void Init(FruitFactory fruitFactory, FruitDatabase fruitDatabase)
        {
            _fruitFactory = fruitFactory;
            _fruitDatabase = fruitDatabase;
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                var fruit = _fruitFactory.Create(FruitType.Apple);
                Debug.Log($"Created fruit of type: {fruit.FruitType}, next fruit type: {_fruitDatabase.GetNextFruitType(fruit.FruitType)}");
                fruit.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), 0);
            }
        }
    }
}