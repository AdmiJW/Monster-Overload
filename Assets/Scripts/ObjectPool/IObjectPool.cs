
public interface IObjectPool<T> {
    public void Clear();
    public T Get();
    public void Release(T element);
}
