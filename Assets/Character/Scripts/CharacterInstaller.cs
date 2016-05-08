namespace Character
{
    using System;
    using UnityEngine;
    using Zenject;

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
            try
            {
                Container.Bind<Camera>("Main").ToSingleInstance(_settings.MainCamera);

                Container.Bind<HorizontalStateStopped>().ToSingle();
                Container.Bind<HorizontalStateMoving>().ToSingle();
                Container.Bind<HorizontalStateMachine>().ToSingle();

                Container.Bind<VerticalStateStanding>().ToSingle();
                Container.Bind<VerticalStateDucking>().ToSingle();
                Container.Bind<VerticalStateJumping>().ToSingle();
                Container.Bind<VerticalStateMachine>().ToSingle();

                //Container.Bind<EquipmentStateFactory>().ToSingle();

                Container.Bind<SpeedStateSlow>().ToSingle();
                Container.Bind<SpeedStateFast>().ToSingle();
                Container.Bind<SpeedStateMachine>().ToSingle();

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
                Container.Bind<HorizontalStateStopped.Settings>()
			    .ToSingleInstance(_settings.Character.StateIdle);
                Container.Bind<HorizontalStateMoving.Settings>()
			    .ToSingleInstance(_settings.Character.StateMoving);
            }
            catch (Exception e)
            {
                throw e;
            }
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
            public HorizontalStateMoving.Settings StateMoving;
            public HorizontalStateStopped.Settings StateIdle;
            public GameObject Prefab;
        }
    }
}