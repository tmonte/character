namespace Character
{
    using Stateless;
    using System;

    /// <summary>
    /// Character speed state
    /// </summary>
	public class SpeedSM : StateMachine<SpeedStates, Trigger>
	{
		
	    public SpeedSM() : base(SpeedStates.Slow) 
	    {
		    
            Configure(SpeedStates.Slow)
                .Permit(Trigger.SpeedToggle, SpeedStates.Fast)
                .Permit(Trigger.SpeedPress, SpeedStates.Fast);

            Configure(SpeedStates.Fast)
                .Permit(Trigger.SpeedToggle, SpeedStates.Slow)
                .Permit(Trigger.SpeedRelease, SpeedStates.Slow);
        }
    }
}
