using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFactory : MonoBehaviour
{
    private readonly LaserConfiguration laserConfiguration;
    public LaserFactory(LaserConfiguration laserConfiguration)
    {
        this.laserConfiguration = laserConfiguration;
    }

    public Laser Create(string id, Vector3 position, Transform container)
    {
        var laser = laserConfiguration.GetLaserFromId(id);
       
        if (!container)
            return Instantiate(laser, position, Quaternion.identity);
        else
            return Instantiate(laser, container.position, container.rotation, container);
    }
}
