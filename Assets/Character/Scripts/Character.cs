using System;
using UnityEngine;
using Zenject;
using Stateless;

namespace Character
{
	public class Character : ITickable, IInitializable
	{
		// Hooks to get Monobehaviour props
	    CharacterHooks _hooks;
		
		// State machines that handle state changes
		HorizontalStateFactory _horizontalStateFactory;
	    VerticalStateFactory _verticalStateFactory;
	    EquipmentStateFactory _equipmentStateFactory;
	    SpeedStateFactory _speedStateFactory;
		
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
        }
	    
	    public CapsuleCollider CapsuleCollider
	    {
	    	get 
	    	{
	    		return _hooks.GetComponent<CapsuleCollider>();
	    	}
	    }
	    
	    public Animator Animator {
		    get
		    {
		    	return _hooks.GetComponent<Animator>();
		    }
	    }

        public Rigidbody Rigidbody {
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
	    
	    public void Initialize()
	    {
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
    }
}

