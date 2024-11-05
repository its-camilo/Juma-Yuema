using UnityEngine;

public class Block3 : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject rope; // Referencia a la cuerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // El bloque no se moverá hasta que la cuerda se corte
    }

    void Update()
    {
        // Si la cuerda ha sido destruida
        if (rope == null)
        {
            rb.isKinematic = false; // Activa la gravedad
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el bloque choca con el enemigo, lo destruye
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("¡Enemigo eliminado!");
        }
    }
}