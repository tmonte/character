using UnityEngine;
using Zenject;


namespace Character
{
    class InputHandler : ITickable
    {
        Character _character;

        public InputHandler(Character character)
        {
            _character = character;
        }

        public void Tick()
        {
            if(Input.GetButtonDown("Jump"))
            {
                _character.ChangeState(Trigger.MovePress);
            }
        }
    }
}
