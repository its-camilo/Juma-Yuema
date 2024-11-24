using UnityEngine;

public class Ofrenda : MonoBehaviour
{
    public GameObject ofrendaText;
    public PlayerController2 Player;

    void Start()
    {
        ofrendaText.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ofrendaText.SetActive(true);
            Player.haveOfrenda = true;
            Destroy(this.gameObject);
        }
    }
}
