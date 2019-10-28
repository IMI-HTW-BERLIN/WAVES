using Entities;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy;
        if ((enemy = other.GetComponent<Enemy>()) != null)
        {
            enemy.ReduceHealth(damage);
            Destroy(gameObject);
        }
    }
}
