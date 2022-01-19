using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float _fireRate = 0.25f;
    private float _canFire = 0.25f;
    private int laserType = 0;
    private bool onHyperspace = false;
    private bool onShoot = false;
    private Animator animator;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        _instance = this;
    }
   
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = transform.GetComponent<Rigidbody2D>();
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

    public bool OnHyperspace
    {
        get { return onHyperspace; }
        set { onHyperspace = value; }
    }

    public bool OnShoot
    {
        get { return onShoot; }
        set { onShoot = value; }
    }

    public int LaserType
    {
        get { return laserType; }
        set { laserType = value; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.H) && !OnHyperspace)
            JumpToHyperspace();
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            StopShoot();
    }

    public void Reset()
    {
        GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, transform.position);
        animator.SetBool("reset", true);
        StartCoroutine("StopResetAnim");
        //transform.position = Vector3.zero;
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidBody.angularVelocity = 0;
        rigidBody.velocity =Vector2.zero;
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
        LaserType++;       
        if (LaserType > 2)
            LaserType = 0;
    }

    private void JumpToHyperspace()
    {
        SoundManager.Instance.PlaySound("sfx_Teleportation Jump");
        OnHyperspace = true;        
        transform.position = new Vector3(UnityEngine.Random.Range(9.5f, -9.5f), UnityEngine.Random.Range(5.5f, -5.5f));
        animator.SetBool("hyperspace", true);
        StartCoroutine("StopHyperspaceAnim");
    }

    IEnumerator StopHyperspaceAnim()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("hyperspace", false);        
       
        var randomDestroy = UnityEngine.Random.Range(0, 10);
        if(randomDestroy > 5)
        {
            GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, transform.position);
            GameManager.Instance.ResetGame();
        }

        OnHyperspace = false;
    }

    private void Shoot()
    {
        OnShoot = true;
    }

    private void StopShoot()
    {
        OnShoot = false;
    }
}
