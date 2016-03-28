namespace Character
{
    using System;
    using Stateless;

    /// <summary>
    /// Character equipment state.
    /// </summary>
	public class EquipmentSM : StateMachine<EquipmentStates, Trigger>
	{
	    public EquipmentSM() : base(EquipmentStates.Equipped)
		{
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
    }
}