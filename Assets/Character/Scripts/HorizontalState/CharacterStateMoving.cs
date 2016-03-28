using UnityEngine;
using System.Collections;
using System;
using Zenject;

namespace Character
{
    public class CharacterStateMoving : HorizontalState
	{
		Settings _settings;
		Camera _camera;
		
		public CharacterStateMoving(
			Settings settings,
			Character character,
			[Inject("Main")] Camera camera
		) : base(character) 
		{
			_settings = settings;
			_camera = camera;
		}
		
		public override void Update()
		{
            Debug.Log("Character is MOVING");
		}
		
		[Serializable]
		public class Settings
		{
			// Add settings here
		}
    }
}
