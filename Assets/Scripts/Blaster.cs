using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public SpriteRenderer SpriteRenderer => spriteRenderer;

    public void Fire()
    {
        Vector3 bulletSpawnPos = firePosition.position;
        Vector2 direction = transform.right;
        GameObject bulletInstance = Instantiate(bullet, bulletSpawnPos, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        Destroy(bulletInstance, bulletLifetime);
    }
}