using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LaserFactory : MonoBehaviour
{
    private readonly LaserConfiguration laserConfiguration;
    public LaserFactory(LaserConfiguration laserConfiguration)
    {
        this.laserConfiguration = laserConfiguration;
    }

    public Entity Create(int id, Vector3 position)
    {      
        return laserConfiguration.GetLaserFromId(id);
    }
}
