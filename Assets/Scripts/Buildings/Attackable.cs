using System;
using Entities;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    public class Attackable : MonoBehaviour
    {
        private Collider2D _collider2D;
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _collider2D.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger triggered");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy == null) return;
            enemy.Attack();
        }
    }
}