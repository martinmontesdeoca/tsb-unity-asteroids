
using Unity.Entities;

public class AutoDestroySystem : ComponentSystem
{   
    protected override void OnUpdate()
    {      
        Entities.WithAll<AutoDestroyComponent>().ForEach((Entity entity, ref AutoDestroyComponent lifetime) =>
        {         
            lifetime.Value -= Time.DeltaTime;
            if (lifetime.Value <= 0)
            {
                GameManager.Instance.entityManager.SetComponentData(entity, new DestroyEntityComponent { entity = entity });                              
            }
        });

        Entities.WithAll<ShieldTag>().ForEach((Entity entity, ref AutoDestroyComponent lifetime) =>
        {
            lifetime.Value -= Time.DeltaTime;
            if (lifetime.Value <= 0)
            {
                GameManager.Instance.entityManager.DestroyEntity(entity);
            }
        });
    }
}
