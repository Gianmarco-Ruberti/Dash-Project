using UnityEngine;

public class playerTarget : MonoBehaviour
{
    void Update()
    {
        // Il suit la position du joueur, mais garde ses propres axes (0,0,0)
        transform.position = GameObject.FindWithTag("Player").transform.position;
    }
}
