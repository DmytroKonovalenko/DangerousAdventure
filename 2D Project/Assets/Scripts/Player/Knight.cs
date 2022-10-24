using UnityEngine;

namespace ScriptsPlayer
{
    public class Knight : Creature
    {     

        [SerializeField]
        private Transform groundCheck;

       
        [SerializeField]
        private float jumpForce;

        [SerializeField]
        private Transform attackPoint;

        [SerializeField]
        private float attackRange;

        [SerializeField]
        private float hitDelay;

       

        private bool onGround = true;

        public float stairSpeed = 25.0f;

        [SerializeField]
        private bool onStair;

        public bool OnStair
        {
            get { return onStair; }
            set
            {
                if (value == true)
                { _rigidbody.gravityScale = 0; }
                else
                {
                    _rigidbody.gravityScale = 1;
                }
                onStair = value;
            }
        }

        private void HandleOnUpdateHeroParameters(HeroParameters parameters)
        {
            Health = parameters.MaxHealth;
            Damage = parameters.Damage;
            Speed = parameters.Speed;
        }
        private void Awake()
        {
            GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
            GameController.Instance.Knight = this;
            animator = gameObject.GetComponentInChildren<Animator>();
          
        }
        // Start is called before the first frame update
        void Start()
        {
          
            //health = GameController.Instance.MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            onGround = CheckGround();
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
            animator.SetBool("Jump", !onGround);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = Input.GetAxis("Horizontal") * Speed;
            _rigidbody.velocity = velocity;
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Attack");
                //Attack();

                Invoke("Attack", hitDelay);
            }
            if (Input.GetButtonDown("Jump") && onGround) 
            {
                _rigidbody.AddForce(Vector2.up * jumpForce);
               
            }
            if (transform.localScale.x < 0)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    transform.localScale = Vector3.one;
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            if (OnStair)
            {
                velocity = _rigidbody.velocity;
                velocity.y = Input.GetAxis("Vertical") * stairSpeed;
                _rigidbody.velocity = velocity;
            }

        }
        private bool CheckGround()
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, groundCheck.position);

            for (int i = 0; i < hits.Length; i++)
            {
                if (!GameObject.Equals(hits[i].collider.gameObject, gameObject))
                {
                    return true;
                }
            }
            return false;
        }

        private void Attack()
        {
            DoHit(attackPoint.position, attackRange, Damage);
           // Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
            //for (int i = 0; i < hits.Length; i++) 
           // { 
              //  if(!GameObject.Equals(hits[i].gameObject, gameObject)) 
             //   {
            //        IDestrucable destrucable = hits[i].gameObject.GetComponent<IDestrucable>();
              //      if (destrucable != null) 
              //      {
             //           Debug.Log("Hit" + destrucable.ToString());
             //           destrucable.RecieveHit(Damage);
             //           break;
              //      }
          //      }
           // }
        }
        private void OnDestroy()
        {
            GameController.Instance.OnUpdateHeroParameters -= HandleOnUpdateHeroParameters;
        }
       // public override void Die()
        //{
          //  GameController.Instance.GameOver();
        //}
    }
}
