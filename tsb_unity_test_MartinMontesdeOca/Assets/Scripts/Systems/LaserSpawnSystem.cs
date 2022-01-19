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
class LaserSpawnSystem : ComponentSystem
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
        m_ElapsedTime += Time.DeltaTime;
        if (m_ElapsedTime > 0.1f)
        {
            m_ElapsedTime = 0;
            if (PlayerManager.Instance.OnShoot)
            {
                CreateLaser();
            }
        }    

        Entities.WithAll<BulletTag>().ForEach((ref Translation trans, ref DestroyEntityComponent destroyEntity) =>
        {
            if (destroyEntity.entity != Entity.Null)
            {                                
                DestroyLaser(destroyEntity.entity, trans.Value);
            }
        });
    }

    private void DestroyLaser(Entity laser, float3 position)
    {        
        var index = laserArray.IndexOf<Entity, Entity>(laser);
        GameManager.Instance.entityManager.DestroyEntity(laser);        
        if(index!=-1)laserArray.RemoveAt(index);        
    }    

    private void CreateLaser()
    {
        SoundManager.Instance.PlaySound("sfx_laser"+ PlayerManager.Instance.LaserType.ToString(), true);
        laser = GameFactoryManager.Instance.CreateObject(GameFactoryManager.LASER, PlayerManager.Instance.LaserType, Vector3.up);
        GameManager.Instance.entityManager.SetComponentData(laser, new Translation { Value = PlayerManager.Instance.GetPlayerTransform().position });
        GameManager.Instance.entityManager.SetComponentData(laser, new Rotation { Value = Quaternion.Euler(PlayerManager.Instance.GetPlayerTransform().rotation.eulerAngles) });       
        laserArray.Add(laser);                     
    }
}
