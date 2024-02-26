using FintieStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerAnimations
{
    public enum PlayerParameters
    {
        Speed
    }

    public enum PlayerStates
    {
        Grounded
    }

    public class PlayerAnimatorHashes : AnimatorHashes
    {
        public PlayerAnimatorHashes() : base(typeof(PlayerStates), typeof(PlayerParameters))
        {
        }
    }
}

