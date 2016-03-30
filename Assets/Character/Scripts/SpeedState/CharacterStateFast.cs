using System;
using UnityEngine;


namespace Character
{
    class CharacterStateFast : SpeedState
    {
        public CharacterStateFast(
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
