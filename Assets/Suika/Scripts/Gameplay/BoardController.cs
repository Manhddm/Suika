using System;
using System.Collections.Generic;
using R3;
using Suika.Scripts.Database;
using Suika.Scripts.Factory;
using Suika.Scripts.Gameplay.Fruits;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Suika.Scripts.Gameplay
{
    public class BoardController : MonoBehaviour
    {
        private FruitFactory _fruitFactory;
        private FruitDatabase _fruitDatabase;
        private DisposableBag _disposableBag;
        private HashSet<BaseFruit> _subscribedFruits  = new HashSet<BaseFruit>();
        public void Init(FruitFactory fruitFactory, FruitDatabase fruitDatabase)
        {
            _fruitFactory = fruitFactory;
            _fruitDatabase = fruitDatabase;
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                var fruit = _fruitFactory.Create(FruitType.Blueberry);
                Debug.Log($"Created fruit of type: {fruit.FruitType}, next fruit type: {_fruitDatabase.GetNextFruitType(fruit.FruitType)}");
                fruit.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), 0);
                SubscribeToFruits(fruit);
            }
        }

        private void HandleOnMerge((BaseFruit f1, BaseFruit f2) fruitPair)
        {
            var mergePosition =
                (fruitPair.f1.transform.position + fruitPair.f2.transform.position) / 2;

            var nextFruitType =
                _fruitDatabase.GetNextFruitType(fruitPair.f1.FruitType);

            fruitPair.f1.ReturnToPool();
            fruitPair.f2.ReturnToPool();

            if (nextFruitType == FruitType.None)
                return;

            var newFruit = _fruitFactory.Create(nextFruitType);
            SubscribeToFruits(newFruit);
            newFruit.transform.position = mergePosition;
        }

        private void SubscribeToFruits(BaseFruit fruit)
        {
            if (!_subscribedFruits.Add(fruit))
                return;
            fruit.OnMergeCommand.Subscribe(HandleOnMerge).AddTo(ref _disposableBag);
        }

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}