﻿using UnityEngine;

namespace Character
{
    public class SpeedStateSlow : SpeedState
    {
        public void Start(Character character) {}

        public void FixedUpdate(Character character) {}

        public void Update(Character character) 
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                character.ChangeState(Trigger.SpeedToggle);
            }
        }
    }
}
