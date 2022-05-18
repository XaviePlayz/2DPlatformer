using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerControls : MonoBehaviour
{
    public int gemValue = 1;
    public int daggerValue = 1;

    private Rigidbody2D rb;
    public Animator animator;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            SoundManagerScript.PlaySound("Gem");
            Collectibles.instance.ChangeScore(gemValue);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Dagger"))
        {
            SoundManagerScript.PlaySound("Dagger");
            Weapon.weapon.ChangeDaggerAmount(daggerValue);
            Destroy(other.gameObject);
            LevelManager.instance.score += 35;
        }
        if (other.gameObject.CompareTag("Chest"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetTrigger("FindItem");
            LevelManager.instance.score += 150;
            StartCoroutine(ItemFound());
        }
    }
    IEnumerator ItemFound()
    {
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<PlayerControls>().enabled = false;
        this.GetComponent<Weapon>().enabled = false;
        yield return new WaitForSeconds(1.7f);
        rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
        this.GetComponent<PlayerMovement>().enabled = true;
        this.GetComponent<PlayerControls>().enabled = true;
        this.GetComponent<Weapon>().enabled = true;
    }
}
