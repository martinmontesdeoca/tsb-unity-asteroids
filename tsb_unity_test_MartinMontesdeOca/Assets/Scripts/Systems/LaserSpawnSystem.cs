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
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };   
    private float xPos;
    private float yPos;
    private int sideCount = 0;

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
                Debug.Log("Laser Destroy ---");
                DestroyLaser(destroyEntity.entity, trans.Value);
            }
        });
    }

    private void DestroyLaser(Entity laser, float3 position)
    {
        //GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, new Vector3(position.x, position.y));
        var index = laserArray.IndexOf<Entity, Entity>(laser);
        GameManager.Instance.entityManager.DestroyEntity(laser);
        GameManager.Instance.entityManager.DestroyEntity(laserArray[index]);
        laserArray.RemoveAt(index);        
    }    

    private void CreateLaser()
    {
        SoundManager.Instance.PlaySound("sfx_laser"+ PlayerManager.Instance.LaserType.ToString(), true);
        laser = GameFactoryManager.Instance.CreateObject(GameFactoryManager.LASER, PlayerManager.Instance.LaserType, Vector3.up);
        GameManager.Instance.entityManager.SetComponentData(laser, new Translation { Value = PlayerManager.Instance.GetPlayerTransform().position });
        GameManager.Instance.entityManager.SetComponentData(laser, new Rotation { Value = Quaternion.Euler(PlayerManager.Instance.GetPlayerTransform().rotation.eulerAngles) });
        GameManager.Instance.entityManager.SetComponentData(laser, new MoveLaserComponent { speed = UnityEngine.Random.Range(10f, 12f) });
        GameManager.Instance.entityManager.SetComponentData(laser, new MoveLaserComponent { playerPosition = PlayerManager.Instance.GetPlayerTransform().position });
        laserArray.Add(laser);
        sideCount++;
        if (sideCount > 3) sideCount = 0;       
        Debug.Log("Ovni Create --- ovniArray: " + laserArray.Length);
    }
}
