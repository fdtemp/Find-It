

abstract public class ObjectPool<T> where T : class {
    private T[] d;
    private int Size;
    public int Count { get; private set; }

    public ObjectPool(int Size) {
        Count = 0;
        d = new T[Size];
        this.Size = Size;
    }
    public void Put(T Object) {
        if (Object == null) return;
        if (Count == Size) {
            _Destroy(Object);
            return;
        }
        if (!_Collect(Object))
            throw new System.Exception("ObjectPool : " + Object.GetType() + " can't be destruct by Destructor.");
        d[Count++] = (Object);
    }
    public T Get() {
        if (Count == 0) {
            return _Create();
        }
        if (!_Reset(d[--Count]))
            throw new System.Exception("ObjectCollector : " + d[Count].GetType() + " can't be instruct by Intructor.");
        return d[Count];
    }
    abstract public T _Create();
    abstract public bool _Collect(T Object);
    abstract public bool _Reset(T Object);
    abstract public bool _Destroy(T Object);
}
abstract public class ObjectPool<obj, data> where obj : class {
    private obj[] d;
    private int Size;
    public int Count { get; private set; }

    public ObjectPool(int Size) {
        Count = 0;
        d = new obj[Size];
        this.Size = Size;
    }
    public void Put(obj Object) {
        if (Object == null) return;
        if (Count == Size) {
            _Destroy(Object);
            return;
        }
        if (!_Collect(Object))
            throw new System.Exception("ObjectPool : " + Object.GetType() + " can't be destruct by Destructor.");
        d[Count++] = (Object);
    }
    public obj Get(ref data data) {
        if (Count == 0) {
            return _Create(ref data);
        }
        if (!_Reset(d[--Count], ref data))
            throw new System.Exception("ObjectCollector : " + d[Count].GetType() + " can't be instruct by Intructor.");
        return d[Count];
    }
    abstract public obj _Create(ref data Data);
    abstract public bool _Collect(obj Object);
    abstract public bool _Reset(obj Object, ref data Data);
    abstract public bool _Destroy(obj Object);
}