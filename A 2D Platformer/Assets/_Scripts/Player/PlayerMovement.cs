using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    public Animator animator;
    private bool facingRight = true;

    [Header("Ground Check")]
    [Space]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;
    [Space]
    public GameObject ControlScreen;
    public bool hasPressedEscape;

    void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check for Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Move Player Horizontally
        moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Flip Player
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (hasPressedEscape == false)
            {
                this.GetComponent<PlayerControls>().enabled = false;
                this.GetComponent<Weapon>().enabled = false;
                Time.timeScale = 0;
                hasPressedEscape = true;
                ControlScreen.SetActive(true);
            }
            else
            {
                HideControls();
            }
        }

        if (isGrounded == true)
        {
            animator.SetBool("IsJumping", false);
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetBool("IsJumping", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == true)
        {
            animator.SetTrigger("isSliding");
        }
    }

    public void HideControls()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;
        hasPressedEscape = false;
        ControlScreen.SetActive(false);
        this.GetComponent<PlayerControls>().enabled = true;
        this.GetComponent<Weapon>().enabled = true;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        hasPressedEscape = false;
        SceneManager.LoadScene(0);
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public void CatchMouseClicks(GameObject setSelection)
    {
        EventSystem.current.SetSelectedGameObject(setSelection);
    }
}
