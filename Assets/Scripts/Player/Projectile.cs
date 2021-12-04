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

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), TimeToLive);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
