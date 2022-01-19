using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/PowerUp configuration")]
public class PowerUpConfiguration : ScriptableObject
{
    [SerializeField]
    private PowerUp[] powerUps;
    private Entity powerUpEntity;
    private NativeList<Entity> powerUpEntitysArray;

    void Awake()
    {
        powerUpEntitysArray = new NativeList<Entity>(powerUps.Length, Allocator.Persistent);
        foreach (var asteroid in powerUps)
        {
            powerUpEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroid.gameObject, GameManager.Instance.settingsEntityManager);
            powerUpEntitysArray.Add(powerUpEntity);
        }
    }

    void OnDestroy()
    {
        powerUpEntitysArray.Dispose();
    }

    public Entity GetPowerUpFromId(int id)
    {
        if (powerUpEntitysArray[id] == null)
            throw new Exception("The PowerUp id not exits");

        return powerUpEntitysArray[id];
    }
}
