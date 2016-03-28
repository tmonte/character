using UnityEngine;
using Zenject;
using Stateless;
	
namespace Character
{
	public class HorizontalStateFactory : StateMachine<HorizontalStates, Trigger>
	{
		DiContainer _container;
		
		public HorizontalStateFactory(DiContainer container) : base(HorizontalStates.Idle) 
		{
			_container = container;
			
			Configure(HorizontalStates.Idle)
				.OnEntry(() => Debug.Log("IDLE"))
				.Permit(Trigger.MovePress, HorizontalStates.Moving);
			
			Configure(HorizontalStates.Moving)
				.OnEntry(() => Debug.Log("MOVING"))
				.Permit(Trigger.MoveRelease, HorizontalStates.Idle);     
		}
		
		public HorizontalState Create(params object[] constructorArgs)
		{
			switch(State)
			{
				case HorizontalStates.Idle:
					return _container.Instantiate<CharacterIdleState>(constructorArgs);
				case  HorizontalStates.Moving:
					return _container.Instantiate<CharacterMovingState>(constructorArgs);
			}
		}
	}
}
