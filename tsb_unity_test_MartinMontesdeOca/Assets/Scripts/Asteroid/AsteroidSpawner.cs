using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float time;
    //public GameObject[] prefabs;
    //public Transform[] container;  
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };
    string[] names = new string[] { "Big", "Medium", "Small", "Tiny" };

    IEnumerator Start()
    {
        while (true)
        {
            var randomPos = Random.Range(0, 4);
            var randomType = Random.Range(0, 3);
            var randomId = Random.Range(1, 2);
            if(randomType == 0)
                randomId = Random.Range(1, 4);

            //Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(x_pos[randomPos], y_pos[randomPos], 0), Quaternion.identity);
            GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType]+ randomId, new Vector3(x_pos[randomPos], y_pos[randomPos], 0),null);
            yield return new WaitForSeconds(time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
