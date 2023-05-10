using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;
// using TMPro;

public class PlayerController : MonoBehaviour
{
    // [Header("Reference")]
    // [SerializeField] Slider hpUI;

    [Header("Controller")]
    [SerializeField] KeyCode jumpKey = KeyCode.W;
    [SerializeField] KeyCode altJumpKey = KeyCode.Space;

    [Header("Stats")]
    public float speed;
    public float jumpHeight;
    public int extraJumpsValue;
    private float moveInput;
    public bool isInvincible = false;

    private Rigidbody2D rb;
    private Animator animator;

    [Header("Ground Check")]
    private bool isGrounded, isAir;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius = .2f;
    [SerializeField] LayerMask groundLayer;

    [Header("References")]
    [SerializeField] Transform sprite;

    [Header("Effects")]
    [SerializeField] ParticleSystem jumpFx;
    [SerializeField] GameObject deathFx;

    [Header("Audios")]
    [SerializeField] Sound jumpSfx;
    [SerializeField] Sound deathSfx;

    [Header("Camera Shake (Death)")]
    [SerializeField] float magnitude;
    [SerializeField] float roughness;
    [SerializeField] float fadeIn;
    [SerializeField] float fadeOut;

    private int extraJumps;
    
    private bool facingRight = true;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        // Ground check
        GroundHandler();

        // Handle Jumping
        JumpHandler();

        // Flip player
        FlipHandler();
    }

    void GroundHandler() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (!isGrounded) {
            if (!isAir) isAir = !isAir;
        } else {
            if (isAir) {
                isAir = false;
            }        
        }
    }

    void JumpHandler() {
        // Reset extra jumps
        if (isGrounded) extraJumps = extraJumpsValue;

        // Jumping
        if (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(altJumpKey)) {
            if (isGrounded) {
                rb.velocity = Vector2.up * jumpHeight;
                jumpFx.Play();
                AudioManager.Instance.PlaySFX(jumpSfx);
            }
            else if (extraJumps > 0) {
                rb.velocity = Vector2.up * jumpHeight;
                jumpFx.Play();
                extraJumps--;  
                AudioManager.Instance.PlaySFX(jumpSfx);
            }
        }
    }

    void FlipHandler() {
        if (facingRight == false && moveInput > 0) Flip();
        else if (facingRight == true && moveInput < 0) Flip();
    }

    void FixedUpdate() {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = sprite.transform.localScale;
        scaler.x *= -1;
        sprite.transform.localScale = scaler;
    }

    void OnCollisionEnter2D(Collision2D col) {
        //  DEAD
        if (col.gameObject.CompareTag("Enemy")) {
            if (isInvincible) return;
            Dead();
        }
    }

    public void Dead() {
        Instantiate(deathFx,transform.position,Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(magnitude,roughness,fadeIn,fadeOut);
        AudioManager.Instance.PlaySFX(deathSfx);
        FindObjectOfType<HitStop>().Stop(0.1f,1f);
        Destroy(gameObject);
    }
}
