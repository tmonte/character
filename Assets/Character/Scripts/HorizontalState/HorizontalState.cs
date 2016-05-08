using UnityEngine;
using System.Collections;

namespace Character
{
	public enum HorizontalStates
	{
		Idle,
		Moving
	}
	
	public interface HorizontalState
	{		
        void Update(Character character);

        void FixedUpdate(Character character);

        void Start(Character character);
	}
}