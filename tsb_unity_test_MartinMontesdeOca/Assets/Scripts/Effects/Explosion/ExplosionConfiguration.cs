using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Explosion configuration")]
public class ExplosionConfiguration : ScriptableObject
{
    [SerializeField]
    private Explosion[] explosions;
    private Dictionary<int, Explosion> explosionArray;

    void Awake()
    {
        explosionArray = new Dictionary<int, Explosion>();
        foreach (var explosion in explosions)
        {
            explosionArray.Add(explosion.Id, explosion);
        }
    }

    public Explosion GetExplosionFromId(int id, Vector3 position)
    {
        if (!explosionArray.TryGetValue(id, out var explosion))
        {
            throw new Exception("The Explosion id not exits");
        }

        return Instantiate(explosion, position,Quaternion.identity);
    }
}
