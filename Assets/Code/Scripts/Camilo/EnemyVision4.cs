using UnityEngine;

public class EnemyVision4 : MonoBehaviour
{
    public float visionRange = 5f;
    public int rayCount = 5;
    public float angleSpread = 30f;
    private bool canSee = true;

    void Update()
    {
        if (canSee)
        {
            CastVision();
        }
    }

    void CastVision()
    {
        for (int i = 0; i < rayCount; i++)
        {
            float angle = -angleSpread / 2 + (angleSpread / (rayCount - 1)) * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionRange);

            // Dibujar los rayos en la escena
            Debug.DrawRay(transform.position, direction * visionRange, Color.red);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Debug.Log("Jugador en el campo de visión del enemigo");
                // Aquí podrías agregar una reacción del enemigo al ver al jugador
            }
        }
    }

    public void DisableVision()
    {
        canSee = false;
        Debug.Log("El enemigo ha perdido su campo de visión");
    }
}