using System;
using Stateless;
using Zenject;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Character stealth state.
    /// </summary>
    public class VerticalStateFactory : StateMachine<VerticalStates, Trigger>
	{
        DiContainer _container;

        public VerticalStateFactory(DiContainer container) : base(VerticalStates.Standing)
		{
            _container = container;

            Configure(VerticalStates.Standing)
				.OnEntry(() => Debug.Log("STANDING"))
                .Permit(Trigger.DuckToggle, VerticalStates.Ducking)
                .Permit(Trigger.JumpPress, VerticalStates.Jumping);

            Configure(VerticalStates.Jumping)
				.OnEntry(() => Debug.Log("JUMPING"))
                .SubstateOf(VerticalStates.Standing)
                .Permit(Trigger.JumpRelease, VerticalStates.Standing);

            Configure(VerticalStates.Ducking)
				.OnEntry(() => Debug.Log("DUCKING"))
                .Permit(Trigger.DuckToggle, VerticalStates.Standing);
        }

        public VerticalState Create(params object[] constructorArgs)
        {
            switch (State)
            {
                case VerticalStates.Standing:
                    return _container.Instantiate<CharacterStateStanding>(constructorArgs);
                case VerticalStates.Ducking:
                    return _container.Instantiate<CharacterStateDucking>(constructorArgs);
                case VerticalStates.Jumping:
                    return _container.Instantiate<CharacterStateJumping>(constructorArgs);
                default:
                    return null;
            }
        }
    }
}