using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFactory : MonoBehaviour
{
    private readonly ShieldConfiguration shieldConfiguration;
    public ShieldFactory(ShieldConfiguration shieldConfiguration)
    {
        this.shieldConfiguration = shieldConfiguration;
    }

    public Shield Create(int id, Vector3 position)
    {       
        return shieldConfiguration.GetShieldFromId(id);
    }
}
