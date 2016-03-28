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
        }
    }
}
