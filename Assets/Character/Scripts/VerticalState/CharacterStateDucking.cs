using UnityEngine;

namespace Character
{
    class CharacterStateDucking : VerticalState
    {
        float Height;
        Vector3 Center;

        public CharacterStateDucking(
            Character character
        ) : base(character)
        {
            _character.Animator.SetBool("Crouch", true);
            Height = _character.CapsuleCollider.height;
            Center = _character.CapsuleCollider.center;
            ScaleCapsuleForCrouching(true);
        }

        public override void Update()
        {
            if (Input.GetKey("c"))
            {
                _character.Animator.SetBool("Crouch", false);
                ScaleCapsuleForCrouching(false);
                _character.ChangeState(Trigger.DuckToggle);
            }
        }

        public void UpdateAnimator()
        {

        }

        void ScaleCapsuleForCrouching(bool crouch)
        {
            if (!_character.IsInState(VerticalStates.Jumping) && crouch)
            {
                _character.CapsuleCollider.height = _character.CapsuleCollider.height / 2f;
                _character.CapsuleCollider.center = _character.CapsuleCollider.center / 2f;
            }
            else
            {
                _character.CapsuleCollider.height = Height;
                _character.CapsuleCollider.center = Center;
            }
        }
    }
}
