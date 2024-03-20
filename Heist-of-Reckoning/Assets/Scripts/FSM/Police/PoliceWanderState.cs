using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FiniteStateMachine
{
    public class PoliceWanderState : PoliceState
    {

        Vector3 wanderLocation;

        public PoliceWanderState(PoliceStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void Enter()
        {
            wanderLocation = stateMachine.LocationManager.GetRandomLocation(stateMachine.transform.position);
            Move(wanderLocation);
        }

        public override void Exit()
        {

        }

        public override void Update(float deltaTime)
        {
            SwitchToWander();
        }

        void SwitchToWander()
        {
            if (Vector3.Distance(stateMachine.transform.position, wanderLocation) < 0.2f)
            {
                stateMachine.SetCurrentState(new PoliceWanderState(stateMachine));
            }
        }
    }
}

