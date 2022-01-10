using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Laser configuration")]
public class LaserConfiguration : ScriptableObject
{
    [SerializeField]
    private Laser[] lasers;
    private Dictionary<string, Laser> laserArray;

    void Awake()
    {
        laserArray = new Dictionary<string, Laser>();
        foreach (var laser in lasers)
        {
            laserArray.Add(laser.Id, laser);
        }
    }

    public Laser GetLaserFromId(string id)
    {
        if (!laserArray.TryGetValue(id, out var laser))
        {
            throw new Exception("The Laser id not exits");
        }

        return laser;
    }
}
