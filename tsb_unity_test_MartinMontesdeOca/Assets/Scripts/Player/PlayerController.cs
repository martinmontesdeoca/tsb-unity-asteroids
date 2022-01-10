using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    private GameObject bulletsContainer;

    private void Awake()
    {
        //GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
       // ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ballPrefab, settings);
    }

    void Start()
    {
        bulletsContainer = GameObject.Find("Bullets");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {           
            Shoot();            
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            //Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            GameFactoryManager.Instance.CreateObject(GameFactoryManager.LASER, "Laser1", Vector3.up, bulletsContainer.transform);
            _canFire = Time.time + _fireRate;
        }
    }

    
}
