namespace Character
{
    using System;
    using UnityEngine;
	using Zenject;
	using Stateless;

    public enum Cameras
    {
        Main
    }

	public class CharacterInstaller: MonoInstaller
	
    {
        [SerializeField]
        Settings Settings = null;

        public override void InstallBindings()
	    {
	    	Container.Bind<Camera>("Main").ToSingleInstance(Settings.MainCamera);
		    
		    // Character hooks to access unity specific props
		    Container.Bind<CharacterHooks>()
			    .ToTransientPrefab<CharacterHooks>(Settings.Character.Prefab)
			    .WhenInjectedInto<Character>();
		    
		    // State machines necessary for the character state
		    //Container.Bind<HorizontalSM>().ToSingle();
		    //Container.Bind<VerticalSM>().ToSingle();
		    //Container.Bind<SpeedSM>().ToSingle();
		    //Container.Bind<EquipmentSM>().ToSingle();
		    Container.Bind<CharacterState>().ToSingle();
		    
		    // Character object
		    Container.Bind<Character>().ToSingle();
		    
		    // Character behaviors
		    //Container.Bind<HorizontalBehavior>().ToSingle();		    
		    //Container.Bind<IFixedTickable>().ToSingle<HorizontalBehavior>();
		    
		    Container.Bind<InputHandler>().ToSingle();
		    Container.Bind<ITickable>().ToSingle<InputHandler>();
        }
    }

    [Serializable]
    public class Settings
    {
        public Camera MainCamera;
        public CharacterSettings Character;

        [Serializable]
        public class CharacterSettings
        {
            public GameObject Prefab;
        }
    }
}