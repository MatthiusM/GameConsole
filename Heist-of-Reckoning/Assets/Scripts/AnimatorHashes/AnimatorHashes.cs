using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorHashes<T> where T : Enum
{
    private readonly Dictionary<T, int> hashes = new();

    protected AnimatorHashes()
    {
        foreach (T val in Enum.GetValues(typeof(T)))
        {
            hashes[val] = Animator.StringToHash(val.ToString());
        }
    }

    public int GetHash(T variable)
    {
        if (hashes.TryGetValue(variable, out int hash))
        {
            return hash;
        }
        else
        {
            Debug.LogError($"Hash not found for {variable}");
            return 0;
        }
    }
}
