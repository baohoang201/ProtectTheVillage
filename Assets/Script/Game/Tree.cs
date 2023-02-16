using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool isTreeFire;
    [SerializeField] private Sprite[] sprites;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isTreeFire = false;
    }
    public void TreeOneFire()
    {
        if (isTreeFire) spriteRenderer.sprite = sprites[1];
        else spriteRenderer.sprite = sprites[0];
    }
}
