using UnityEngine;

namespace Character
{
    public class VerticalStateDucking : VerticalState
    {
        float Height;
        Vector3 Center;

        public void Start(Character character)
        {
            Height = character.CapsuleCollider.height;
            Center = character.CapsuleCollider.center;
            ScaleCapsuleForCrouching(character, true);
            character.Animator.SetBool("Crouch", true);
        }

        public void Update(Character character)
        {
            if (Input.GetKeyUp("c"))
            {
                character.Animator.SetBool("Crouch", false);
                ScaleCapsuleForCrouching(character, false);
                character.ChangeState(Trigger.DuckToggle);
            }
        }

        public void FixedUpdate(Character character) {}

        public void UpdateAnimator()
        {

        }

        void ScaleCapsuleForCrouching(Character character, bool crouch)
        {
            if (!character.IsInState(VerticalStates.Jumping) && crouch)
            {
                character.CapsuleCollider.height = character.CapsuleCollider.height / 2f;
                character.CapsuleCollider.center = character.CapsuleCollider.center / 2f;
            }
            else
            {
                character.CapsuleCollider.height = Height;
                character.CapsuleCollider.center = Center;
            }
        }
    }
}
