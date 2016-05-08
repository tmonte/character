using UnityEngine;
using Zenject;
using Stateless;

namespace Character
{
    public class HorizontalStateMachine : StateMachine<HorizontalStates, Trigger>
    {
        HorizontalStateMoving _characterStateMoving;
        HorizontalStateStopped _characterStateStopped;

        public HorizontalStateMachine(
            HorizontalStateStopped characterStateStopped,
            HorizontalStateMoving characterStateMoving
        )
            : base(HorizontalStates.Idle)
        {
            _characterStateStopped = characterStateStopped;
            _characterStateMoving = characterStateMoving;

            Configure(HorizontalStates.Idle)
				.OnEntry(() => Debug.Log("IDLE"))
				.Permit(Trigger.MovePress, HorizontalStates.Moving);
			
            Configure(HorizontalStates.Moving)
				.OnEntry(() => Debug.Log("MOVING"))
				.Permit(Trigger.MoveRelease, HorizontalStates.Idle);     
        }

        public HorizontalState GetCurrentState(Character character)
        {
            switch (State)
            {
                case HorizontalStates.Idle:
                    _characterStateStopped.Start(character);
                    return _characterStateStopped;
                case  HorizontalStates.Moving:
                    _characterStateMoving.Start(character);
                    return _characterStateMoving;
                default:
                    return null;
            }
        }
    }
}
