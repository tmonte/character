using UnityEngine;

namespace Character
{
    class CharacterStateSlow : SpeedState
    {
        public CharacterStateSlow(
            Character character
        ) : base(character)
        { }

        public override void Update()
        {
            if(Input.GetKey(KeyCode.Tab))
            {
                _character.ChangeState(Trigger.SpeedToggle);
            }
        }
    }
}
