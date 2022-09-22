using System.Collections;
using System.Collections.Generic;


public interface IObjectPool<T> {
    public void Clear();
    public T Get();
    public void Release(T element);
    public void ReleaseAll();
}
