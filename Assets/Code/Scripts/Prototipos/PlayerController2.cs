using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5f;
    public float hiddenAlpha = 0.3f; // Opacidad cuando está oculto
    public float visibleAlpha = 1f; // Opacidad cuando está visible
    public bool isHidden = false; // Indica si el jugador está oculto
    private bool canHide = false; // Indica si el jugador puede ocultarse
    private SpriteRenderer spriteRenderer;
    public float jumpForce = 5f; // Fuerza del salto
    private Rigidbody2D rb;
    private bool playerMode = true;
    public Bird3 Bird;
    public bool haveOfrenda = false;
    public GameObject ofrendaText;
    public GameObject ofrenda;
    public PlayableDirector ofrendaPowerTimeline;
    public GameObject winPanel;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (ofrendaPowerTimeline != null)
        {
            ofrendaPowerTimeline.Stop();
        }
    }

    void Update()
    {
        if (playerMode)
        {
            // Movimiento horizontal
            float move = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(move * speed * Time.deltaTime, 0f, 0f);
            transform.Translate(movement);

            // Salir del modo oculto al presionar S
            if (isHidden && Input.GetKeyDown(KeyCode.S) && !canHide && isHidden)
            {
                SetVisibility(true);
                isHidden = false;
            }

            // Saltar al presionar espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

            // Ocultar al jugador al presionar W
            if (Input.GetKeyDown(KeyCode.W) && !isHidden && canHide)
            {
                SetVisibility(false);
                isHidden = true;
            }
            
            if (haveOfrenda && Input.GetKeyDown(KeyCode.E))
            {
                ofrenda.SetActive(true);
                ofrendaText.SetActive(false);
                haveOfrenda = false;
                
                Invoke("DestroyOfrenda", 3f);
                if (ofrendaPowerTimeline != null)
                {
                    ofrendaPowerTimeline.Play();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerMode = !playerMode;
            if (playerMode)
            {
                Bird.isFree = false;
            }
            else
            {
                Bird.isFree = true;
            }
        }
    }

    // Maneja la interacción con el trigger collider para ocultarse
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sombra"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sombra"))
        {
            if (isHidden)
            {
                SetVisibility(true);
                isHidden = false;
            }
            
            canHide = false;
        }
    }

    // Cambia la opacidad del jugador
    private void SetVisibility(bool isVisible)
    {
        Color color = spriteRenderer.color;
        color.a = isVisible ? visibleAlpha : hiddenAlpha;
        spriteRenderer.color = color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void DestroyOfrenda()
    {
        Destroy(ofrenda);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Win"))
        {
            winPanel.SetActive(true);
            Invoke("ReloadGame", 3f);
        }
    }
    
    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}