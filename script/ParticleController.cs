using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementparticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if (Mathf.Abs(playerRb.linearVelocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementparticle.Play();
                counter = 0;
            }

        }
    }
}
