using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Ovni configuration")]
public class OvniConfiguration : ScriptableObject
{
    [SerializeField]
    private Ovni[] ovni;    
    private Entity ovniEntity;
    private NativeList<Entity> ovniEntitysArray;

    void Awake()
    {
        ovniEntitysArray = new NativeList<Entity>(ovni.Length, Allocator.Persistent);
        foreach (var asteroid in ovni)
        {
            ovniEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroid.gameObject, GameManager.Instance.settingsEntityManager);
            ovniEntitysArray.Add(ovniEntity);
        }
    }

    void OnDestroy()
    {
        ovniEntitysArray.Dispose();
    }

    public Entity GetOvniFromId(int id)
    {       
        if (ovniEntitysArray[id] == null)
            throw new Exception("The Asteroid id not exits");

        return ovniEntitysArray[id];
    }
}
