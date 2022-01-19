using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Laser configuration")]
public class LaserConfiguration : ScriptableObject
{
    [SerializeField]
    private Laser[] lasers; 
    private Entity laserEntity;
    private NativeList<Entity> laserEntitysArray;

    void Awake()
    {    
        laserEntitysArray = new NativeList<Entity>(lasers.Length, Allocator.Persistent);
        foreach (var asteroid in lasers)
        {
            laserEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroid.gameObject, GameManager.Instance.settingsEntityManager);
            laserEntitysArray.Add(laserEntity);
        }
    }

    public Entity GetLaserFromId(int id)
    {        
        if (laserEntitysArray[id] == null)
            throw new Exception("The Asteroid id not exits");

        return laserEntitysArray[id];
    }
}
