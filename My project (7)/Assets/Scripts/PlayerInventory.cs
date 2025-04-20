using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class PlayerInventory : MonoBehaviour
{
    public int collectiblesToFinish = 6;
    public List<string> equipment = new List<string>();

    private int collectedCount = 0;
    private PlayerController player;

    private float totalSpeedBoost = 0f;
    private int totalHealthGained = 0;
    private float totalJumpBoost = 0f;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            CollectItem(other.gameObject);
        }
    }

    void CollectItem(GameObject item)
    {
        string itemName = item.name;
        equipment.Add(itemName);
        collectedCount++;

        Debug.Log($"Collected: {itemName} ({collectedCount}/{collectiblesToFinish})");

        CollectibleItem data = item.GetComponent<CollectibleItem>();
        if (data != null)
        {
            ApplyItemEffect(data);
        }

        Destroy(item);

        if (collectedCount >= collectiblesToFinish)
        {
            WinGame();
        }
    }

    void ApplyItemEffect(CollectibleItem item)
    {
        ItemType type = item.itemType;
        float value = item.effectValue;

        switch (type)
        {
            case ItemType.SpeedBoost:
                player.playerSpeed += value;
                totalSpeedBoost += value;
                Debug.Log($"Speed increased by {value}. Total boost: {totalSpeedBoost}. New speed: {player.playerSpeed}");
                break;

            case ItemType.HealthBoost:
                int healthGain = Mathf.RoundToInt(value);
                player.playerHealth += healthGain;
                totalHealthGained += healthGain;
                Debug.Log($"Health increased by {healthGain}. Total gained: {totalHealthGained}. New health: {player.playerHealth}");
                break;

            case ItemType.JumpBoost:
                player.jumpHeight += value;
                totalJumpBoost += value;
                Debug.Log($"Jump height increased by {value}. Total boost: {totalJumpBoost}. New jump height: {player.jumpHeight}");
                break;

            default:
                Debug.LogWarning($"Unknown item type: {type}. No effect applied.");
                break;
        }
    }

    void WinGame()
    {
        Debug.Log("Congratulations! You've collected everything and won the game!");
        Debug.Log($"--- Final Player Stats ---");
        Debug.Log($"Total Speed Boost: {totalSpeedBoost}");
        Debug.Log($"Total Health Gained: {totalHealthGained}");
        Debug.Log($"Total Jump Boost: {totalJumpBoost}");
    }
}