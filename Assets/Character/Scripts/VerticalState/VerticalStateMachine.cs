using System;
using Stateless;
using Zenject;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Character stealth state.
    /// </summary>
    public class VerticalStateMachine : StateMachine<VerticalStates, Trigger>
	{
        VerticalStateStanding _characterStateStanding;
        VerticalStateDucking _characterStateDucking;
        VerticalStateJumping _characterStateJumping;

        public VerticalStateMachine(
            VerticalStateStanding characterStateStanding,
            VerticalStateDucking characterStateDucking,
            VerticalStateJumping characterStateJumping
        ) : base(VerticalStates.Standing)
		{
            _characterStateStanding = characterStateStanding;
            _characterStateDucking = characterStateDucking;
            _characterStateJumping = characterStateJumping;

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

        public VerticalState GetCurrentState(Character character)
        {
            switch (State)
            {
                case VerticalStates.Standing:
                    _characterStateStanding.Start(character);
                    return _characterStateStanding;
                case VerticalStates.Ducking:
                    _characterStateDucking.Start(character);
                    return _characterStateDucking;
                case VerticalStates.Jumping:
                    _characterStateJumping.Start(character);
                    return _characterStateJumping;
                default:
                    return null;
            }
        }
    }
}