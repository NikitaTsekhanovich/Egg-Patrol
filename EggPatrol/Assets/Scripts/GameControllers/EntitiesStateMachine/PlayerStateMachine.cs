using System;
using System.Collections.Generic;
using GameControllers.Controllers;
using GameControllers.StateMachineBasic;
using GameControllers.EntitiesStateMachine.States;
using UnityEngine;

namespace GameControllers.EntitiesStateMachine
{
    public class PlayerStateMachine : StateMachine, IDisposable
    {
        private readonly List<IDisposable> _disposables = new ();

        public const float RotateTime = 0.8f;
        
        public PlayerStateMachine(
            PlayerAnimatorController playerAnimatorController,
            Rigidbody rigidbody, 
            float speedMove,
            float jumpForce,
            float slipTime,
            float linearDamping,
            float slipForce,
            float maxVelocity,
            GroundChecker groundChecker,
            Transform transform,
            Action<bool> blockInput,
            AudioSource jumpSound,
            AudioSource slipSound,
            AudioSource runSound)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(IdleState)] = new IdleState(
                    transform, rigidbody),
                [typeof(LeftMovementState)] = new LeftMovementState(
                    rigidbody, speedMove, maxVelocity, playerAnimatorController, transform, groundChecker, runSound),
                [typeof(RightMovementState)] = new RightMovementState(
                    rigidbody, speedMove, maxVelocity, playerAnimatorController, transform, groundChecker, runSound),
                [typeof(FlightState)] = new FlightState(
                    groundChecker, jumpForce, linearDamping, rigidbody, playerAnimatorController, this, jumpSound),
                [typeof(SlipState)] = new SlipState(
                    this, slipTime, slipForce, playerAnimatorController, blockInput, rigidbody, slipSound),
                [typeof(StunState)] = new StunState(
                    this, playerAnimatorController, blockInput),
                [typeof(DeadState)] = new DeadState(
                    playerAnimatorController)
            };

            _disposables.Add((IDisposable)States[typeof(FlightState)]);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
