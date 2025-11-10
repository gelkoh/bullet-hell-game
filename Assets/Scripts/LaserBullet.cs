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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(10);
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    } 
}
