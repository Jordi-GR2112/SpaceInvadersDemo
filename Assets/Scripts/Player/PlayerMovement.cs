using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float speed = 50f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    private float lastFired;

    //Lock movement to screen
    public Camera mainCamera;
    private Vector2 screenLimits;
    private float playerWd;
    private float playerHt;

    Touch touch;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        screenLimits = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        playerWd = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        playerHt = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //Right movement
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//Left movement
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //Firing!
            if(Time.time > (1/fireRate) + lastFired)
            {
                Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
                GameManager.Instance.PlayShootingPlayer();
                lastFired = Time.time;
            }
        }

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                if(touch.deltaPosition.x > 0)
                {
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else if(touch.deltaPosition.x < 0)
                {
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
            }
            else if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Stationary)
            {
                if (Time.time > (1 / fireRate) + lastFired)
                {
                    Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
                    lastFired = Time.time;
                }
            }
        }


    }

    private void LateUpdate() //Ship position is kept within screen boundaries. 
    {
        Vector3 currPos = transform.position;

        currPos.x = Mathf.Clamp(currPos.x, -screenLimits.x + playerWd, screenLimits.x - playerWd);
        currPos.y = Mathf.Clamp(currPos.y, -screenLimits.y + playerHt, screenLimits.y - playerHt);
        transform.position = currPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"hit by: {collision.name}");
        if (collision.CompareTag("enemy"))
        {
            GameManager.Instance.GameOver();
            DestroySelf();
            Debug.Log("game over!");
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
