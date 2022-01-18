using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFactory : MonoBehaviour
{
    private readonly ExplosionConfiguration explosionConfiguration;
    public ExplosionFactory(ExplosionConfiguration explosionConfiguration)
    {
        this.explosionConfiguration = explosionConfiguration;
    }

    public Explosion Create(int id, Vector3 position)
    {
        SoundManager.Instance.PlaySound("sfx_Explosion", true);
        return explosionConfiguration.GetExplosionFromId(id, position);
    }
}
