using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Vector3 respawnPoint;
    private PlayerMovement movement; // Référence au script de mouvement

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void RespawnNow()
    {
        transform.position = respawnPoint;

        // On appelle la réinitialisation des paramètres
        if (movement != null)
        {
            movement.ResetPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            RespawnNow();
        }
    }
}
