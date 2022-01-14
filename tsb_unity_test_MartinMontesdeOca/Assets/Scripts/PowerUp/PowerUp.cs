using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : FactoryObject
{
    [SerializeField]
    protected string id;
    public string Id => id;
}
