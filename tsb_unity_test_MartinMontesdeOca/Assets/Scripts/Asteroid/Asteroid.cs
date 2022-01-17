using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : FactoryObject
{
    [SerializeField]
    private int speed;
    [SerializeField]
    protected string id;
    public string Id => id;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        //rigidbody = transform.GetComponent<Rigidbody2D>();
        //Movement();
    }

    private void Movement()
    {
        rigidbody.velocity = new Vector3(
           Random.Range(-1f, 1f),
           Random.Range(-1f, 1f),
           Random.Range(-1f, 1f)
       ).normalized * speed;

        rigidbody.angularVelocity = Random.Range(-50f, 50f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       /* if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            //player.Damage();
        }
        else if (other.tag == "Laser")
        {
            if (id == "Big1")
                BigExplosion();
           
            Destroy(gameObject);
            //_UIManager.UpdateScore(10);
            Destroy(other.gameObject);

        }*/

        //Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        
    }

    private void BigExplosion()
    {
        for (int i = 0; i < 2; i++)
        {
            var randomId = Random.Range(1, 2);
            GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, "Medium" + randomId, transform.position);
        }
        
    }
}
