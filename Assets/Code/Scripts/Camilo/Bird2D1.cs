using UnityEngine;

public class Bird2D1 : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public bool isWhistling = false; // Si el pájaro está silbando
    public float whistleRadius = 5f; // Rango del silbido

    void Update()
    {
        // Control de movimiento usando las teclas A (izquierda) y D (derecha)
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        // Silbido al presionar la tecla "W"
        if (Input.GetKeyDown(KeyCode.W))
        {
            Whistle();
        }
    }

    void Whistle()
    {
        isWhistling = true;
        // Opcional: agregar sonido o animación del silbido aquí

        // Crear un círculo para representar el área en la que el silbido afecta al enemigo
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, whistleRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Llamar al enemigo para que reaccione al silbido
                hitCollider.GetComponent<Enemy2D1>().ReactToWhistle(transform.position);
            }
        }
    }

    // Para visualizar el área de silbido en la ventana de Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, whistleRadius);
    }
}