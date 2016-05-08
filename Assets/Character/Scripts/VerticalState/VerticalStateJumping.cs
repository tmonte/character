using UnityEngine;

namespace Character
{
    public class VerticalStateJumping : VerticalState
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

        public void Start(Character character)
        {
            _originalGroundCheckDistance = _groundCheckDistance;
            character.Rigidbody.velocity = new Vector3(
                character.Rigidbody.velocity.x,
                _jumpPower,
                character.Rigidbody.velocity.z);
            character.Animator.applyRootMotion = false;
            _groundCheckDistance = 0.1f;
            character.Animator.SetBool("OnGround", false);
        }

        public void FixedUpdate(Character character) {}

        public void Update(Character character)
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            Debug.DrawLine(
                character.Transform.position + (Vector3.up * 0.1f),
                character.Transform.position + (Vector3.up * 0.1f) +
                (Vector3.down * GroundCheckDistance));
#endif
            if (Physics.Raycast(
                character.Transform.position +
                (Vector3.up * 0.1f),
                Vector3.down,
                out hitInfo,
                GroundCheckDistance))
            {
                character.Animator.SetBool("OnGround", true);
                character.Animator.applyRootMotion = true;
                character.ChangeState(Trigger.JumpRelease);
            }
            else
            {
                Vector3 extraGravityForce = (Physics.gravity * _gravityMultiplier) - Physics.gravity;
                character.Rigidbody.AddForce(extraGravityForce);
                _groundCheckDistance = character.Rigidbody.velocity.y < 0 ? _originalGroundCheckDistance : 0.01f;
                character.Animator.SetFloat("Jump", character.Rigidbody.velocity.y);
            }
        }
    }
}