using UnityEngine;
using DG.Tweening;

public class Metor : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    void Update()
    {
        var direction = PlayerController.instance.road.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.road.position, 40 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            ShakeCam();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("house"))
        {
            ShakeCam();
            if (!other.gameObject.GetComponent<House>().isFire)
            {
                other.gameObject.GetComponent<House>().isFire = true;
                other.gameObject.GetComponent<House>().HouseFire();
                Fire(other.gameObject);

                if (GameManager.instance.count >= 3) GameManager.instance.GameOver();
                else GameManager.instance.count++;
                print(GameManager.instance.count);

            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Tree1"))
        {
            ShakeCam();
            if (!other.gameObject.GetComponent<Tree>().isTreeFire)
            {
                other.gameObject.GetComponent<Tree>().isTreeFire = true;
                other.gameObject.GetComponent<Tree>().TreeOneFire();
                Fire(other.gameObject);
            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Tree2"))
        {
            ShakeCam();
            if (!other.gameObject.GetComponent<Tree1>().isTreeFire)
            {
                other.gameObject.GetComponent<Tree1>().isTreeFire = true;
                other.gameObject.GetComponent<Tree1>().TreeTwoFire();
                Fire(other.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void Fire(GameObject other)
    {
        var fireIns = Instantiate(fire, other.gameObject.transform.position, other.gameObject.transform.rotation);
        fireIns.transform.SetParent(other.transform);
    }

    private void ShakeCam()
    {
        Camera.main.DOShakePosition(0.05f, 0.3f, 1).Play().OnComplete(() =>
                    {
                        Camera.main.transform.position = new Vector3(0, 0, -10);
                    });
    }

}
