using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public void RestartGame()
    {
        // Recharge la scène active
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
