using UnityEngine;

namespace Character
{
    class CharacterStateJumping : VerticalState
    {
        public CharacterStateJumping(
            Character character
        ) : base(character)
        { }

        public override void Update()
        {
            Debug.Log("Character is JUMPING");
        }
    }
}
