using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : FactoryObject
{
    [SerializeField]
    private int speed;
    [SerializeField]
    protected int id;
    public int Id => id;
}
