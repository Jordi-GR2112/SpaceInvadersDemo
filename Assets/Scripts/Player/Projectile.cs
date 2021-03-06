using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
public class Projectile : MonoBehaviour
{
    public float TimeToLive = 5f;
    public float speed = 100f;
    public int hitDamage = 1;

    public bool isEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), TimeToLive);
    }

    private void Update()
    {
        if (!isEnemy)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.up);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector2.down);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
