using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FintieStateMachine
{
    public abstract class State
    {
        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
}

