using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysUpdateSystem]
class OvniLaserSpawnSystem : ComponentSystem
{
    private Entity laser;
    private float m_ElapsedTime;       
    private NativeList<Entity> laserArray;    

    protected override void OnCreate()
    {
        base.OnCreate();
        laserArray = new NativeList<Entity>(10, Allocator.Persistent);        
    }

    protected override void OnDestroy()
    {
        laserArray.Dispose();
    }

    protected override void OnUpdate()
    {        
        Entities.WithAll<OvniLaserTag>().ForEach((ref Translation trans, ref DestroyEntityComponent destroyEntity) =>
        {
            if (destroyEntity.entity != Entity.Null)
            {                
                Debug.Log("Laser Destroy ---");
                DestroyLaser(destroyEntity.entity, trans.Value);
            }
        });

        Entities.WithAll<OvniTag>().ForEach((ref Translation trans, ref OvniShootComponent ovniShoot) =>
        {
            if (ovniShoot.shoot == true)                         
                CreateLaser(ovniShoot.position, ovniShoot.rotation);
           
        });
    }

    private void DestroyLaser(Entity laser, float3 position)
    {       
        var index = laserArray.IndexOf<Entity, Entity>(laser);
        GameManager.Instance.entityManager.DestroyEntity(laser);       
        if(index!=-1)laserArray.RemoveAt(index);        
    }    

    private void CreateLaser(Vector3 position, Quaternion rotation)
    {        
        SoundManager.Instance.PlaySound("sfx_laser2", true);
        laser = GameFactoryManager.Instance.CreateObject(GameFactoryManager.LASER, 3, Vector3.zero);
        GameManager.Instance.entityManager.SetComponentData(laser, new Translation { Value = position });
        GameManager.Instance.entityManager.SetComponentData(laser, new Rotation { Value =  Quaternion.Euler(PlayerManager.Instance.GetPlayerTransform().rotation.eulerAngles) });      
        laserArray.Add(laser);                   
    }
}
