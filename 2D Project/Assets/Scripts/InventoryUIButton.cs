using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButton : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Text label;

    [SerializeField]
    private Text count;

    [SerializeField]
    private List<Sprite> sprites;

    private InventoryItem itemData;

    private InventoryUsedCallback callback;

 
    public InventoryUsedCallback Callback { get => callback; set => callback = value; }
    public InventoryItem ItemData { get => itemData; set => itemData = value; }

    public void Start()
    {
     
        count.text = itemData.Quantity.ToString();
        string spriteNameToSearch = itemData.CrystallType.ToString().ToLower();
        image.sprite = sprites.Find(x => x.name.Contains(spriteNameToSearch));
        label.text = spriteNameToSearch;
        gameObject.GetComponent<Button>().onClick.AddListener(() => callback(this));
    }
}
