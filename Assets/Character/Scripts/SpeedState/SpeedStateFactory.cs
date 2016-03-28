namespace Character
{
    using Stateless;
    using System;
    using Zenject;

    /// <summary>
    /// Character speed state
    /// </summary>
    public class SpeedStateFactory : StateMachine<SpeedStates, Trigger>
	{
        DiContainer _container;

	    public SpeedStateFactory(DiContainer container) : base(SpeedStates.Slow) 
	    {
            _container = container;

            Configure(SpeedStates.Slow)
                .Permit(Trigger.SpeedToggle, SpeedStates.Fast)
                .Permit(Trigger.SpeedPress, SpeedStates.Fast);

            Configure(SpeedStates.Fast)
                .Permit(Trigger.SpeedToggle, SpeedStates.Slow)
                .Permit(Trigger.SpeedRelease, SpeedStates.Slow);
        }

        public SpeedState Create(params object[] constructorArgs)
        {
            switch (State)
            {
                case SpeedStates.Slow:
                    return _container.Instantiate<CharacterStateSlow>(constructorArgs);
                case SpeedStates.Fast:
                    return _container.Instantiate<CharacterStateFast>(constructorArgs);
                default:
                    return null;
            }
        }
    }
}
