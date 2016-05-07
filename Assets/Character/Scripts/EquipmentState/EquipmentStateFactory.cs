namespace Character
{
    using Zenject;
    using Stateless;

    /// <summary>
    /// Character equipment state.
    /// </summary>
    public class EquipmentStateFactory : StateMachine<EquipmentStates, Trigger>
	{
        DiContainer _container;

	    public EquipmentStateFactory(DiContainer container) : base(EquipmentStates.Equipped)
		{
            _container = container;

			Configure(EquipmentStates.Equipped)
				.Permit(Trigger.AttackPress, EquipmentStates.Attacking)
				.Permit(Trigger.BlockPress, EquipmentStates.Blocking);
			
			Configure(EquipmentStates.Blocking)
				.SubstateOf(EquipmentStates.Equipped)
				.Permit(Trigger.AttackPress, EquipmentStates.Bashing)
				.Permit(Trigger.BlockRelease, EquipmentStates.Equipped);
			
			Configure(EquipmentStates.Bashing)
				.SubstateOf(EquipmentStates.Blocking)
				.Permit(Trigger.AttackRelease, EquipmentStates.Blocking)
				.Permit(Trigger.BlockRelease, EquipmentStates.Equipped);
			
			Configure(EquipmentStates.Attacking)
				.SubstateOf(EquipmentStates.Equipped)
				.Permit(Trigger.AttackRelease, EquipmentStates.Equipped);        
		}

        public EquipmentState Create(params object[] constructorArgs)
        {
            switch (State)
            {
                case EquipmentStates.Equipped:
                    return _container.Instantiate<CharacterStateEquipped>(constructorArgs);
                case EquipmentStates.Attacking:
                    return _container.Instantiate<CharacterStateAttacking>(constructorArgs);
                case EquipmentStates.Blocking:
                    return _container.Instantiate<CharacterStateBlocking>(constructorArgs);
                case EquipmentStates.Bashing:
                    return _container.Instantiate<CharacterStateBashing>(constructorArgs);
                default:
                    return null;
            }
        }
    }
}