using Suika.Scripts.Gameplay.Fruits;
using UnityEngine;

namespace Suika.Scripts.Gameplay
{
    public class TriggerLostGame : MonoBehaviour
    {
        [SerializeField] private LayerMask fruitLayer;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Fruit"))
            {
                var fruit = collision.gameObject.GetComponent<BaseFruit>();
                if (fruit.IsInBox)
                {
                    GameEvent.OnGameOver();
                }
            }
        }
        
    }
}