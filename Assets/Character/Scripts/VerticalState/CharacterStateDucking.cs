using UnityEngine;

namespace Character
{
    class CharacterStateDucking : VerticalState
    {
        public CharacterStateDucking(
            Character character
        ) : base(character)
        { }

        public override void Update()
        {
            Debug.Log("Character is Ducking");
            if (Input.GetKey("c"))
            {
                _character.ChangeState(Trigger.DuckToggle);
            }
        }
    }
}
