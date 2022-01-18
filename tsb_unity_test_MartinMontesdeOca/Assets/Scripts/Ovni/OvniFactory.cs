using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniFactory : MonoBehaviour
{
    private readonly OvniConfiguration ovniConfiguration;
    public OvniFactory(OvniConfiguration ovniConfiguration)
    {
        this.ovniConfiguration = ovniConfiguration;
    }

    public Ovni Create(int id)
    {       
        return ovniConfiguration.GetOvniFromId(id);
    }
}
