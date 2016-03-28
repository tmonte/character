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
	    VerticalSM _verticalSM;
	    EquipmentSM _equipmentSM;
	    SpeedSM _speedSM;
		
		// Current character states
	    HorizontalState _horizontalState;
	    VerticalState _verticalState;
	    EquipmentState _equipmentState;
	    SpeedState _speedState;

	    public Character
	    (
		    CharacterHooks hooks,
		    HorizontalStateFactory horizontalStateFactory,
		    VerticalSM verticalSM,
		    EquipmentSM equipmentSM,
		    SpeedSM speedSM
	    )
        {
	        _hooks = hooks;
	        _horizontalStateFactory = horizontalStateFactory;
	        _verticalSM = verticalSM;
	        _equipmentSM = equipmentSM;
	        _speedSM = speedSM;
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
	    	// Set the character states to his
	    	// state machines initial states
	    	
	    	_horizontalState = _horizontalStateFactory.Create();
	    	_verticalState = _verticalSM.State;
	    	_equipmentState = _equipmentSM.State;
	    	_speedState = _speedSM.State;
	    }
    }
}

