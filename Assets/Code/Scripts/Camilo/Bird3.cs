using UnityEngine;

public class Bird3 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask whatIsRope; // Capa de la cuerda
    private bool nearRope = false;
    private GameObject rope;

    void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.down * moveInput * moveSpeed * Time.deltaTime);

        // Interacción con la cuerda
        if (nearRope && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Cortando la cuerda...");
            CutRope();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si el pájaro está tocando la cuerda
        if (((1 << collision.gameObject.layer) & whatIsRope) != 0)
        {
            nearRope = true;
            rope = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Se aleja de la cuerda
        if (((1 << collision.gameObject.layer) & whatIsRope) != 0)
        {
            nearRope = false;
            rope = null;
        }
    }

    void CutRope()
    {
        if (rope != null)
        {
            Destroy(rope); // Destruye la cuerda
        }
    }
}