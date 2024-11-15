using UnityEngine;

public class Projectile4 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destruir el proyectil si impacta una luz
        if (collision.CompareTag("Light"))
        {
            Destroy(collision.gameObject); // Destruir la luz
            Destroy(gameObject); // Destruir el proyectil
        }
    }
}