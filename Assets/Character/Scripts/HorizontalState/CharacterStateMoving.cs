using UnityEngine;
using System;
using Zenject;

namespace Character
{
    public class CharacterStateMoving : HorizontalState
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

        Settings _settings;
        Camera _camera;
        const float _movementThreshold = 0.00001f;

        float _turnAmount;
        float _forwardAmount;


        public CharacterStateMoving(
            Settings settings,
            Character character,
            [Inject("Main")] Camera camera) : base(character)
        {
            _settings = settings;
            _camera = camera;
        }

        private void CheckStoppedMoving(float h, float v)
        {
            bool hasH = h > _movementThreshold || h < -_movementThreshold;
            bool hasV = v > _movementThreshold || v < -_movementThreshold;
            if (!hasH && !hasV && !_character.IsInState(VerticalStates.Jumping))
                _character.ChangeState(Trigger.MoveRelease);
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            CheckStoppedMoving(h, v);
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
            if (_character.IsInState(SpeedStates.Fast))
                move *= 0.5f;
#endif
            Move(move);
        }

        public void Move(Vector3 move)
        {
            if (move.magnitude > 1f)
                move.Normalize();
            move = _character.Transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            _turnAmount = Mathf.Atan2(move.x, move.z);
            _forwardAmount = move.z;
            ApplyExtraTurnRotation();
            UpdateAnimator(move);
        }

        void UpdateAnimator(Vector3 move)
        {
            _character.Animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
            _character.Animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
            if (move.magnitude > 0)
            {
                _character.Animator.speed = AnimSpeedMultiplier;
            }
            else
            {
                _character.Animator.speed = 1;
            }
        }

        void ApplyExtraTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, _forwardAmount);
            _character.Transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
        }


        public void OnAnimatorMove()
        {
            if (Time.deltaTime > 0)
            {
                Vector3 v = (_character.Animator.deltaPosition * MoveSpeedMultiplier) / Time.deltaTime;
                v.y = _character.Rigidbody.velocity.y;
                _character.Rigidbody.velocity = v;
            }
        }

        [Serializable]
        public class Settings
        {
            // Add settings here
        }
    }
}