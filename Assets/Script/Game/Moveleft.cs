using UnityEngine;

public class Moveleft : MonoBehaviour
{
    void OnMouseDrag()
    {
        PlayerController.instance.MoveLeft();
    }
    void OnMouseUp()
    {
        PlayerController.instance.PlayAniPlayer(false);
    }
}
