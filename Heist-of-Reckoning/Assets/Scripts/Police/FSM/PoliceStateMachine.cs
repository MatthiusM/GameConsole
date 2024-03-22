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
        [SerializeField, Range(0.5f, 2.0f)]
        private float speed = 1.0f;

        public NavMeshAgent Agent { get; private set; }

        public Animator Animator { get; private set; }

        public LocationManager LocationManager { get; private set; }

        public PoliceAnimatorHashes PoliceAnimationHashes { get; private set; }

        public PoliceVision PoliceVision { get; private set; }

        public PoliceCollision PoliceCollision { get; private set; }

        public bool Running { get; private set; } = false;

        void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            PoliceVision = GetComponent<PoliceVision>();
            PoliceCollision = GetComponent<PoliceCollision>();
            LocationManager = FindChildWithTag(this.gameObject, "LocationManager").GetComponent<LocationManager>();
            PoliceAnimationHashes = new PoliceAnimatorHashes();

            SetCurrentState(new PoliceWanderState(this));
        }

        public void SetRunning(bool isRunning)
        {
            Running = isRunning;
            Agent.speed = isRunning ? speed * 2 : speed;
        }

        //https://forum.unity.com/threads/how-to-find-child-objects-with-a-specific-tag.1030315/
        GameObject FindChildWithTag(GameObject parent, string tag)
        {
            GameObject child = null;

            foreach (Transform transform in parent.transform)
            {
                if (transform.CompareTag(tag))
                {
                    child = transform.gameObject;
                    break;
                }
            }

            return child;
        }
    }

}
