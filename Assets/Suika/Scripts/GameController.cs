using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using Suika.Scripts.Database;
using Suika.Scripts.Factory;
using Suika.Scripts.Gameplay;
using Suika.Scripts.Gameplay.Fruits;
using Suika.Scripts.GameUI;
using Suika.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private PopupService popupService;
        [SerializeField] private SceneFlowController sceneFlowController;
        private GameplayContext _gameplayContext;
        private DisposableBag _disposableBag;
        private GameOver _gameOverPopup;
        private bool _isGameOverShown;
        private bool _isRestarting;
        public bool IsInitialized { get; private set;}

        private void Awake()
        {
            IsInitialized = false;
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
            IsInitialized = true;
        }

        private void SetupGame()
        {
            GameEvent.GameOver += LostGame;
        }

        private void LostGame()
        {
            HandleGameOver(_gameplayContext.Score.Value).Forget();
        }

        private async UniTask HandleGameOver(int score = 0)
        {
            if (_isGameOverShown)
                return;

            if (popupService == null)
            {
                Debug.LogError("GameController requires a PopupService.", this);
                return;
            }

            _isGameOverShown = true;

            GameObject obj;
            try
            {
                obj = await popupService.Show("GameOver");
            }
            catch (Exception exception)
            {
                _isGameOverShown = false;
                Debug.LogException(exception, this);
                return;
            }

            if (obj == null)
            {
                _isGameOverShown = false;
                return;
            }

            var gameOver = obj.GetComponent<GameOver>();
            if (gameOver == null)
            {
                _isGameOverShown = false;
                Debug.LogError("GameOver popup prefab is missing the GameOver component.", obj);
                popupService.Close(obj);
                return;
            }

            _gameOverPopup = gameOver;
            gameOver.Initialize(score);
            gameOver.RetryClickedCommand
                    .Subscribe(_ => RestartGame())
                    .AddTo(ref _disposableBag);
        }

        private void RestartGame()
        {
            if (_isRestarting)
                return;

            if (sceneFlowController == null)
            {
                Debug.LogError("GameController requires a SceneFlowController.", this);
                return;
            }

            _isRestarting = true;

            if (_gameOverPopup != null)
            {
                popupService.Close(_gameOverPopup.gameObject);
                _gameOverPopup = null;
            }

            sceneFlowController.RestartGameplay();
        }
        private void OnDestroy()
        {
            GameEvent.GameOver -= LostGame;
            _disposableBag.Dispose();
        }
        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.capsLockKey.wasPressedThisFrame)
            {
                GameEvent.OnGameOver();
            }
        }
    }
}
