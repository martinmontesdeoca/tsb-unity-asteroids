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
class OvniSpawnSystem : ComponentSystem
{
    private Entity ovni;
    private float m_ElapsedTime;       
    private NativeList<Entity> ovniArray;
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };   
    private float xPos;
    private float yPos;
    private int sideCount = 0;

    protected override void OnCreate()
    {
        base.OnCreate();
        ovniArray = new NativeList<Entity>(2, Allocator.Persistent);        
    }

    protected override void OnDestroy()
    {
        ovniArray.Dispose();
    }

    protected override void OnUpdate()
    {
        m_ElapsedTime += Time.DeltaTime;
        if (m_ElapsedTime > 2f)
        {
            m_ElapsedTime = 0;
            if (ovniArray.Length < 2)
            {
                CreateOvni();
            }
        }    

        Entities.WithAll<OvniTag>().ForEach((ref Translation trans, ref DestroyEntityComponent destroyEntity) =>
        {
            if (destroyEntity.entity != Entity.Null)
            {                
                Debug.Log("Ovni Destroy ---");
                DestroyOvni(destroyEntity.entity, trans.Value);
            }
        });
    }

    private void DestroyOvni(Entity ovni, float3 position)
    {
        GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, new Vector3(position.x, position.y));
        var index = ovniArray.IndexOf<Entity, Entity>(ovni);
        GameManager.Instance.entityManager.DestroyEntity(ovni);       
        ovniArray.RemoveAt(index);        
    }    

    private void CreateOvni()
    {
        var randomPos = sideCount;       
        var randomId = UnityEngine.Random.Range(0, 2);
       
        if (randomPos == 0)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = -10f;// y_pos[randomPos];
        }
        if (randomPos == 1)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);

        }
        if (randomPos == 2)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = 10f;// y_pos[randomPos];

        }
        if (randomPos == 3)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);

        }

        var position = new Vector3(xPos, yPos, 0);
        ovni = GameFactoryManager.Instance.CreateObject(GameFactoryManager.OVNI, randomId, position);
        GameManager.Instance.entityManager.SetComponentData(ovni, new Translation { Value = position });
        GameManager.Instance.entityManager.SetComponentData(ovni, new MoveComponent { forceSide = sideCount, speed = UnityEngine.Random.Range(1f, 3f) });
        ovniArray.Add(ovni);
        sideCount++;
        if (sideCount > 3) sideCount = 0;
        
        Debug.Log("Ovni Create --- ovniArray: " + ovniArray.Length);
    }
}
//}
