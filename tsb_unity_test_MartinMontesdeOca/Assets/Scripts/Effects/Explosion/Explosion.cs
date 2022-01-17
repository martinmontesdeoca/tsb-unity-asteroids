using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : FactoryObject
{
    [SerializeField]
    protected int id;
    public int Id => id;

    private void Start()
    {
       Destroy(this.gameObject, 0.45f);
    }    
}
