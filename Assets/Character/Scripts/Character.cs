using UnityEngine;
using Zenject;

namespace Character
{
    public class Character : ITickable, IFixedTickable, IInitializable
    {
        // Hooks to get Monobehaviour props
        CharacterHooks _hooks;
		
        // State machines that handle state changes
        public HorizontalStateFactory _horizontalStateFactory;
        public VerticalStateFactory _verticalStateFactory;
        public EquipmentStateFactory _equipmentStateFactory;
        public SpeedStateFactory _speedStateFactory;
		
        // Current character states
        HorizontalState _horizontalState;
        VerticalState _verticalState;
        EquipmentState _equipmentState;
        SpeedState _speedState;

        public Character
	    (
            CharacterHooks hooks,
            HorizontalStateFactory horizontalStateFactory,
            VerticalStateFactory verticalStateFactory,
            EquipmentStateFactory equipmentStateFactory,
            SpeedStateFactory speedStateFactory
        )
        {
            _hooks = hooks;
            _horizontalStateFactory = horizontalStateFactory;
            _verticalStateFactory = verticalStateFactory;
            _equipmentStateFactory = equipmentStateFactory;
            _speedStateFactory = speedStateFactory;
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
            // Delegates the update to each state
	    	
            _horizontalState.Update();
            _verticalState.Update();
            _equipmentState.Update();
            _speedState.Update();
        }

        public void FixedTick()
        {
            // Delegates the fixed update to each state

            _horizontalState.FixedUpdate();
        }

        public void Initialize()
        {
            // Initialize all state machines

            UpdateState();
        }

        /// <summary>
        /// Sets the character states to his state machines
        /// current state
        /// </summary>
        public void UpdateState()
        {
            _horizontalState = _horizontalStateFactory.Create(this);
            _verticalState = _verticalStateFactory.Create(this);
            _equipmentState = _equipmentStateFactory.Create(this);
            _speedState = _speedStateFactory.Create(this);
        }

        /// <summary>
        /// Changes the the character based on a trigger
        /// </summary>
        /// <param name="trigger">A trigger, or input command</param>
        public void ChangeState(Trigger trigger)
        {
            if (_horizontalStateFactory.CanFire(trigger))
                _horizontalStateFactory.Fire(trigger);
            if (_verticalStateFactory.CanFire(trigger))
                _verticalStateFactory.Fire(trigger);
            if (_equipmentStateFactory.CanFire(trigger))
                _equipmentStateFactory.Fire(trigger);
            if (_speedStateFactory.CanFire(trigger))
                _speedStateFactory.Fire(trigger);

            UpdateState();
        }

        public bool IsInState(SpeedStates state)
        {
            return _speedStateFactory.IsInState(state);
        }

        public bool IsInState(VerticalStates state)
        {
            return _verticalStateFactory.IsInState(state);
        }
    }
}

