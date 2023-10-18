using UnityEngine;

//Интерфейс для factory, так как их несколько
public interface IFactory<T>
{
    public T Get(Transform parent);
}
