using UnityEngine;

public class Enemy2D1 : MonoBehaviour
{
    public float detectionDistance = 5f;  // Distancia de detección del raycast
    private bool isFacingLeft = true;  // Indica si el enemigo está mirando a la izquierda

    void Start()
    {
        // Asegurarse de que el enemigo mire inicialmente a la izquierda
        transform.localScale = new Vector3(-1, 1, 1);  // Invierte la escala en el eje X para mirar a la izquierda
    }

    void Update()
    {
        // Realiza un raycast en 2D en la dirección que el enemigo está mirando
        Vector2 direction = isFacingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);

        // Visualizar el raycast en la escena
        Debug.DrawRay(transform.position, direction * detectionDistance, hit.collider ? Color.red : Color.green);
    }

    // Este método es llamado cuando el pájaro silba
    public void ReactToWhistle(Vector2 birdPosition)
    {
        // Si el pájaro está a la derecha del enemigo, voltear
        if (birdPosition.x > transform.position.x && isFacingLeft)
        {
            Flip();
        }
    }

    // Detectar el silbido cuando el pájaro entra en el área de detección
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bird"))  // Asegúrate de que el pájaro tenga el tag "Bird"
        {
            ReactToWhistle(other.transform.position);
        }
    }

    void Flip()
    {
        // Cambiar la dirección del enemigo
        isFacingLeft = !isFacingLeft;
        transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
    }
}