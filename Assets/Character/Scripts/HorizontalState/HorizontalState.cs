using UnityEngine;
using System.Collections;

namespace Character
{
	public enum HorizontalStates 
	{
		Idle,
		Moving
	}
	
	public abstract class HorizontalState 
	{
		protected Character _character;
		
		public HorizontalState(Character character)
		{
			_character = character;
		}
		
		public abstract void Update();

        public abstract void FixedUpdate();
		
		public virtual void Start()
		{
		}
		
		public virtual void Stop()
		{
		}		
	}
	
}