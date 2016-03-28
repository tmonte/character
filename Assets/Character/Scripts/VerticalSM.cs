namespace Character
{
    using System;
    using Stateless;

    /// <summary>
    /// Character stealth state.
    /// </summary>
	public class VerticalSM : StateMachine<VerticalStates, Trigger>
	{
		
	    public VerticalSM() : base(VerticalStates.Standing)
		{
            Configure(VerticalStates.Standing)
                .Permit(Trigger.DuckToggle, VerticalStates.Ducking)
                .Permit(Trigger.JumpPress, VerticalStates.Jumping);

            Configure(VerticalStates.Jumping)
                .SubstateOf(VerticalStates.Standing)
                .Permit(Trigger.JumpRelease, VerticalStates.Standing);

            Configure(VerticalStates.Ducking)
                .Permit(Trigger.DuckToggle, VerticalStates.Standing);
        }
    }
}