using System;
using System.Collections.Generic;
using R3;
using Suika.Scripts.Database;
using Suika.Scripts.Factory;
using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.Input;
using UnityEngine;

namespace Suika.Scripts.Gameplay
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private Transform fruitSpawnPoint;
        private FruitFactory _fruitFactory;
        private FruitDatabase _fruitDatabase;
        private InputController _inputController;
        private Camera _mainCamera;

        private DisposableBag _disposableBag;
        private HashSet<BaseFruit> _subscribedFruits = new HashSet<BaseFruit>();
        private BaseFruit _currentFruit;
        public ReactiveProperty<FruitType> NextFruitType { get; } = new();

        public void Init(FruitFactory fruitFactory, FruitDatabase fruitDatabase, InputController inputController)
        {
            _fruitFactory = fruitFactory;
            _fruitDatabase = fruitDatabase;
            _inputController = inputController;
            _mainCamera = Camera.main;
            Setup();
        }

        private void Setup()
        {
            NextFruitType.Value = _fruitDatabase.GetRandomFruitType();
            PickUpFruit();
            _inputController.TouchBeganCommand
                .Subscribe(MoveFruit)
                .AddTo(ref _disposableBag);
            _inputController.TouchMovedCommand
                .Subscribe(MoveFruit)
                .AddTo(ref _disposableBag);
            _inputController.TouchEndedCommand
                .Subscribe(MoveFruit)
                .AddTo(ref _disposableBag);
            _inputController.TouchEndedCommand
                .Subscribe(_ => DropFruit())
                .AddTo(ref _disposableBag);
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

            var newFruit = _fruitFactory.Create(nextFruitType, mergePosition);
            newFruit.PhysicsBody.simulated = true;
            SubscribeToFruits(newFruit);
        }

        private void SubscribeToFruits(BaseFruit fruit)
        {
            if (!_subscribedFruits.Add(fruit))
                return;
            fruit.OnMergeCommand.Subscribe(HandleOnMerge).AddTo(ref _disposableBag);
        }

        #region Gameplay

        private void DropFruit()
        {
            _currentFruit.PhysicsBody.simulated = true;
            PickUpFruit();
        }

        private void PickUpFruit()
        {
            _currentFruit = _fruitFactory.Create(NextFruitType.Value, fruitSpawnPoint.position);
            _currentFruit.PhysicsBody.simulated = false;
            SubscribeToFruits(_currentFruit);
            NextFruitType.Value = _fruitDatabase.GetRandomFruitType();
        }

        private void MoveFruit(Vector2 position)
        {
            if (_currentFruit == null || _mainCamera == null)
                return;

            var worldPosition = _mainCamera.ScreenToWorldPoint(
                new Vector3(position.x, position.y, -_mainCamera.transform.position.z));
            worldPosition.y = fruitSpawnPoint.position.y;
            worldPosition.z = fruitSpawnPoint.position.z;
            _currentFruit.transform.position = worldPosition;
        }

        #endregion

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}
