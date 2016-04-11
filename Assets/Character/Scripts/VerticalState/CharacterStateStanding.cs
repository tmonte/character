using UnityEngine;
using Zenject;

namespace Character
{
    class CharacterStateStanding : VerticalState
    {
        Camera _camera;

        public CharacterStateStanding(
            Character character,
            [Inject("Main")] Camera camera
        ) : base(character)
        {
            _camera = camera;
        }

        public override void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                _character.ChangeState(Trigger.JumpPress);
            }
            if (Input.GetKey("c"))
            {
                _character.ChangeState(Trigger.DuckToggle);
            }
        }
    }
}
