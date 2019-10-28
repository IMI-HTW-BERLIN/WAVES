using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private float aimSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private float bulletLifetime;

    private float aimDirection;
    private float currentAngle;
    
    public void OnFire()
    {
        Vector3 bulletSpawnPos = firePosition.position;
        Vector2 direction = transform.right;
        GameObject bulletInstance = Instantiate(bullet, bulletSpawnPos, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        Destroy(bulletInstance, bulletLifetime);
    }

    public void OnWeaponAim(InputValue inputValue) => aimDirection = inputValue.Get<float>();

    private void Update()
    {
        currentAngle = Mathf.Clamp(currentAngle + aimDirection * aimSpeed, -15, 65);
        transform.localEulerAngles = new Vector3(0, 0, currentAngle < 0 ? -(360 - currentAngle) : currentAngle);
    }

    private Vector2 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return new Vector2(mousePosition.x, mousePosition.y);
    }
}