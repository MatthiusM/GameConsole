using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class PoliceStateMachine : FiniteStateMachine
    {
        public NavMeshAgent Agent { get; private set; }

        public Animator Animator { get; private set; }

        public LocationManager LocationManager { get; private set; }

        void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            LocationManager = GameObject.FindGameObjectWithTag("LocationManager").GetComponent<LocationManager>();

            SetCurrentState(new PoliceWanderState(this));
        }
    }

}
