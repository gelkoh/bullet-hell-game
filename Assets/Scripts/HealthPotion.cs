using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Heal(50);
            Destroy(gameObject);
            return;
        }
    }
}
