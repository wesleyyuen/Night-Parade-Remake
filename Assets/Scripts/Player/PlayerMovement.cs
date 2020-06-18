﻿using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour {

    [Header ("References")]
    public Animator animator;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public ParticleSystem dustTrail;
    public Grounded grounded;
    public AudioSource audioSource;

    [Header ("Movement Settings")]
    public float movementSpeed;
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpBufferTime = 0.2f;
    public float coyoteTime = 0.05f;

    [Header ("SFX")]
    [SerializeField]
    private AudioClip[] footstepsClips;

    float jumpBuffer = 0;
    float coyoteTimer = 0;

    void Update () {
        animator.SetBool ("IsGrounded", grounded.isGrounded);
        // Jump uses rb.velocity, should be in Update() instead of FixedUpdate()
        Jump ();
    }

    void FixedUpdate () {
        float horizontalInput = Input.GetAxisRaw ("Horizontal");
        Vector3 prevLocalScale = transform.localScale;

        if (horizontalInput != 0) {
            if (horizontalInput > 0) {
                if (prevLocalScale.x != 1f) {
                    CreateDustTrail ();
                    animator.SetBool ("FacingRight", true);
                    transform.localScale = new Vector3 (1f, 1f, 1f);
                }
            } else if (horizontalInput < 0) {
                if (prevLocalScale.x != -1f) {
                    CreateDustTrail ();
                    animator.SetBool ("FacingRight", false);
                    transform.localScale = new Vector3 (-1f, 1f, 1f);
                }
            }
        }

        animator.SetFloat ("Horizontal", horizontalInput);
        animator.SetFloat ("Vertical", rb.velocity.y);

        Vector3 horizontalMovement = new Vector2 (horizontalInput * movementSpeed, 0.0f);
        transform.position += horizontalMovement * Time.deltaTime;

    }

    void Jump () {
        // Coyote Time
        coyoteTimer -= Time.deltaTime;
        if (FindObjectOfType<Grounded> ().isGrounded) {
            coyoteTimer = coyoteTime;
        }

        // Jump Buffering
        jumpBuffer -= Time.deltaTime;
        if (Input.GetButtonDown ("Jump")) {
            jumpBuffer = jumpBufferTime;
        }

        if (jumpBuffer > 0 && coyoteTimer > 0) {
            CreateDustTrail ();
            jumpBuffer = 0;
            coyoteTimer = 0;
            rb.velocity += Vector2.up * jumpVelocity;
        }
        // Low Jump
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton ("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CreateDustTrail () {
        dustTrail.Play ();
    }

    void FootstepSFX () {
        audioSource.PlayOneShot(footstepsClips[Random.Range(0, footstepsClips.Length)]);
    }
}