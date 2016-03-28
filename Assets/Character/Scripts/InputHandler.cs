using UnityEngine;
using System.Collections;
using Zenject;
namespace Character
{
	public class InputHandler: ITickable
	{
		private Character _character;
		
		public InputHandler(Character character)
		{
			_character = character;
		}
		
		public void Tick()
		{
			if(Input.GetKey("up")) {
				//_character.Fire(Trigger.MovePress);
			}
		}
	}
}
