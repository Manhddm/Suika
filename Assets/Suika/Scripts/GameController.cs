using System.Collections.Generic;
using Suika.Scripts.Database;
using Suika.Scripts.Factory;
using Suika.Scripts.Gameplay;
using Suika.Scripts.Gameplay.Fruits;
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

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            fruitFactory.Init(fruitPrefabs);
            boardController.Init(fruitFactory, fruitDatabase);
        }
    }
}