using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;

    public int health = 100;
    private float dazedTime;
    public float startDazedTime;

    public GameObject deathEffect;
    public GameObject hitEffect;

    void Update()
    {
        if (dazedTime <= 0)
        {
            speed = 3;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            Die();
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }       
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        SoundManagerScript.PlaySound("SlimeHit");
        dazedTime = startDazedTime;
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        health -= damage;
    }

    void Die()
    {
        SoundManagerScript.PlaySound("SlimeDeath");
        LevelManager.instance.score += 23;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }   
}
