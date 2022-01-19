using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Asteroids configuration")]
public class AsteroidConfiguration : ScriptableObject
{
    [SerializeField]
    private Asteroid[] asteroids;
    private Entity asteroidEntity;    
    private NativeList<Entity> asteroidEntitysArray;

    void Awake()
    {
        asteroidEntitysArray = new NativeList<Entity>(asteroids.Length, Allocator.Persistent);
        foreach (var asteroid in asteroids)
        {
            asteroidEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroid.gameObject, GameManager.Instance.settingsEntityManager);
            asteroidEntitysArray.Add(asteroidEntity);
        }
    }    

    void OnDestroy()
    {
        asteroidEntitysArray.Dispose();
    }

    public Entity GetAsteroidFromId(int id)
    {        
        if(asteroidEntitysArray[id] == null)
            throw new Exception("The Asteroid id not exits");

        return asteroidEntitysArray[id];
    }
}
