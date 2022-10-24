using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptsPlayer
{
    public class Dragon : Creature
    {

        [SerializeField]
        private CircleCollider2D hitCollider;
        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            Vector2 velocity = _rigidbody.velocity;
            velocity.x = Speed * transform.localScale.x* -1;
            _rigidbody.velocity = velocity;
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Knight>() != null)
            {
                for (int i = 0; i < collision.contacts.Length; i++)
                {
                    Vector2 fromDragonToContactVector = collision.contacts[i].point - (Vector2)transform.position;
                    if (Vector2.Angle(fromDragonToContactVector, Vector2.up) < 45) 
                    {
                        Die();
                    }
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            Knight knight = collider.gameObject.GetComponent<Knight>();
            if (knight != null)
            {
                animator.SetTrigger("Attack");
            }
            else //if (collider.gameObject != gameObject) 
            {
                ChangeDirection();
            }
        }

        private void ChangeDirection()
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = Vector3.one;
            }
            else 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void Attack()
        {

            Vector3 hitPosition = transform.TransformPoint(hitCollider.offset);
            DoHit(hitPosition, hitCollider.radius, Damage);
            //Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitCollider.radius);
           // for (int i = 0; i < hits.Length; i++)
            //{
             //   if (!GameObject.Equals(hits[i].gameObject, gameObject))
             //   {
            
               //     IDestrucable destrucable = hits[i].gameObject.GetComponent<IDestrucable>();
             //       if (destrucable != null)
              //      {
                      //  destrucable.RecieveHit(Damage);
             //       }
            //    }
           // }
        }

    }
}
