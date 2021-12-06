using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //TODO: send score to Player
        Debug.Log("im dead!");
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"hit by: {collision.name}");
        if(collision.CompareTag("projectile"))
        {
            var bullet = collision.gameObject.GetComponent<Projectile>();
            int dmg = bullet.hitDamage;
            TakeDamage(dmg);
            bullet.DestroySelf();
        }
    }
}
