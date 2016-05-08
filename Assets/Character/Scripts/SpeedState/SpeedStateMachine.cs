namespace Character
{
    using Stateless;
    using System;
    using Zenject;
    using UnityEngine;

    public class SpeedStateMachine : StateMachine<SpeedStates, Trigger>
	{
        SpeedStateSlow _characterStateSlow;
        SpeedStateFast _characterStateFast;

	    public SpeedStateMachine(
            SpeedStateSlow characterStateSlow,
            SpeedStateFast characterStateFast
        ) : base(SpeedStates.Slow) 
	    {
            _characterStateSlow = characterStateSlow;
            _characterStateFast = characterStateFast;

            Configure(SpeedStates.Slow)
                .OnEntry(() => Debug.Log("SLOW"))
                .Permit(Trigger.SpeedToggle, SpeedStates.Fast);

            Configure(SpeedStates.Fast)
                .OnEntry(() => Debug.Log("FAST"))
                .Permit(Trigger.SpeedToggle, SpeedStates.Slow);
        }

        public SpeedState GetCurrentState(Character character)
        {
            switch (State)
            {
                case SpeedStates.Slow:
                    _characterStateSlow.Start(character);
                    return _characterStateSlow;
                case SpeedStates.Fast:
                    _characterStateFast.Start(character);
                    return _characterStateFast;
                default:
                    return null;
            }
        }
    }
}
