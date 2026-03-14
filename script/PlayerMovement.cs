using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius = 0.1f;
    [SerializeField] private Transform groundCheck;    // Bas
    [SerializeField] private Transform groundCheckUp;  // Haut
    [SerializeField] private Transform groundCheckV;   // Bas V
    [SerializeField] private Transform groundCheckUpV; // Haut V

    [Header("Movement Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float rotationSpeed = 300f;

    private float baseSpeed = 6f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isVerticalMode = false;
    private bool isGravityUp = false;
    private Coroutine rotationCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = speed;
    }

    private void Update()
    {
        CheckGrounded();
        HandleAutoMovement();

        // 1. On calcule la valeur de gravité (en positif pour ne pas s'embrouiller)
        float speedFactor = Mathf.Abs(speed) / baseSpeed;
        float gravityIntensity = 34.62f * (speedFactor * speedFactor);

        // 2. On applique la direction
        if (!isVerticalMode)
        {
            // Si isGravityUp est vrai, force vers le haut (+), sinon vers le bas (-)
            float gravityY = isGravityUp ? gravityIntensity : -gravityIntensity;
            Physics2D.gravity = new Vector2(0, gravityY);
        }
        else
        {
            // Mode vertical (X)
            float gravityX = isGravityUp ? -gravityIntensity : gravityIntensity;
            Physics2D.gravity = new Vector2(gravityX, 0);
        }

        // 3. Saut
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && isGrounded)
        {
            Jump(speedFactor);
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        // On définit la couleur : Vert si touché, Rouge si vide
        Gizmos.color = isGrounded ? Color.green : Color.red;

        // Dessine les boîtes de détection pour le mode Normal
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(0.5f, 0.05f, 0));
        Gizmos.DrawWireCube(groundCheckUp.position, new Vector3(0.5f, 0.05f, 0));

        // Dessine les boîtes pour le mode Vertical (en Bleu pour différencier)
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckV.position, new Vector3(0.05f, 0.5f, 0));
        Gizmos.DrawWireCube(groundCheckUpV.position, new Vector3(0.05f, 0.5f, 0));
    }

    private void CheckGrounded()
    {
        // 1. Détection HORIZONTALE (Sol et Plafond)
        Vector2 posDown = groundCheck != null ? (Vector2)groundCheck.position : (Vector2)transform.position;
        bool touchingDown = Physics2D.OverlapBox(posDown, new Vector2(0.5f, 0.05f), 0, groundLayer);

        Vector2 posUp = groundCheckUp != null ? (Vector2)groundCheckUp.position : (Vector2)transform.position;
        bool touchingUp = Physics2D.OverlapBox(posUp, new Vector2(0.5f, 0.05f), 0, groundLayer);

        // 2. Détection VERTICALE (Murs Gauche et Droite)
        Vector2 posDownV = groundCheckV != null ? (Vector2)groundCheckV.position : (Vector2)transform.position;
        bool touchingDownV = Physics2D.OverlapBox(posDownV, new Vector2(0.05f, 0.5f), 0, groundLayer);

        Vector2 posUpV = groundCheckUpV != null ? (Vector2)groundCheckUpV.position : (Vector2)transform.position;
        bool touchingUpV = Physics2D.OverlapBox(posUpV, new Vector2(0.05f, 0.5f), 0, groundLayer);


        // 3. Logique de décision selon le mode
        if (!isVerticalMode)
        {
            // En mode normal, on ne peut sauter que si on touche le sol ou le plafond
            isGrounded = touchingDown || touchingUp;
        }
        else
        {
            // En mode vertical, on ne peut sauter que si on touche un mur
            isGrounded = touchingDownV || touchingUpV;
        }
        Debug.Log("Vertical " + isVerticalMode);
        Debug.Log("NB " + touchingDown);
        Debug.Log("NH " + touchingUp);
        Debug.Log("VB " + touchingDownV);
        Debug.Log("VH " + touchingUpV);
        Debug.Log("Ground " + isGrounded);
    }

    private void HandleAutoMovement()
    {
        if (isVerticalMode)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
        else
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
    }

    private void Jump(float multiplier)
    {
        // Déclenche la rotation
        if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
        rotationCoroutine = StartCoroutine(RotateCube());

        float adjustedJump = jumpForce * multiplier;

        if (isVerticalMode)
        {
            float jumpDirectionX = isGravityUp ? adjustedJump : -adjustedJump;
            rb.linearVelocity = new Vector2(jumpDirectionX, rb.linearVelocity.y);
        }
        else
        {
            float jumpDirectionY = isGravityUp ? -adjustedJump : adjustedJump;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpDirectionY);
        }
    }

    private void ApplyGroundGravity()
    {
        if (!isGrounded) return;

        if (isVerticalMode)
        {
            Physics2D.gravity = isGravityUp ? new Vector2(-9.81f, 0) : new Vector2(9.81f, 0);
        }
        else
        {
            Physics2D.gravity = isGravityUp ? new Vector2(0, 9.81f) : new Vector2(0, -9.81f);
        }

        StopRotation();
    }

    public void ResetPlayer()
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }

        isVerticalMode = false;
        isGravityUp = false;
        speed = baseSpeed;
        Physics2D.gravity = new Vector2(0, -34.62f);
        rb.linearVelocity = Vector2.zero;
        transform.eulerAngles = Vector3.zero;
    }

    private void StopRotation()
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;

            // Aligne le cube parfaitement au sol
            Vector3 finalRotation = transform.eulerAngles;
            finalRotation.z = Mathf.Round(finalRotation.z / 90) * 90;
            transform.eulerAngles = finalRotation;
        }
    }

    IEnumerator RotateCube()
    {
        float targetRotation = transform.eulerAngles.z - 90f;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetRotation)) > 0.5f)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, -step);
            yield return null;
        }

        Vector3 finalRotation = transform.eulerAngles;
        finalRotation.z = Mathf.Round(finalRotation.z / 90) * 90;
        transform.eulerAngles = finalRotation;
        rotationCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int direction = Math.Sign(speed);

        // Modes de jeu
        if (collision.CompareTag("mode_vertical"))
        {
            isVerticalMode = true;
            Physics2D.gravity = new Vector2(20f, 0);
        }
        else if (collision.CompareTag("mode_normal"))
        {
            isVerticalMode = false;
            Physics2D.gravity = new Vector2(0, -20f);
        }

        // Vitesse
        if (collision.CompareTag("speed_1")) speed = baseSpeed * direction;
        else if (collision.CompareTag("speed_2")) speed = (baseSpeed + 2) * direction;
        else if (collision.CompareTag("speed_3")) speed = (baseSpeed + 4) * direction;
        else if (collision.CompareTag("speed_4")) speed = (baseSpeed + 6) * direction;

        // Miroirs
        if (collision.CompareTag("mirror_blue")) speed = -Math.Abs(speed);
        if (collision.CompareTag("mirror_red")) speed = Math.Abs(speed);

        // Bumps (Saut forcé)
        if (collision.CompareTag("bump"))
        {
            float speedFactor = Mathf.Abs(speed) / baseSpeed;
            Jump(speedFactor); // Utilise la fonction Jump existante
            rb.linearVelocity *= 1.2f; // Donne un petit boost supplémentaire pour le bump
        }

        // Gravité
        if (collision.CompareTag("gravity_up"))
        {
            isGravityUp = true;
            //Physics2D.gravity = isVerticalMode ? new Vector2(-99.81f, 0) : new Vector2(0, 99.81f);
        }
        else if (collision.CompareTag("gravity_down"))
        {
            isGravityUp = false;
            //Physics2D.gravity = isVerticalMode ? new Vector2(99.81f, 0) : new Vector2(0, -99.81f);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}