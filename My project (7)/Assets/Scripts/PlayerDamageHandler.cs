using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerDamageHandler : MonoBehaviour
{
    public int damageAmount = 10;

    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController component not found!");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject enemy = hit.gameObject;

        if (enemy.CompareTag("Enemy"))
        {
            DealDamage(damageAmount);
        }
    }

    void DealDamage(int amount)
    {
        player.playerHealth -= amount;
        Debug.Log($"{player.playerName} took {amount} damage! Remaining Health: {player.playerHealth}");

        if (player.playerHealth < 50)
        {
            Debug.LogWarning($"Warning: {player.playerName}'s health is below 50! Current Health: {player.playerHealth}");
        }

        if (player.playerHealth <= 0)
        {
            Debug.LogError($"{player.playerName} has died! Restarting the game...");
            RestartGame();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Die()
    {
        Debug.Log($"{player.playerName} has died!");
    }
}