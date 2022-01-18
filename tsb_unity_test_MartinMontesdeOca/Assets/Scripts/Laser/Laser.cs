using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : FactoryObject
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private int id;
    public int Id => id;  
}
