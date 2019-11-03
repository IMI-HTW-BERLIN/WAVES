using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField]
        private float jumpForce;
        
        private float _speed;

        public void OnMove(InputValue inputValue)
        {
            float input = inputValue.Get<float>();
            
            if (input < 0)
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else if (input > 0)
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            
            _speed = input * baseMovementSpeed;
        }

        public void OnJump() => rb.AddForce(new Vector2(0, jumpForce));

        private void FixedUpdate() => rb.velocity = new Vector2(_speed, rb.velocity.y);
    }
}