using UnityEngine;
using System.Collections;

namespace Character
{
	public enum VerticalStates
	{
		Standing,
		Ducking,
		Jumping
	}
	
	public abstract class VerticalState 
	{
        protected Character _character;

        public VerticalState(Character character)
        {
            _character = character;
        }

        public abstract void Update();

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }
    }
}