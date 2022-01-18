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
class AsteroidSpawnSystem : ComponentSystem
{
    private Entity asteroid;
    private float m_ElapsedTime;       
    private NativeList<Entity> asteroidArray;
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };   
    private float xPos;
    private float yPos;
    private int sideCount = 0;   

    protected override void OnCreate()
    {
        base.OnCreate();
        asteroidArray = new NativeList<Entity>(10, Allocator.Persistent);        
    }

    protected override void OnDestroy()
    {
        asteroidArray.Dispose();
    }

    protected override void OnUpdate()
    {
        m_ElapsedTime += Time.DeltaTime;
        if (m_ElapsedTime > 2f)
        {
            m_ElapsedTime = 0;
            if (asteroidArray.Length < 10)
            {
                CreateAsteroid();
            }
        }    

        Entities.WithAll<AsteroidTag>().ForEach((ref Translation trans, ref DestroyEntityComponent destroyEntity) =>
        {
            if (destroyEntity.entity != Entity.Null)
            {
                Debug.Log("Asteroid Destroy ---");
                DestroyAsteroid(destroyEntity.entity, trans.Value);
            }
        });
    }

    private void DestroyAsteroid(Entity asteroid, float3 position)
    {
        GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, new Vector3(position.x,position.y));
        var index = asteroidArray.IndexOf<Entity, Entity>(asteroid);       
        GameManager.Instance.entityManager.DestroyEntity(asteroid);       
        if (index != -1)asteroidArray.RemoveAt(index);        
    }    

    private void CreateAsteroid()
    {
        var randomPos = sideCount;
        var randomType = UnityEngine.Random.Range(0, 3);
        var randomId = UnityEngine.Random.Range(0, 7);     

        if (randomPos == 0)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = -10f;
        }
        if (randomPos == 1)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);

        }
        if (randomPos == 2)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = 10f;

        }
        if (randomPos == 3)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);
        }

        var position = new Vector3(xPos, yPos, 0);
        asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, randomId, position);
        GameManager.Instance.entityManager.SetComponentData(asteroid, new Translation { Value = position });
        GameManager.Instance.entityManager.SetComponentData(asteroid, new MoveComponent { forceSide = sideCount, speed = UnityEngine.Random.Range(1f, 3f) });
        asteroidArray.Add(asteroid);
        sideCount++;
        if (sideCount > 3) sideCount = 0;
    
        Debug.Log("Asteroid Create --- asteroidArray: " + asteroidArray.Length);
    }
}

