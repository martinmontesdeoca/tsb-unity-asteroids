using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Shield configuration")]
public class ShieldConfiguration : ScriptableObject
{
    [SerializeField]
    private Shield[] shields;
    private Entity shieldEntity;
    private NativeList<Entity> shieldEntitysArray;

    void Awake()
    {
        shieldEntitysArray = new NativeList<Entity>(shields.Length, Allocator.Persistent);
        foreach (var asteroid in shields)
        {
            shieldEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroid.gameObject, GameManager.Instance.settingsEntityManager);
            shieldEntitysArray.Add(shieldEntity);
        }
    }

    void OnDestroy()
    {
        shieldEntitysArray.Dispose();
    }

    public Entity GetShieldFromId(int id)
    {
        if (shieldEntitysArray[id] == null)
            throw new Exception("The Shield id not exits");

        return shieldEntitysArray[id];
    }
}
