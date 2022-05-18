using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{
    private Rigidbody2D rb;

    public static Weapon weapon;
    public GameObject noDaggers;
    public TextMeshProUGUI AmountText;

    public int damage;

    public Transform firePoint;
    public GameObject daggerPrefab;
    public Animator animator;

    [Header("Attacks")]
    [Space]
    public int daggerAmount;
    public Slider slider;

    [Header("Melee Settings")]
    [Space]
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemies;

    [Header("Special Attack Settings")]
    [Space]
    public int manaValue;
    public Transform specialAttackPos;
    public float specialAttackRangeX;
    public float specialAttackRangeY;
    [Space]
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    void Start()
    {
        noDaggers.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

        slider.value = 0;
        AmountText.text = daggerAmount.ToString();
        if (weapon == null)
        {
            weapon = this;
        }
    }
    void Update()
    {
        if (daggerAmount == 0)
        {
            noDaggers.SetActive(true);
        }
        else
        {
            noDaggers.SetActive(false);
        }
        
        if (timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (timeBtwAttack <= 0)
                {
                    SoundManagerScript.PlaySound("Sword");
                    animator.SetTrigger("Melee");
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Slime>().TakeDamage(40);
                    }
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire2") && daggerAmount > 0 || Input.GetKeyDown(KeyCode.E) && daggerAmount > 0)
            {
                SoundManagerScript.PlaySound("DaggerThrow");
                animator.SetTrigger("Shoot");
                Instantiate(daggerPrefab, firePoint.position, firePoint.rotation);
                daggerAmount -= 1;
                AmountText.text = daggerAmount.ToString();
                timeBtwAttack = startTimeBtwAttack;
            }
        }


        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && slider.value == 30 || Input.GetKeyDown(KeyCode.R) && slider.value == 30)
            {
                animator.SetTrigger("Special Attack");
                slider.value = 0;
                StartCoroutine(SpecialAttack());
                timeBtwAttack = startTimeBtwAttack;
                LevelManager.instance.score += 70;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mana Potion"))
        {
            SoundManagerScript.PlaySound("Mana Potion");
            Destroy(other.gameObject);
            slider.value += manaValue;
            LevelManager.instance.score += 35;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(specialAttackPos.position, new Vector3(specialAttackRangeX, specialAttackRangeY, 1));
    }
    public void ChangeDaggerAmount(int daggerValue)
    {
        daggerAmount += daggerValue;
        AmountText.text = daggerAmount.ToString();
    }

    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(0.7f);
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(specialAttackPos.position, new Vector2(specialAttackRangeX, specialAttackRangeY), 0, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Slime>().TakeDamage(999);
        }
    }
}