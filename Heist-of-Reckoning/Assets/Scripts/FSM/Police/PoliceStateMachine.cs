using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PoliceStateMachine : FiniteStateMachine
    {
        // Start is called before the first frame update
        void Start()
        {
            SetCurrentState(new PoliceWanderState(this));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
