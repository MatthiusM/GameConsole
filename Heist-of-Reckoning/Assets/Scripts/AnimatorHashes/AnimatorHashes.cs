using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class AnimatorHashes
{
    private readonly Dictionary<Type, Dictionary<Enum, int>> allHashes = new();

    protected AnimatorHashes(params Type[] enumTypes)
    {
        foreach (var enumType in enumTypes)
        {
            if (!enumType.IsEnum)
            {
                Debug.LogError($"{enumType} is not an enum.");
                continue;
            }

            var enumDict = new Dictionary<Enum, int>();
            foreach (Enum val in Enum.GetValues(enumType))
            {
                enumDict[val] = Animator.StringToHash(val.ToString());
            }
            allHashes[enumType] = enumDict;
        }
    }

    public int GetHash<T>(T variable) where T : Enum
    {
        var enumType = typeof(T);
        if (allHashes.TryGetValue(enumType, out var hashes) && hashes.TryGetValue(variable, out int hash))
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
