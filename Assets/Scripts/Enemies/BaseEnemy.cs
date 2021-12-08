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

    public GameObject EnemyLaser;
    public Transform LaserSpawner;
    public float chanceToShoot = 5f; //shoot validation cooldown in seconds


    private void Start()
    {
        InvokeRepeating("CheckShootingProb", chanceToShoot, chanceToShoot);
    }

    private void CheckShootingProb()
    {
        if(Random.value < (1f / EnemySpawner.Instance.ReturnEnemiesLeft()))
        {
            Instantiate(EnemyLaser, LaserSpawner.position, Quaternion.identity);
            GameManager.Instance.PlayShootingPlayer();
        }                    
    }

    public void TakeDamage(int hitDamage)
    {
        HitPoint -= hitDamage;

        Debug.Log("Taking damage!");

        if(HitPoint <= 0)
        {
            DestroySelf();
        }
        else
        {
            GameManager.Instance.PlayEnemyDamaged();
        }
    }

    public void DestroySelf()
    {
        var middleOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, Camera.main.nearClipPlane));
        var enemypos = EnemySpawner.Instance.ReturnSpawnPosition();

        int multiplier = 1;

        if(enemypos.y < middleOfScreen.y)
        {
            multiplier = 1;
        }
        else if(enemypos.y > middleOfScreen.y)
        {
            multiplier = 3;
        }

        GameManager.Instance.UpdateScore(Points*multiplier);
        GameManager.Instance.PlayEnemyDeath();
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
