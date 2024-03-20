using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PoliceAnimation;

namespace FiniteStateMachine
{
    public class PoliceStateMachine : FiniteStateMachine
    {
        public NavMeshAgent Agent { get; private set; }

        public Animator Animator { get; private set; }

        public LocationManager LocationManager { get; private set; }

        public PoliceAnimatorHashes PoliceAnimationHashes { get; private set; }

        public bool Running { get; private set; } = false;

        void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            LocationManager = GameObject.FindGameObjectWithTag("LocationManager").GetComponent<LocationManager>();
            PoliceAnimationHashes = new PoliceAnimatorHashes();

            SetCurrentState(new PoliceWanderState(this));
        }
    }

}
