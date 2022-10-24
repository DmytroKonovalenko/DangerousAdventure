
using UnityEngine;
using UnityEngine.UI;

public class ScrollingImage : MonoBehaviour
{
    public float scrollerSpeed;

    public Image image;


    private void LateUpdate()
    {
        float vOffset = Time.time * scrollerSpeed;
        image.material.SetTextureOffset("_MainTex", new Vector2(vOffset, 0));
    }
}