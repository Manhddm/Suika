using System;

namespace Suika.Scripts.Pool
{
    public interface IReturnable
    {
        void ReturnToPool();
    }
    public interface IReturnable<T> : IReturnable where T : IReturnable <T>
    {
        event Action<T> ReturnHandler;
    }
}