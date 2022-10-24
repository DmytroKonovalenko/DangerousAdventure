using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptsPlayer
{
    public class Princess : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Knight knight = collider.gameObject.GetComponent<Knight>();
            if (knight != null) 
            {
                GameController.Instance.PrincessFound();
            }
        }
    }
}
