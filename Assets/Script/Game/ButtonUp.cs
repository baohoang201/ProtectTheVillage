using UnityEngine;

public class ButtonUp : MonoBehaviour
{
    void OnMouseDown()
    {
        PlayerController.instance.PickUp();
        PlayerController.instance.ThrowItem();
    }
}
