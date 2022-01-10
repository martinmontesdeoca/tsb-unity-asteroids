using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class PlayerMovementSystem : JobComponentSystem
{
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float deltaTime = Time.DeltaTime;
		float yBound = 5f;// GameManager.main.yBound;
		float xBound = 5f;// GameManager.main.yBound;

		/*Entities.ForEach((ref Translation trans, in PlayerMovementData data) =>
		{
			trans.Value.y = math.clamp(trans.Value.y + (data.aceleration * 5f * deltaTime), -yBound, yBound);
		}).Run();*/

		Entities.ForEach((ref Translation trans, ref Rotation rotation, in PaddleMovementData data) =>
		{
			trans.Value.x = math.clamp(trans.Value.x + (data.speed * data.horizontal * deltaTime), -xBound, xBound);
			trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.vertical * deltaTime), -yBound, yBound);
			//trans.Value.z = -90 * data.horizontal * deltaTime;
			rotation.Value.value.z = -90 * 1 * deltaTime;

		}).Run();

		return default;
	}
}
