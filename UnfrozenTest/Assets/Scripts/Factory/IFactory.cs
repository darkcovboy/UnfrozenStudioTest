using UnityEngine;

//��������� ��� factory, ��� ��� �� ���������
public interface IFactory<T>
{
    public T Get(Transform parent);
}
