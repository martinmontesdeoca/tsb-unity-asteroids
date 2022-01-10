using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var x = transform.position.x;
        var y = transform.position.y;

        if (x > 9.5)
        {
            pos.x = -9.5f;
            transform.position = pos;
        }
        if (x < -9.5)
        {
            pos.x = 9.5f;
            transform.position = pos;
        }

        if (y > 5.5)
        {
            pos.y = -5.5f;
            transform.position = pos;
        }
        if (y < -5.5f)
        {
            pos.y = 5.5f;
            transform.position = pos;
        }
    }
}
