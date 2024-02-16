using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FintieStateMachine
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        State currentState;

        void Update()
        {
            currentState?.Update();
        }

        public void SetCurrentState(State state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }
    }

}
