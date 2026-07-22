using System;
using Suika.Scripts.Factory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Suika.Scripts.Gameplay
{
    public class BoardController : MonoBehaviour
    {
        private FruitFactory _fruitFactory;
        public void Init(FruitFactory fruitFactory)
        {
            _fruitFactory = fruitFactory;
        }

        // private void Update()
        // {
        //     if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //     {
        //         var fruit = _fruitFactory.Create(FruitType.Apple);
        //         fruit.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), 0);
        //     }
        // }
    }
}