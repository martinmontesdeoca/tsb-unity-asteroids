using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Ovni configuration")]
public class OvniConfiguration : ScriptableObject
{
    [SerializeField]
    private Ovni[] ovni;
    private Dictionary<int, Ovni> ovniArray;

    void Awake()
    {
        ovniArray = new Dictionary<int, Ovni>();
        foreach (var ovni in ovni)
        {
            ovniArray.Add(ovni.Id, ovni);
        }
    }

    public Ovni GetOvniFromId(int id)
    {
        if (!ovniArray.TryGetValue(id, out var ovni))
        {
            throw new Exception("The PowerUp id not exits");
        }

        return ovni;
    }
}
