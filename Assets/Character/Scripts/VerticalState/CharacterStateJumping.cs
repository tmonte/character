using UnityEngine;

namespace Character
{
    class CharacterStateJumping : VerticalState
    {
        [SerializeField]
        float _jumpPower = 12f;
        [SerializeField]
        float GroundCheckDistance = 0.1f;

        [Range(1f, 4f)]
        [SerializeField]
        float _gravityMultiplier = 2f;

        [SerializeField]
        float _groundCheckDistance = 0.1f;
        float _originalGroundCheckDistance;
        Vector3 _groundNormal;
        bool _isGrounded;

        bool _isJumping;

        public CharacterStateJumping(
            Character character
        ) : base(character)
        {
            _originalGroundCheckDistance = _groundCheckDistance;
            _character.Rigidbody.velocity = new Vector3(
                _character.Rigidbody.velocity.x,
                _jumpPower,
                _character.Rigidbody.velocity.z);
            _character.Animator.applyRootMotion = false;
            _groundCheckDistance = 0.1f;
            _character.Animator.SetBool("OnGround", false);
        }

        public override void Update()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            Debug.DrawLine(
                _character.Transform.position + (Vector3.up * 0.1f),
                _character.Transform.position + (Vector3.up * 0.1f) +
                (Vector3.down * GroundCheckDistance));
#endif
            if (Physics.Raycast(
                _character.Transform.position +
                (Vector3.up * 0.1f),
                Vector3.down,
                out hitInfo,
                GroundCheckDistance))
            {
                _character.Animator.SetBool("OnGround", true);
                _character.Animator.applyRootMotion = true;
                _character.ChangeState(Trigger.JumpRelease);
            }
            else
            {
                Vector3 extraGravityForce = (Physics.gravity * _gravityMultiplier) - Physics.gravity;
                _character.Rigidbody.AddForce(extraGravityForce);
                _groundCheckDistance = _character.Rigidbody.velocity.y < 0 ? _originalGroundCheckDistance : 0.01f;
                _character.Animator.SetFloat("Jump", _character.Rigidbody.velocity.y);
            }
        }
    }
}