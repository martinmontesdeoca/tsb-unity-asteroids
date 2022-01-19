using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class OvniFactory : MonoBehaviour
{
    private readonly OvniConfiguration ovniConfiguration;
    public OvniFactory(OvniConfiguration ovniConfiguration)
    {
        this.ovniConfiguration = ovniConfiguration;
    }

    public Entity Create(int id)
    {       
        return ovniConfiguration.GetOvniFromId(id);
    }
}
