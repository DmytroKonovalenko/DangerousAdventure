using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptsPlayer
{
    public class Creature : MonoBehaviour, IDestrucable
    {
        [SerializeField]
        protected Animator animator;

        [SerializeField]
        protected Rigidbody2D _rigidbody;

        [SerializeField]
        private float speed;

        [SerializeField]
        private float damage;

        [SerializeField]
        protected float health = 100;
        public float Health { get { return health; } set { health = value; } }

        public float Speed { get => speed; set => speed = value; }
        public float Damage { get => damage; set => damage = value; }

        void Awake()
        {
            animator = gameObject.GetComponentInChildren<Animator>();
        }

        public void Die()
        {
            GameController.Instance.Killed(this);
        }

        public void     RecieveHit(float damage)
        {
            Health -= damage;
            GameController.Instance.Hit(this);
            if (Health <= 0)
            {
                Die();
            }
        }

        protected void DoHit(Vector3 hitPosition, float hitRadius, float hitDamage)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitRadius);

            for (int i = 0; i < hits.Length; i++)
            {
                if (!GameObject.Equals(hits[i].gameObject, gameObject))
                {
                    IDestrucable destrucable = hits[i].gameObject.GetComponent<IDestrucable>();

                    if (destrucable != null)
                    {
                        destrucable.RecieveHit(hitDamage);
                    }
                }
            }
            
        }

    }
}