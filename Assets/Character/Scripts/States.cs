using UnityEngine;
using Stateless;
using System.Collections.Generic;

namespace Character 
{
	public enum Trigger
	{
		MovePress,
		MoveRelease,
		SpeedToggle,
		SpeedPress,
		SpeedRelease,
		BlockPress,
		BlockRelease,
		DuckToggle,
		AttackPress,
		AttackRelease,
		JumpPress,
		JumpRelease
	}
	
	public enum HorizontalStates 
	{
		Idle,
		Moving
	}
	
	public enum VerticalStates
	{
		Standing,
		Ducking,
		Jumping
	}
	
	public enum EquipmentStates
	{
		Equipped,
		Blocking,
		Bashing,
		Attacking
	}
	
	public enum SpeedStates
	{
		Slow,
		Fast
	}
}
