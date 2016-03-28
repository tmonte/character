namespace Character
{
	public enum EquipmentStates
	{
		Equipped,
		Blocking,
		Bashing,
		Attacking
	}
	
	public abstract class EquipmentState 
	{
        Character _character;

        public EquipmentState(Character character)
        {
            _character = character;
        }

        public abstract void Update();

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }
	}
}