using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerInputSystem : JobComponentSystem
{
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		/*Entities.ForEach((ref PlayerMovementData moveData, in PlayerInputData inputData) =>
		{
            moveData.move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

			//moveData.direction += Input.GetKey(inputData.upKey) ? 1 : 0;
			//moveData.direction -= Input.GetKey(inputData.downKey) ? 1 : 0;
		}).Run();*/

		Entities.ForEach((ref PaddleMovementData moveData, in PaddleInputData inputData) =>
		{
			moveData.vertical = 0;
			//moveData.horizontal = Input.GetAxis("Vertical");
			moveData.horizontal += Input.GetKey(inputData.rightKey) ? 1 : 0;
			moveData.horizontal -= Input.GetKey(inputData.leftKey) ? 1 : 0;
			moveData.vertical += Input.GetKey(inputData.upKey) ? 1 : 0;
			moveData.vertical -= Input.GetKey(inputData.downKey) ? 1 : 0;
		}).Run();

		return default;
	}
}
