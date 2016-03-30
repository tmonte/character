namespace Character
{
    public enum SpeedStates
    {
        Slow,
        Fast
    }

    public abstract class SpeedState
    {
        protected Character _character;

        public SpeedState(Character character)
        {
            _character = character;
        }

        public abstract void Update();

        public virtual void Start()
        { }

        public virtual void Stop()
        { }
    }
}
