using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/PowerUp configuration")]
public class PowerUpConfiguration : ScriptableObject
{
    [SerializeField]
    private PowerUp[] powerUps;
    private Dictionary<string, PowerUp> powerUpsArray;

    void Awake()
    {
        powerUpsArray = new Dictionary<string, PowerUp>();
        foreach (var powerUp in powerUps)
        {
            powerUpsArray.Add(powerUp.Id, powerUp);
        }
    }

    public PowerUp GetPowerUpFromId(string id)
    {
        if (!powerUpsArray.TryGetValue(id, out var powerUp))
        {
            throw new Exception("The PowerUp id not exits");
        }

        return powerUp;
    }
}
