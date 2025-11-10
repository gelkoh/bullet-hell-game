using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserBullet : MonoBehaviour, IProjectile
{
    private Rigidbody2D m_rigidBody2d;
    private float m_timeToLive = 3f;

    void Awake()
    {
        m_rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, m_timeToLive);
    }

    public void Launch(Vector2 direction, float force)
    {
        m_rigidBody2d.AddForce(direction * force);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + ": LaserBullet collided with " + other.gameObject.name, other.gameObject);

        Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(10);
            Destroy(gameObject);
            return;
        }

        TilemapCollider2D tilemapCollider = other.gameObject.GetComponent<TilemapCollider2D>();

        if (tilemapCollider != null)
        {
            Debug.Log(gameObject.name + ": LaserBullet collided with " + other.gameObject.name, other.gameObject);
            Destroy(gameObject);
            return;
        }

        // Destroy at any collision anyways
        Destroy(gameObject);
    } 
}
