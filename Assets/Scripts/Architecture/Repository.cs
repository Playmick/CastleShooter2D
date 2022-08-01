namespace Architecture
{
    public abstract class Repository
    {
        public virtual void OnCreate() { }
        public virtual void OnStart() { }
        public abstract void Initialize();
        public abstract void Save();

    }
}

