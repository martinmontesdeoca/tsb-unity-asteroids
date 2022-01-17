using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Shield configuration")]
public class ShieldConfiguration : ScriptableObject
{
    [SerializeField]
    private Shield[] shields;
    private Dictionary<int, Shield> shieldsArray;

    void Awake()
    {
        shieldsArray = new Dictionary<int, Shield>();
        foreach (var shield in shields)
        {
            shieldsArray.Add(shield.Id, shield);
        }
    }

    public Shield GetShieldFromId(int id)
    {
        if (!shieldsArray.TryGetValue(id, out var powerUp))
        {
            throw new Exception("The Shield id not exits");
        }

        return powerUp;
    }
}
