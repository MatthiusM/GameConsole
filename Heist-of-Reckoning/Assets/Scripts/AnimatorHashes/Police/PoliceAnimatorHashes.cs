using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoliceAnimation {

    public enum PoliceParameters
    {
        Speed,
    }

    public enum PoliceStates
    {
        Grounded,
    }

    public class PoliceAnimatorHashes : AnimatorHashes
    {
        public PoliceAnimatorHashes() : base(typeof(PoliceStates), typeof(PoliceParameters))
        {
        }
    }
}

