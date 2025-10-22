using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private int m_damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player.Instance.TakeDamage(m_damage);
        }
    }
}
