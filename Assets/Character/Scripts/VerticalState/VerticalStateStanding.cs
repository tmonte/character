using UnityEngine;
using Zenject;

namespace Character
{
    public class VerticalStateStanding : VerticalState
    {
        bool _jump = false;

        public void Update(Character character)
        {
            if(Input.GetButtonDown("Jump"))
            {
                _jump = true;
            }
            if (Input.GetKeyUp("c"))
            {
                character.ChangeState(Trigger.DuckToggle);
            }
        }

        public void FixedUpdate(Character character) 
        {
            if (_jump)
                character.ChangeState(Trigger.JumpPress);
            _jump = false;
        }

        public void Start(Character character) {}
    }
}
