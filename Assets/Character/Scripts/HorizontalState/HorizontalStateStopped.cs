using UnityEngine;
using Zenject;
using System;

namespace Character
{
    public class HorizontalStateStopped : HorizontalState
	{
		Settings _settings;
		Camera _camera;
        const float MovementThreshold = 0.00001f;
		
		public HorizontalStateStopped(Settings settings, [Inject("Main")] Camera camera)
		{
			_settings = settings;
			_camera = camera;
		}
		
        public void Start(Character character) {}

        public void Update(Character character)
		{
            
		}
		
        public void FixedUpdate(Character character)
        {
            if (character.IsInState(VerticalStates.Jumping)) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            bool hasH = h > MovementThreshold || h < -MovementThreshold;
            bool hasV = v > MovementThreshold || v < -MovementThreshold;

            UpdateAnimator(character);

            if (hasH || hasV)
                character.ChangeState(Trigger.MovePress);
        }

        void UpdateAnimator(Character character)
        {
            character.Animator.SetFloat("Forward", 0, 0.1f, Time.deltaTime);
            character.Animator.SetFloat("Turn", 0, 0.1f, Time.deltaTime);
        }

		[Serializable]
		public class Settings
		{
			// Add settings here
		}
    }
}
