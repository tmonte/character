using UnityEngine;
using Zenject;
using System;

namespace Character
{
    public class CharacterStateStopped : HorizontalState
	{
		Settings _settings;
		Camera _camera;
        const float MovementThreshold = 0.00001f;
		
		public CharacterStateStopped(
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
            
		}
		
        public override void FixedUpdate()
        {
            if (_character.IsInState(VerticalStates.Jumping)) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            bool hasH = h > MovementThreshold || h < -MovementThreshold;
            bool hasV = v > MovementThreshold || v < -MovementThreshold;

            UpdateAnimator();

            if (hasH || hasV)
                _character.ChangeState(Trigger.MovePress);
        }

        void UpdateAnimator()
        {
            _character.Animator.SetFloat("Forward", 0, 0.1f, Time.deltaTime);
            _character.Animator.SetFloat("Turn", 0, 0.1f, Time.deltaTime);
        }

		[Serializable]
		public class Settings
		{
			// Add settings here
		}
    }
}
