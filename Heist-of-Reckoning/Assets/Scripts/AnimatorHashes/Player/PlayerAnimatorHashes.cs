using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerAnimations
{
    public enum PlayerParameters
    {
        Speed,
        Posture
    }

    public enum PlayerStates
    {
        Grounded,
        Jump,
    }

    public class PlayerAnimatorHashes : AnimatorHashes
    {
        public PlayerAnimatorHashes() : base(typeof(PlayerStates), typeof(PlayerParameters))
        {
        }
    }
}

