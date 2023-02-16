using UnityEngine;

public class ButtonDown : MonoBehaviour
{
    void OnMouseDown()
    {
        PlayerController.instance.TakeWater();
        PlayerController.instance.FireFight();
    }

    void OnMouseUp()
    {
        PlayerController.instance.canFireFight = false;
    }
}
