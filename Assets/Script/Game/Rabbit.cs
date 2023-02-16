using UnityEngine;
using DG.Tweening;

public class Rabbit : MonoBehaviour
{
    private Transform rabbit;
    private float moveRate, moveNext;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private PolygonCollider2D col;
    private int randomNumber, speed;
    public bool isPickUp, isScore;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rabbit = GameObject.Find("Rabbit").GetComponent<Transform>();
        isPickUp = false;
        isScore = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        moveRate = 2f;
        moveNext = Time.time;
        randomNumber = 0;
        speed = 5;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPickUp)
        {
            RandomFunction();
            CheckForward();
        }
    }


    private void RandomFunction()
    {
        if (Time.time > moveNext)
        {
            randomNumber = Random.Range(0, 2);
            moveNext = Time.time + moveRate;
        }
        if (randomNumber == 0) transform.RotateAround(PlayerController.instance.road.position, Vector3.forward, speed * Time.deltaTime);
        else transform.RotateAround(PlayerController.instance.road.position, Vector3.forward, -speed * Time.deltaTime);
    }

    private void CheckForward()
    {
        if (randomNumber == 0) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            transform.SetParent(rabbit);
            isPickUp = false;

        }

        if (other.gameObject.CompareTag("house") && isScore)
        {
            if (!other.gameObject.GetComponent<House>().isFire)
            {
                sprite.DOFade(0f, 0.5f).Play().OnComplete(() =>
           {
               Destroy(gameObject);
               ScoreManager.instance.score += 10;
               ScoreManager.instance.UpdateScore();
               GameManager.instance.InstiateRabbit();
           });

            }

        }


    }

}
