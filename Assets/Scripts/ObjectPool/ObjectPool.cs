using System;
using System.Collections.Generic;

public class ObjectPool<T> : IObjectPool<T> {

    private Stack<T> pool;
    private HashSet<T> active;
    private bool expandOnDemand;

    private Func<T> createFunc;
    private Action<T> getFunc;
    private Action<T> releaseFunc;
    private Action<T> destroyFunc;


    //===================================
    // Constructor and Builder functions
    //===================================
    public ObjectPool(Func<T> createFunc, bool expandOnDemand = true, int initialSize = 10) {
        this.pool = new Stack<T>(initialSize);
        this.active = new HashSet<T>();
        this.createFunc = createFunc;
        this.expandOnDemand = expandOnDemand;
        
        for (int i = 0; i < initialSize; i++) pool.Push(createFunc());
    }

    public ObjectPool<T> SetGetFunc(Action<T> getFunc) {
        this.getFunc = getFunc;
        return this;
    }

    public ObjectPool<T> SetReleaseFunc(Action<T> releaseFunc) {
        this.releaseFunc = releaseFunc;
        return this;
    }

    public ObjectPool<T> SetDestroyFunc(Action<T> destroyFunc) {
        this.destroyFunc = destroyFunc;
        return this;
    }



    //===================================
    // Public
    //===================================
    public void Clear() {
        if (destroyFunc != null) {
            foreach (T element in pool) destroyFunc(element);
            foreach (T element in active) destroyFunc(element);
        }
        pool.Clear();
        active.Clear();
    }

    public T Get() {
        if (pool.Count == 0) {
            if (expandOnDemand) pool.Push(createFunc());
            else throw new Exception("Pool is empty");
        }

        if (getFunc != null) getFunc(pool.Peek());
        active.Add(pool.Peek());
        return pool.Pop();
    }

    public void Release(T element) {
        if (!active.Contains(element)) throw new Exception("Element is not active/does not belong in the pool");

        if (releaseFunc != null) releaseFunc(element);
        active.Remove(element);
        pool.Push(element);
    }


    public void ReleaseAll() {
        foreach (T element in active) {
            pool.Push(element);
            if (releaseFunc != null) releaseFunc(element);
        }
        active.Clear();
    }
}
