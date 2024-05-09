using PoliceAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PoliceShootState : PoliceState
    {
        public PoliceShootState(PoliceStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            int hash = stateMachine.PoliceAnimationHashes.GetHash(PoliceStates.Shooting);
            stateMachine.Animator.CrossFadeInFixedTime(hash, 0.1f);

            stateMachine.PoliceCollision.onPlayerExitTrigger += SwitchToPursue;
        }

        public override void Exit()
        {
            stateMachine.PoliceCollision.onPlayerExitTrigger -= SwitchToPursue;
        }

        public override void Update(float deltaTime)
        {
            FacePlayer(deltaTime);        
        }

        void SwitchToPursue()
        {
            stateMachine.SetCurrentState(new PolicePursueState(stateMachine));
            int hash = stateMachine.PoliceAnimationHashes.GetHash(PoliceStates.Grounded);
            stateMachine.Animator.CrossFadeInFixedTime(hash, 0.1f);
        }

        void FacePlayer(float deltaTime)
        {
            Vector3 playerPosition = LocationManager.PlayerPosition;
            Vector3 lookDirection = playerPosition - stateMachine.transform.position;
            lookDirection.y = 0;

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, lookRotation, 2.0f * deltaTime);
        }

    }
}

