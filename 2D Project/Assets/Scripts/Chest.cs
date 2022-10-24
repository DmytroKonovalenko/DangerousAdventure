using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystallType {Random, Red, Green, Blue}
namespace ScriptsPlayer
{
    public class Chest : MonoBehaviour
    {

        public InventoryItem itemData = new InventoryItem();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Knight knight = collider.gameObject.GetComponent<Knight>();
            if (itemData.CrystallType == CrystallType.Random)
            {
                itemData.CrystallType = (CrystallType)Random.Range(1, 4);
            }
            if (itemData.Quantity == 0)
            {
                itemData.Quantity = Random.Range(1, 6);
            }
            
            if (knight != null)
            {
                
                GameController.Instance.AddNewInventoryItem(itemData);
                Destroy(gameObject);
            }
        }

    }
}