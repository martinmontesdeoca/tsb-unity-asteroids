using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    private int laserType = 0;
    private Animator animator;
    private void Awake()
    {
        _instance = this;
    }
   
    void Start()
    {
        animator = GetComponent<Animator>();
       // GameFactoryManager.Instance.CreateObject(GameFactoryManager.SHIELD, 0.ToString(), GetPlayerTransform().position);
    }

    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PlayerManager is null");
            }
            return _instance;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {           
            Shoot();            
        }
    }

    public void Reset()
    {
        GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, transform.position);
        animator.SetBool("reset", true);
        StartCoroutine("StopResetAnim");
        transform.position = Vector3.zero;
    }

    IEnumerator StopResetAnim()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("reset", false);
        GameManager.Instance.IsResetGame = false;
    }

    public Transform GetPlayerTransform()
    {
        return transform;
    }

    public void SetLaserType()
    {
        laserType++;
        if (laserType > 2)
            laserType = 0;
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {           
            GameFactoryManager.Instance.CreateObject(GameFactoryManager.LASER, laserType.ToString(), Vector3.up);
            _canFire = Time.time + _fireRate;
        }
    }
}
