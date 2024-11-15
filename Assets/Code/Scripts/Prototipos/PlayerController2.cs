using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5f;
    public float hiddenAlpha = 0.3f; // Opacidad cuando está oculto
    public float visibleAlpha = 1f; // Opacidad cuando está visible
    public bool isHidden = false; // Indica si el jugador está oculto

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move * speed * Time.deltaTime, 0f, 0f);
        transform.Translate(movement);

        // Salir del modo oculto al presionar S
        if (isHidden && Input.GetKeyDown(KeyCode.S))
        {
            SetVisibility(true);
            isHidden = false;
        }
    }

    // Maneja la interacción con el trigger collider para ocultarse
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sombra"))
        {
            if (Input.GetKeyDown(KeyCode.W) && !isHidden)
            {
                SetVisibility(false); // Ocultar al jugador
                isHidden = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sombra"))
        {
            if (isHidden)
            {
                SetVisibility(true); // Mostrar al jugador
                isHidden = false;
            }
        }
    }

    // Cambia la opacidad del jugador
    private void SetVisibility(bool isVisible)
    {
        Color color = spriteRenderer.color;
        color.a = isVisible ? visibleAlpha : hiddenAlpha;
        spriteRenderer.color = color;
    }
}