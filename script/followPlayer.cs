using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Glisse ton cube ici dans l'inspecteur
    public Vector3 offset = new Vector3(0, -0.5f, 0); // Pour décaler les particules sous le cube

    void LateUpdate() // LateUpdate est meilleur pour le suivi de caméra/particules
    {
        if (player != null)
        {
            // On suit la position, mais on garde notre propre rotation (0,0,0)
            transform.position = player.position + offset;
        }
    }
}