namespace Character
{
	public enum VerticalStates
	{
		Standing,
		Ducking,
		Jumping
	}
	
	public interface VerticalState 
	{
        void Start(Character character);

        void Update(Character character);

        void FixedUpdate(Character character);
    }
}