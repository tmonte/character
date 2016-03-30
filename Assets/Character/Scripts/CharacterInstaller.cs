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
	    Settings _settings = null;

        public override void InstallBindings()
	    {
	    	Container.Bind<Camera>("Main").ToSingleInstance(_settings.MainCamera);
		    
            // Character State Factories
		    Container.Bind<HorizontalStateFactory>().ToSingle();
            Container.Bind<VerticalStateFactory>().ToSingle();
            Container.Bind<EquipmentStateFactory>().ToSingle();
            Container.Bind<SpeedStateFactory>().ToSingle();

            // Character hooks to access unity props
            Container.Bind<CharacterHooks>()
			    .ToTransientPrefab<CharacterHooks>(_settings.Character.Prefab)
			    .WhenInjectedInto<Character>();
		    
            // Character
		    Container.Bind<Character>().ToSingle();
		    Container.Bind<ITickable>().ToSingle<Character>();
            Container.Bind<IFixedTickable>().ToSingle<Character>();
		    Container.Bind<IInitializable>().ToSingle<Character>();
		    
            // Character States Settings
		    Container.Bind<CharacterStateIdle.Settings>()
			    .ToSingleInstance(_settings.Character.StateIdle);
		    Container.Bind<CharacterStateMoving.Settings>()
			    .ToSingleInstance(_settings.Character.StateMoving);
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
		    public CharacterStateMoving.Settings StateMoving;
		    public CharacterStateIdle.Settings StateIdle;
            public GameObject Prefab;
        }
    }
}