using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public static PlayerHealth instance;
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody2D rb;
    public GameObject player, finishLine;

    public Animator animator;

    public float timeValue;

    public bool hasFinished = false, hasDied = false, losingHealth = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        if (timeValue == 0)
        {
            health = 0;
        }
        if (hasDied == true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (hasFinished == true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (health <= 0)
        {
            StartCoroutine(Death());
        }

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {      
        if (other.gameObject.CompareTag("Enemy"))
        {
            LevelManager.instance.score += 24;
            if (losingHealth == false)
            {
                losingHealth = true;
                if (health > 0)
                {
                    SoundManagerScript.PlaySound("Hit");
                    animator.SetTrigger("isHit");
                }
                health--;
            }
            StartCoroutine(IsLosingHealth());            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            SoundManagerScript.PlaySound("Heart");
            LevelManager.instance.score += 11;
            health++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Void"))
        {
            LevelManager.instance.score += 24;
            health = 0;
        }
        if (other.gameObject.CompareTag("Chest"))
        {
            numOfHearts++;
            health++;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            animator.SetBool("IsFinished", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            player.transform.position = finishLine.transform.position;
            StartCoroutine(Victory());
        }
    }
    IEnumerator IsLosingHealth()
    {
        yield return new WaitForSeconds(2f);
        losingHealth = false;
    }

    IEnumerator Death()
    {
        SoundManagerScript.PlaySound("Death");
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<PlayerControls>().enabled = false;
        this.GetComponent<Weapon>().enabled = false;

        animator.SetTrigger("Death");

        yield return new WaitForSeconds(1.2f);
        LevelManager.instance.GameOverScreen.SetActive(true);
        LevelManager.instance.totalDeathPointsText.text = LevelManager.instance.score.ToString() + " Points";
        Destroy(this.gameObject);
        hasDied = true;
    }
    IEnumerator Victory()
    {
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<PlayerControls>().enabled = false;
        this.GetComponent<Weapon>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        LevelManager.instance.VictoryScreen.SetActive(true);
        LevelManager.instance.totalVictoryPointsText.text = LevelManager.instance.score.ToString() + " Points";
        hasFinished = true;
    }
}