using UnityEngine;

public class LightSource4 : MonoBehaviour
{
    public EnemyVision4 enemyVision;  // Referencia al script del enemigo

    private void OnDestroy()
    {
        if (enemyVision != null)
        {
            enemyVision.DisableVision(); // Desactivar la visión del enemigo
        }
    }
}