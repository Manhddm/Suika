using UnityEngine;
using UnityEngine.SceneManagement;

namespace Suika.Scripts
{
    public sealed class SceneFlowController : MonoBehaviour
    {
        [SerializeField] private string gameplaySceneName = "Gameplay";

        public void RestartGameplay()
        {
            SceneManager.LoadSceneAsync(
                gameplaySceneName,
                LoadSceneMode.Single);
        }
    }
}
