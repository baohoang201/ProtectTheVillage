using UnityEngine;

public class House : MonoBehaviour
{
    public bool isFire;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
  
    public void HouseFire()
    {
        if (isFire) spriteRenderer.sprite = sprites[1];
        else spriteRenderer.sprite = sprites[0];
    }
}
