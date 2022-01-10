using Unity.Mathematics;
using Unity.P;
using Unity.Entities;
using UnityEngine;


public class AddForceSystem : SystemBase
{
	protected override void OnUpdate()
	{
		/*Entities.ForEach((ref PlayerMovementData moveData, in PlayerInputData inputData) =>
		{
            moveData.move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

			//moveData.direction += Input.GetKey(inputData.upKey) ? 1 : 0;
			//moveData.direction -= Input.GetKey(inputData.downKey) ? 1 : 0;
		}).Run();*/

		Entities.ForEach((Entity player,ref PhysicsMass) =>
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
