using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController4 : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Botón izquierdo del mouse o Ctrl izquierdo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegurarse de que la posición esté en el plano 2D

        // Calcular la dirección entre el jugador y el mouse
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Calcular la rotación en ángulo hacia el mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instanciar el proyectil con la rotación hacia el mouse
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

        // Darle velocidad en la dirección calculada
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
    }
}