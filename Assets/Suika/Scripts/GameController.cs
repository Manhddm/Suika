using System;
using System.Collections.Generic;
using R3;
using Suika.Scripts.Database;
using Suika.Scripts.Factory;
using Suika.Scripts.Gameplay;
using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.Input;
using Suika.Scripts.Pool;
using UnityEngine;

namespace Suika.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BoardController boardController;
        [SerializeField] private FruitFactory fruitFactory;
        [SerializeField] private List<BaseFruit> fruitPrefabs;
        [SerializeField] private FruitDatabase fruitDatabase;
        [SerializeField] private InputController inputController;
        [SerializeField] private GameplayScreen gameplayScreen;
        private GameplayContext _gameplayContext;
        private DisposableBag _disposableBag;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            SetupGame();
        }

        private void Init()
        {
            _gameplayContext = new GameplayContext();
            fruitFactory.Init(fruitPrefabs);
            boardController.Init(fruitFactory, fruitDatabase, inputController);
            boardController.NextFruitType.Subscribe(nextFruitType =>
            {
                _gameplayContext.NextFruitType.Value = nextFruitType;
            }).AddTo(ref _disposableBag);
            
            gameplayScreen.Initialize(_gameplayContext, fruitDatabase);
        }

        private void SetupGame()
        {
            GameEvent.GameOver += LostGame;
        }

        private void LostGame()
        {
            Debug.Log("<color=#ff8282>Game Over</color>");
        }
        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}