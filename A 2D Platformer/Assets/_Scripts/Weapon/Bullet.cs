using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int gemValue = 1;
    public int daggerValue = 1;

    public float speed = 20f;
    private int damage = 25;
    public Rigidbody2D rb;
    public GameObject hitEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Slime slime = hitInfo.GetComponent<Slime>();
        if (slime != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            LevelManager.instance.score += 8;
            slime.TakeDamage(damage);
        }

        if (hitInfo.gameObject.CompareTag("Gem"))
        {
            SoundManagerScript.PlaySound("Gem");
            Collectibles.instance.ChangeScore(gemValue);
            LevelManager.instance.score += 8;
            Destroy(hitInfo.gameObject);
        }

        if (hitInfo.gameObject.CompareTag("Dagger"))
        {
            SoundManagerScript.PlaySound("Dagger");
            Weapon.weapon.ChangeDaggerAmount(daggerValue);
            Destroy(hitInfo.gameObject);
            LevelManager.instance.score += 35;
        }
        LevelManager.instance.score += 4;

        Destroy(gameObject);
    }
}
