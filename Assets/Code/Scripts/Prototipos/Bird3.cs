using UnityEngine;

public class Bird3 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask whatIsRope; // Capa de la cuerda
    public bool nearRope = false;
    private GameObject rope;
    public bool isFree = false;
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
        if (isFree)
        {
            // Movimiento horizontal
            float moveInput = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);

            // Interacción con la cuerda
            if (nearRope && Input.GetKeyDown(KeyCode.E))
            {
                CutRope();
            }
        }

        if (!isFree)
        {
            // Calcular la posición objetivo con el desplazamiento
            Vector3 targetPosition = player.position + offset;

            // Mover el seguidor hacia la posición objetivo con un efecto de retraso
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
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