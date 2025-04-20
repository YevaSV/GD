using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float damageRange = 1.5f;
    public int damageAmount = 10;
    public float damageCooldown = 1.5f;

    private float lastDamageTime;

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= damageRange && Time.time >= lastDamageTime + damageCooldown)
        {
            DamagePlayer();
            lastDamageTime = Time.time;
        }
    }

    void DamagePlayer()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.playerHealth -= damageAmount;
            Debug.Log($"Enemy damaged player! Health: {pc.playerHealth}");

            if (pc.playerHealth <= 0)
            {
                Debug.Log($"{pc.playerName} has been defeated!");
            }
        }
    }
}