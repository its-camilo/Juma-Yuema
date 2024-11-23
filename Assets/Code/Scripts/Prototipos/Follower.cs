using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float followSpeed = 2f; // Velocidad de seguimiento
    public Vector3 offset = new Vector3(-5f, 5f, 0f); // Desplazamiento inicial respecto al jugador

    void Start()
    {
        // Establecer la posición inicial del seguidor
        transform.position = player.position + offset;
    }

    void Update()
    {
        // Calcular la posición objetivo con el desplazamiento
        Vector3 targetPosition = player.position + offset;

        // Mover el seguidor hacia la posición objetivo con un efecto de retraso
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}