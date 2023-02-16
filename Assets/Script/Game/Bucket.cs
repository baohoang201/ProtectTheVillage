using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private Transform road;
    private Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private PolygonCollider2D col;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (PlayerController.instance.haveWater) spriteRenderer.sprite = sprites[1];
        else spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            transform.SetParent(road);
        }
    }

}