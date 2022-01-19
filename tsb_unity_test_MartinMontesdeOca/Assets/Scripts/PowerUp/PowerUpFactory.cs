using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PowerUpFactory : MonoBehaviour
{
    private readonly PowerUpConfiguration powerUpConfiguration;
    public PowerUpFactory(PowerUpConfiguration powerUpConfiguration)
    {
        this.powerUpConfiguration = powerUpConfiguration;
    }

    public Entity Create(int id)
    {       
        return powerUpConfiguration.GetPowerUpFromId(id);
    }
}
