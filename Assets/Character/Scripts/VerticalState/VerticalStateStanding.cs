using UnityEngine;
using Zenject;

namespace Character
{
    public class VerticalStateStanding : VerticalState
    {
        public void Update(Character character)
        {
            if(Input.GetButtonDown("Jump"))
            {
                character.ChangeState(Trigger.JumpPress);
            }
            if (Input.GetKey("c"))
            {
                character.ChangeState(Trigger.DuckToggle);
            }
        }

        public void FixedUpdate(Character character) {}

        public void Start(Character character) {}
    }
}
