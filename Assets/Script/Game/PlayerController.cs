using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform holdPos;
    public bool isEmty;
    private float speed;
    private Vector3 offset;
    private Animator animator;
    private RaycastHit2D hit2D, hitUp;
    public Transform road;
    private int rabbit, bucket, wells;
    public bool isBucket, haveWater, canFireFight;
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
        isEmty = true;
        animator = GetComponent<Animator>();
        speed = 100;
        offset = new Vector3(0, 0.5f);
        rabbit = LayerMask.NameToLayer("Rabbit");
        bucket = LayerMask.NameToLayer("Bucket");
        wells = LayerMask.NameToLayer("Wells");
        canFireFight = false;
    }

    void Update()
    {
        hit2D = Physics2D.Raycast(transform.position - offset, Vector2.down, 1f);
        if (hit2D.collider == null) return;

        Debug.DrawRay(transform.position - offset, Vector2.down, Color.blue, 1f);
    }

    public void PickUp()
    {
        Debug.Log(hit2D.collider.gameObject.name);
        if (hit2D.transform.gameObject.layer == rabbit && isEmty)
        {
            hit2D.collider.gameObject.transform.position = holdPos.position;
            hit2D.collider.gameObject.transform.SetParent(transform);
            hit2D.collider.gameObject.GetComponent<Rabbit>().isPickUp = true;
            isEmty = false;
        }

        if (hit2D.transform.gameObject.layer == bucket && isEmty)
        {
            isBucket = true;
            hit2D.collider.gameObject.transform.position = holdPos.position;
            hit2D.collider.gameObject.transform.SetParent(transform);
            isEmty = false;
        }
    }

    public void ThrowItem()
    {
        if (!isEmty)
        {
            hitUp = Physics2D.Raycast(transform.position + offset, Vector2.up, 1f);
            if (hitUp.collider == null) return;
            if (hitUp.transform.gameObject.layer == rabbit)
            {
                hitUp.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                hitUp.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                hitUp.collider.gameObject.GetComponent<Rabbit>().isScore = true;
                isEmty = true;
            }

            if (hitUp.transform.gameObject.layer == bucket)
            {
                hitUp.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                hitUp.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                isBucket = false;
                isEmty = true;
            }


            Debug.DrawRay(transform.position + offset, Vector2.up, Color.blue, 1f);
        }
    }


    public void MoveRight()
    {
        road.RotateAround(road.position, Vector3.forward, speed * Time.deltaTime);
        transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        PlayAniPlayer(true);

    }

    public void MoveLeft()
    {
        road.RotateAround(road.position, Vector3.forward, -speed * Time.deltaTime);
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        PlayAniPlayer(true);
    }

    public void TakeWater()
    {
        hitUp = Physics2D.Raycast(transform.position + offset, Vector2.up, 5f);
        if (isBucket && hit2D.transform.gameObject.layer == wells) haveWater = true;
    }
    public void FireFight() => canFireFight = true;


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("house"))
        {
            if (other.gameObject.GetComponent<House>().isFire)
            {
                if (canFireFight && haveWater)
                {
                    other.gameObject.GetComponent<House>().isFire = false;
                    haveWater = false;
                    Destroy(other.gameObject.transform.GetChild(0).gameObject);
                    other.gameObject.GetComponent<House>().HouseFire();
                    GameManager.instance.count--;
                    print(GameManager.instance.count);
                }
            }
        }

        if (other.gameObject.CompareTag("Tree1"))
        {
            if (other.gameObject.GetComponent<Tree>().isTreeFire)
            {
                if (canFireFight && haveWater)
                {
                    other.gameObject.GetComponent<Tree>().isTreeFire = false;
                    haveWater = false;
                    Destroy(other.gameObject.transform.GetChild(0).gameObject);
                    other.gameObject.GetComponent<Tree>().TreeOneFire();
                }
            }
        }


        if (other.gameObject.CompareTag("Tree2"))
        {
            if (other.gameObject.GetComponent<Tree1>().isTreeFire)
            {
                if (canFireFight && haveWater)
                {
                    other.gameObject.GetComponent<Tree1>().isTreeFire = false;
                    haveWater = false;
                    Destroy(other.gameObject.transform.GetChild(0).gameObject);
                    other.gameObject.GetComponent<Tree1>().TreeTwoFire();

                }
            }

        }
    }



    public void PlayAniPlayer(bool isMove) => animator.SetBool("statusPlayer", isMove);



}
