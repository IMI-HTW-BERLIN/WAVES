using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private float aimSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private float bulletLifetime;

    private float _aimDirection;
    private float _currentAngle;
    
    public void OnFire()
    {
        Vector3 bulletSpawnPos = firePosition.position;
        Vector2 direction = transform.right;
        GameObject bulletInstance = Instantiate(bullet, bulletSpawnPos, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        Destroy(bulletInstance, bulletLifetime);
    }

    public void OnWeaponAim(InputValue inputValue) => _aimDirection = inputValue.Get<float>();

    private void Update()
    {
        _currentAngle = Mathf.Clamp(_currentAngle + _aimDirection * aimSpeed, -15, 65);
        transform.localEulerAngles = new Vector3(0, 0, _currentAngle < 0 ? -(360 - _currentAngle) : _currentAngle);
    }

    private Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}