using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Suika.Scripts.GameUI
{
    public sealed class PopupService : MonoBehaviour
    {
        [SerializeField] private Canvas rootCanvas;

        public async UniTask<GameObject> Show(string key)
        {
            if (rootCanvas == null)
            {
                Debug.LogError("PopupService requires a root Canvas.", this);
                return null;
            }

            return await Addressables.InstantiateAsync(
                key,
                rootCanvas.transform,
                false);
        }

        public void Close(GameObject instance)
        {
            if (instance == null)
                return;

            Addressables.ReleaseInstance(instance);
        }
    }
}
