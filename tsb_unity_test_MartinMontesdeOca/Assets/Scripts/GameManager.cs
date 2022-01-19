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
    private bool resetGame = false;
    public GameObjectConversionSettings settingsEntityManager;    
    private BlobAssetStore blobAssetStore;
    private void Awake()
    {
        main = this;
        _instance = this;
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        settingsEntityManager = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
    }

    private void OnDestroy()
    {
        blobAssetStore.Dispose();
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

    public bool IsResetGame {
        get { return resetGame; }
        set { resetGame = value; }
    }
   
    public void ResetGame()
    {        
        SoundManager.Instance.PlaySound("sfx_lose", true);
        IsResetGame = true;
        PlayerManager.Instance.Reset();
    }    
}
