using System;
using System.Collections.Generic;
using GameControllers.Bootstrap;
using GameControllers.Models.DataContainers;
using GameControllers.StateMachineBasic;
using GlobalSystems;
using MusicSystem;
using TutorialControllers.States;
using Zenject;

namespace TutorialControllers
{
    public class TutorialStateMachine : StateMachine
    {
        public TutorialStateMachine(
            Tutorial tutorial,
            DiContainer container,
            LoadGameData loadGameData,
            UITutorialController uiTutorialController,
            TutorialData tutorialData,
            SceneDataLoader sceneDataLoader,
            MusicSwitcher musicSwitcher)
        {
            var gameSystemsHandler = new GameSystemsHandler();
            
            States = new Dictionary<Type, IState>
            {
                [typeof(LoadTutorialState)] = new LoadTutorialState(
                    this, container, loadGameData, gameSystemsHandler, musicSwitcher, tutorialData),
                [typeof(GreetingState)] = new GreetingState(
                    this, tutorial, uiTutorialController),
                [typeof(MainRulesState)] = new MainRulesState(
                    this, tutorial, uiTutorialController),
                [typeof(PlayerControlState)] = new PlayerControlState(
                    this, tutorial, uiTutorialController, tutorialData, gameSystemsHandler),
                [typeof(EggSpawnState)] = new EggSpawnState(
                    this, uiTutorialController, tutorial, gameSystemsHandler, loadGameData, container, tutorialData),
                [typeof(SpawnEntitiesState)] = new SpawnEntitiesState(
                    this, uiTutorialController, tutorial, gameSystemsHandler, loadGameData, tutorialData),
                [typeof(EndTutorialState)] = new EndTutorialState(
                    uiTutorialController, tutorial, gameSystemsHandler, sceneDataLoader)
            };
            
            EnterIn<LoadTutorialState>();
        }
    }
}
