using UnityEngine;

public enum ItemType
{
    Normal,
    SpeedBoost,
    HealthBoost,
    JumpBoost
}

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;
    public float effectValue = 1f;
}