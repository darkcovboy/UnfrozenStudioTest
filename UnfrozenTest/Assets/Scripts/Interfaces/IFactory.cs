using UnityEngine;

//��������� ��� factory, ��� ��� �� ���������
public interface IFactory<T, U> where U : IData
{
    public T Get(U data, Transform parent);
}
