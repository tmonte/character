using UnityEngine;
using Zenject;

namespace Character
{
    public class Character : ITickable, IFixedTickable, IInitializable
    {
        CharacterHooks _hooks;

        public HorizontalStateMachine _horizontalStateMachine;
        public VerticalStateMachine _verticalStateMachine;
        //public EquipmentStateFactory _equipmentStateFactory;
        public SpeedStateMachine _speedStateMachine;

        HorizontalState _horizontalState;
        VerticalState _verticalState;
        //EquipmentState _equipmentState;
        SpeedState _speedState;

        public Character
        (
            CharacterHooks hooks,
            HorizontalStateMachine horizontalStateMachine,
            VerticalStateMachine verticalStateFactory,
            //EquipmentStateFactory equipmentStateFactory,
            SpeedStateMachine speedStateFactory
        )
        {
            _hooks = hooks;
            _horizontalStateMachine = horizontalStateMachine;
            _verticalStateMachine = verticalStateFactory;
            //_equipmentStateFactory = equipmentStateFactory;
            _speedStateMachine = speedStateFactory;
            Rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
        }

        public CapsuleCollider CapsuleCollider
        {
            get
            {
                return _hooks.GetComponent<CapsuleCollider>();
            }
        }

        public Animator Animator
        {
            get
            {
                return _hooks.GetComponent<Animator>();
            }
        }

        public Rigidbody Rigidbody
        {
            get
            {
                return _hooks.GetComponent<Rigidbody>();
            }
        }

        public Vector3 Position
        {
            get
            {
                return _hooks.transform.position;
            }
            set
            {
                _hooks.transform.position = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return _hooks.transform.rotation;
            }
            set
            {
                _hooks.transform.rotation = value;
            }
        }

        public Transform Transform
        {
            get
            {
                return _hooks.transform;
            }
        }

        public void Tick()
        {
            _horizontalState.Update(this);
            _verticalState.Update(this);
            _speedState.Update(this);
        }

        public void FixedTick()
        {
            _horizontalState.FixedUpdate(this);
            _verticalState.FixedUpdate(this);
            _speedState.FixedUpdate(this);
        }
            
        public void Initialize()
        {
            UpdateState();
        }

        public void UpdateState()
        {
            _horizontalState = _horizontalStateMachine.GetCurrentState(this);
            _verticalState = _verticalStateMachine.GetCurrentState(this);
            _speedState = _speedStateMachine.GetCurrentState(this);
        }

        public void ChangeState(Trigger trigger)
        {
            if (_horizontalStateMachine.CanFire(trigger))
                _horizontalStateMachine.Fire(trigger);
            if (_verticalStateMachine.CanFire(trigger))
                _verticalStateMachine.Fire(trigger);
//            if (_equipmentStateFactory.CanFire(trigger))
//                _equipmentStateFactory.Fire(trigger);
            if (_speedStateMachine.CanFire(trigger))
                _speedStateMachine.Fire(trigger);

            UpdateState();
        }

        public bool IsInState(SpeedStates state)
        {
            return _speedStateMachine.IsInState(state);
        }

        public bool IsInState(VerticalStates state)
        {
            return _verticalStateMachine.IsInState(state);
        }
    }
}

