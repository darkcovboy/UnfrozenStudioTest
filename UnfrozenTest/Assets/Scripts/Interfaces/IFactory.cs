using UnityEngine;

//Интерфейс для factory, так как их несколько
public interface IFactory<T, U> where U : IData
{
    public T Get(U data, Transform parent);
}
