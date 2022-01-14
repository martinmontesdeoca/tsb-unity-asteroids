
using Unity.Entities;

public class TimeoutSystem : ComponentSystem
{   
    protected override void OnUpdate()
    {      
        Entities.WithAll<LifetimeComponent>().ForEach((Entity entity, ref LifetimeComponent lifetime) =>
        {         
            lifetime.Value -= Time.DeltaTime;
            if (lifetime.Value <= 0)
            {
                PostUpdateCommands.DestroyEntity(entity);
            }

        });
    }
}
