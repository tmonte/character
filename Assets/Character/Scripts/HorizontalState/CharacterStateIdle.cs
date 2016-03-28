using UnityEngine;
using System.Collections;
using Zenject;
using System;

namespace Character
{
    public class CharacterStateIdle : HorizontalState
	{
		Settings _settings;
		Camera _camera;
		
		public CharacterStateIdle(
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
            Debug.Log("Character is IDLE");
		}
		
		[Serializable]
		public class Settings
		{
			// Add settings here
		}
    }
}
