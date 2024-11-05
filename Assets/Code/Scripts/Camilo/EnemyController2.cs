using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    public float speed = 3f;
    public Transform pointA;
    public Transform pointB;

    public float visionRange = 3f; // Alcance de la visión del enemigo
    public LayerMask playerLayer; // Capa del jugador

    private bool movingToB = true;

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    // Patrullaje del enemigo entre dos puntos
    void Patrol()
    {
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                movingToB = true;
            }
        }
    }

    // Detección del jugador usando raycasts en 2D
    void DetectPlayer()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, visionRange, playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, visionRange, playerLayer);

        if (hitRight.collider != null && hitRight.collider.CompareTag("Player") && !hitRight.collider.GetComponent<PlayerController2>().isHidden)
        {
            ResetGame();
        }
        if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") && !hitLeft.collider.GetComponent<PlayerController2>().isHidden)
        {
            ResetGame();
        }
    }

    // Reiniciar el nivel o escena cuando el jugador es detectado
    void ResetGame()
    {
        // Reiniciar la escena o cualquier otra lógica que prefieras
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    // Dibujar las líneas de los raycasts en la escena para visualización
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * visionRange);
    }
}
