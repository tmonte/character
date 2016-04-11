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
        }

        public override void Update()
        {
        }
    }
}
