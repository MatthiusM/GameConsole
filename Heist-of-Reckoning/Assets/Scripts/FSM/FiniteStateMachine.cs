using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        State currentState;

        void Update()
        {
            currentState?.Update(Time.deltaTime);
        }

        public void SetCurrentState(State state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }
    }

}
