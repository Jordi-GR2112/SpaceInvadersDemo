using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
/// 
public class BaseEnemy : MonoBehaviour
{
    public string Type; //green, blue, red...
    public Sprite Sprite;
    public int HitPoint; //green = 1, blue = 2, red = 3...
    public int Points; //green = 1, blue = 2, red = 3...

    public void TakeDamage(int hitDamage)
    {
        HitPoint -= hitDamage;

        Debug.Log("Taking damage!");

        if(HitPoint <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        GameManager.Instance.UpdateScore(Points);
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("projectile"))
        {
            var bullet = collision.gameObject.GetComponent<Projectile>();
            int dmg = bullet.hitDamage;
            TakeDamage(dmg);
            bullet.DestroySelf();
        }
    }
}
