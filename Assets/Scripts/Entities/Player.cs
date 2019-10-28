using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private int bulletSpeed;
        [SerializeField] private float bulletLifetime;
        
        private float _speed;

        public void OnMove(InputValue input) => _speed = input.Get<float>() * baseMovementSpeed;

        public void OnFire()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (new Vector2(mousePosition.x, mousePosition.y) - rb.position).normalized;
            GameObject bullet = Instantiate(this.bullet, transform);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
            Destroy(bullet, bulletLifetime);
        }
        
        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(_speed, 0);
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
        }
    }
}