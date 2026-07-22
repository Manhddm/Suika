using System;
using R3;
using Suika.Scripts.Pool;
using UnityEngine;

namespace Suika.Scripts.Gameplay.Fruits
{
    public abstract class BaseFruit : MonoBehaviour, IReturnable<BaseFruit>
    {
        [SerializeField] private FruitType fruitType;
        [SerializeField] private FruitType nextFruitType;
        public bool IsMerging { get; set;}
        public ReactiveCommand<(BaseFruit fruit1, BaseFruit fruit2)> OnMergeCommand { get; } = new ReactiveCommand<(BaseFruit fruit1, BaseFruit fruit2)>();
        
        public FruitType FruitType => fruitType;
        public FruitType NextFruitType => nextFruitType;
        public event Action<BaseFruit> ReturnHandler;

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BaseFruit>(out var otherFruit))
            {
                if (otherFruit.FruitType == this.fruitType && !IsMerging && !otherFruit.IsMerging)
                {
                    IsMerging = true;
                    otherFruit.IsMerging = true;
                    OnMergeCommand.Execute((this, otherFruit));
                }
            }
        }

        public void ReturnToPool()
        {
            ReturnHandler?.Invoke(this);
        }

        
    }
}