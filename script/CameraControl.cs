using UnityEngine;
using Unity.Cinemachine;

public class CameraMirrorHandler : MonoBehaviour
{
    private CinemachineCamera vcam;
    private CinemachinePositionComposer composer;
    private PlayerMovement player;

    [SerializeField] private float horizontalOffset = 3f;
    [SerializeField] private float flipSpeed = 5f;

    void Start()
    {
        // On cherche l'objet qui a le tag "Player" pour récupérer son script de mouvement
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerMovement>();
        }

        // On cherche la caméra
        vcam = GameObject.FindAnyObjectByType<CinemachineCamera>();

        if (vcam != null)
        {
            composer = vcam.GetComponent<CinemachinePositionComposer>();
        }

        // DEBUG : Pour être sûr de ce qui manque
        if (playerObj == null) Debug.LogError("ERREUR : Aucun objet avec le tag 'Player' trouvé dans la scène !");
        if (player == null && playerObj != null) Debug.LogError("ERREUR : Le script PlayerMovement est manquant sur l'objet Player !");
        if (composer == null) Debug.LogError("ERREUR : CinemachinePositionComposer non trouvé sur la caméra !");
    }

    void Update()
    {
        // SI L'UN DES DEUX EST NUL, ON ARRÊTE TOUT (Évite la NullReference)
        if (player == null || composer == null) return;

        // On vérifie la vitesse via ton script de mouvement
        float targetX = player.GetSpeed() > 0 ? horizontalOffset : -horizontalOffset;

        // Application fluide de l'offset
        Vector3 currentOffset = composer.TargetOffset;
        currentOffset.x = Mathf.Lerp(currentOffset.x, targetX, Time.deltaTime * flipSpeed);
        composer.TargetOffset = currentOffset;
    }
}