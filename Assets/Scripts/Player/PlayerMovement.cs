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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) //Right movement
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))//Left movement
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Firing!
            if(Time.time > (1/fireRate) + lastFired)
            {
                Debug.Log("im firing!");
                Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
                lastFired = Time.time;

            }
        }

    }
}
