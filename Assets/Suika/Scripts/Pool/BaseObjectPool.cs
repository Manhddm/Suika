using UnityEngine;
using uPools;

namespace Suika.Scripts.Pool
{
    public class BaseObjectPool<T> : ObjectPoolBase<T> where T : Component, IReturnable<T>
    {
        private readonly T _prefab;
        public Transform PoolParent { get; private set; }
        public BaseObjectPool(T prefab)
        {
            _prefab = prefab;
            var gameObject = new GameObject($"{prefab.name} Pool");
            gameObject.SetActive(false);
            PoolParent = gameObject.transform;
        }

        protected override T CreateInstance()
        {
            var instace = Object.Instantiate(_prefab, PoolParent);
            instace.ReturnHandler += ReturnToPool;
            instace.transform.SetParent(null);
            return instace;
        }
        
        private void ReturnToPool(T instance)
        {
            Return(instance);
        }
        
        protected override void OnRent(T instance)
        {
            instance.transform.SetParent(null);
        }
        protected override void OnReturn(T instance)
        {
            instance.transform.SetParent(PoolParent);
        }

        protected override void OnDestroy(T instance)
        {
            if (instance == null) return;
            instance.ReturnHandler -= ReturnToPool;
        }
    }
}