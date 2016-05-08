using System;
using UnityEngine;

namespace Character
{
    public class SpeedStateFast : SpeedState
    {
        public void Start(Character character) {}

        public void FixedUpdate(Character character) 
        {
            if(Input.GetKey(KeyCode.Tab))
            {
                character.ChangeState(Trigger.SpeedToggle);
            }
        }

        public void Update(Character character) {}
    }
}
