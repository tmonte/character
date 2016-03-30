using UnityEngine;

namespace Character
{
    class CharacterStateJumping : VerticalState
    {
        [SerializeField]
        float _jumpPower = 12f;

        [Range(1f, 4f)]
        [SerializeField]
        float _gravityMultiplier = 2f;

        [SerializeField]
        float _groundCheckDistance = 0.1f;
        float _originalGroundCheckDistance;

        bool _isJumping;

        public CharacterStateJumping(
            Character character
        ) : base(character)
        {
            _originalGroundCheckDistance = _groundCheckDistance;
        }

        public override void Update()
        {
            if (!_isJumping)
            {
                Jump();
                UpdateAnimator(true);
            }
            else
            {
                Fall();
                UpdateAnimator(false);
            }
        }

        void Jump()
        {
            _character.Rigidbody.velocity = new Vector3(
                _character.Rigidbody.velocity.x,
                _jumpPower,
                _character.Rigidbody.velocity.z);
            _character.Animator.applyRootMotion = false;
            _isJumping = true;
            _groundCheckDistance = 0.1f;
        }

        void Fall()
        {
            Vector3 extraGravityForce = (Physics.gravity * _gravityMultiplier) - Physics.gravity;
            _character.Rigidbody.AddForce(extraGravityForce);
            _groundCheckDistance = _character.Rigidbody.velocity.y < 0 ? _originalGroundCheckDistance : 0.01f;
            _character.ChangeState(Trigger.JumpRelease);
        }

        void UpdateAnimator(bool jump)
        {
            if (jump)
                _character.Animator.SetFloat("Jump", _character.Rigidbody.velocity.y);
            else
                _character.Animator.SetFloat("Jump", 0);
        }
    }
}
