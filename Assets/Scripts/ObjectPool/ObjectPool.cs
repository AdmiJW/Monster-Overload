using System;
using System.Collections.Generic;

public class ObjectPool<T> : IObjectPool<T> {

    private Stack<T> pool;
    private Func<T> createFunc;
    private bool expandOnDemand;


    public ObjectPool(Func<T> createFunc, bool expandOnDemand = true, int initialSize = 10) {
        this.pool = new Stack<T>(initialSize);
        this.createFunc = createFunc;
        this.expandOnDemand = expandOnDemand;

        for (int i = 0; i < initialSize; i++) pool.Push(createFunc());
    }


    public void Clear() {
        pool.Clear();
    }

    public T Get() {
        if (pool.Count == 0) {
            if (expandOnDemand) pool.Push(createFunc());
            else throw new Exception("Pool is empty");
        }

        return pool.Pop();
    }

    public void Release(T element) {
        pool.Push(element);
    }
}
