using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] private GameObject bullet;
        
        private float _speed;

        public void OnMove(InputValue input)
        {
            _speed = input.Get<float>() * baseMovementSpeed;
            Debug.Log(_speed);
        }

        public void OnFire()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (new Vector2(mousePosition.x, mousePosition.y) - rigidbody2D.position).normalized;
            GameObject bullet = Instantiate(this.bullet, transform);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * 1000);
            Destroy(bullet, 3f);
        }
        
        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(_speed, 0);
            rigidbody2D.MovePosition(rigidbody2D.position + direction * Time.fixedDeltaTime);
        }
    }
}