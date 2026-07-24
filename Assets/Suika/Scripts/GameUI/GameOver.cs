using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Suika.Scripts.GameUI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button retryButton;
        public ReactiveCommand<Unit> RetryClickedCommand { get;} = new ReactiveCommand<Unit>();

        public void Initialize(int score)
        {
            retryButton.onClick.AddListener(() =>
            {
                RetryClickedCommand.Execute(Unit.Default);
            });
            scoreText.text = $"Score: {score}";
        }

        public void OnDestroy()
        {
            retryButton.onClick.RemoveAllListeners();
        }
    }
}