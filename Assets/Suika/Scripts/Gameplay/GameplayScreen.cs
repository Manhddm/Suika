using R3;
using Suika.Scripts.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Suika.Scripts.Gameplay
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField] private Image nextFruitImage;
        [SerializeField] private TMP_Text scoreText;
        // [SerializeField] private Text highScoreText;
        [SerializeField] private Button settingButton;
        private FruitDatabase _fruitDatabase;
        private FruitType _nextFruitType;
        private DisposableBag _disposableBag;
        public void Initialize(GameplayContext context, FruitDatabase fruitDatabase)
        {
            _fruitDatabase = fruitDatabase;
            context.NextFruitType.Subscribe(nextFruitType =>
            {
                _nextFruitType = nextFruitType;
                UpdateNextFruitImage();
            }).AddTo(ref _disposableBag);
        }
        private void UpdateNextFruitImage()
        {
            if (_nextFruitType == FruitType.None)
            {
                nextFruitImage.sprite = null;
                nextFruitImage.color = Color.clear;
            }
            else
            {
                var fruitData = _fruitDatabase.GetFruitData(_nextFruitType);
                nextFruitImage.sprite = fruitData.Sprite;
            }
        }
        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}