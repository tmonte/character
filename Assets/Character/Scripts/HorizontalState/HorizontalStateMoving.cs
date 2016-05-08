using UnityEngine;
using System;
using Zenject;

namespace Character
{
    public class HorizontalStateMoving : HorizontalState
    {
        [SerializeField]
        float MovingTurnSpeed = 360;
        [SerializeField]
        float StationaryTurnSpeed = 180;
        [Range(1f, 4f)]
        [SerializeField]
        float GravityMultiplier = 2f;
        [SerializeField]
        float MoveSpeedMultiplier = 1f;
        [SerializeField]
        float AnimSpeedMultiplier = 1f;
        [SerializeField]
        float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others

        Settings _settings;
        Camera _camera;
        const float _movementThreshold = 0.00001f;

        float _turnAmount;
        float _forwardAmount;


        public HorizontalStateMoving(Settings settings, [Inject("Main")] Camera camera)
        {
            _settings = settings;
            _camera = camera;
        }

        public void Start(Character character) {}

        private void CheckStoppedMoving(Character character, float h, float v)
        {
            bool hasH = h > _movementThreshold || h < -_movementThreshold;
            bool hasV = v > _movementThreshold || v < -_movementThreshold;
            if (!hasH && !hasV && !character.IsInState(VerticalStates.Jumping))
                character.ChangeState(Trigger.MoveRelease);
        }

        public void Update(Character character)
        {
        }

        public void FixedUpdate(Character character)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            CheckStoppedMoving(character, h, v);
            Vector3 move;
            if (_camera.transform != null)
            {
                var camForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
                move = v * camForward + h * _camera.transform.right;
            }
            else
            {
                move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            if (character.IsInState(SpeedStates.Fast))
                move *= 0.5f;
#endif
            Move(character, move);
        }

        public void Move(Character character, Vector3 move)
        {
            if (move.magnitude > 1f)
                move.Normalize();
            move = character.Transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            _turnAmount = Mathf.Atan2(move.x, move.z);
            _forwardAmount = move.z;
            ApplyExtraTurnRotation(character);
            UpdateAnimator(character, move);
        }

        void UpdateAnimator(Character character, Vector3 move)
        {
            character.Animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
            character.Animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);

            float runCycle =
                Mathf.Repeat(
                    character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
            float jumpLeg = (runCycle < 0.5f ? 1 : -1) * _forwardAmount;
            if(!character.IsInState(VerticalStates.Jumping))
                character.Animator.SetFloat("JumpLeg", jumpLeg);
            if (move.magnitude > 0)
            {
                character.Animator.speed = AnimSpeedMultiplier;
            }
            else
            {
                character.Animator.speed = 1;
            }
        }

        void ApplyExtraTurnRotation(Character character)
        {
            float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, _forwardAmount);
            character.Transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
        }


        public void OnAnimatorMove(Character character)
        {
            if (Time.deltaTime > 0)
            {
                Vector3 v = (character.Animator.deltaPosition * MoveSpeedMultiplier) / Time.deltaTime;
                v.y = character.Rigidbody.velocity.y;
                character.Rigidbody.velocity = v;
            }
        }

        [Serializable]
        public class Settings
        {
            // Add settings here
        }
    }
}