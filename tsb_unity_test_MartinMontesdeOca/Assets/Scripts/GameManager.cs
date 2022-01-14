using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    [SerializeField]
    private Transform playerTransform;
    public EntityManager entityManager;
   // EntityManager manager;
    private void Awake()
    {
        main = this;
        _instance = this;
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //  manager = World.DefaultGameObjectInjectionWorld.EntityManager;

    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }

    public static bool IsGameOver()
    {
        if (GameManager.Instance == null)
        {
            return false;
        }

        return false;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
