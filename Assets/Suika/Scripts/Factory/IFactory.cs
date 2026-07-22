namespace Suika.Scripts.Factory
{
    public interface IFactory<out T>
    {
        T Create();
    }
    public interface IFactory<TParam, out T>
    {
        T Create(TParam param);
    }
    public interface IFactory<TParam1, TParam2, out T> 
    {
        T Create(TParam1 param1, TParam2 param2);
    }
}