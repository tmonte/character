namespace Character
{
    public enum SpeedStates
    {
        Slow,
        Fast
    }

    public interface SpeedState
    {
        void Start(Character character);

        void Update(Character character);

        void FixedUpdate(Character character);
    }
}
