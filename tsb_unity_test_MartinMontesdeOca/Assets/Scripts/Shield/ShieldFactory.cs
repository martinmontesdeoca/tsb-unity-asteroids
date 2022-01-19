using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShieldFactory : MonoBehaviour
{
    private readonly ShieldConfiguration shieldConfiguration;
    public ShieldFactory(ShieldConfiguration shieldConfiguration)
    {
        this.shieldConfiguration = shieldConfiguration;
    }

    public Entity Create(int id, Vector3 position)
    {       
        return shieldConfiguration.GetShieldFromId(id);
    }
}
