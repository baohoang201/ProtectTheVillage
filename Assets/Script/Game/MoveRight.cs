using UnityEngine;

public class MoveRight : MonoBehaviour
{
    private void OnMouseDrag()
    {
        PlayerController.instance.MoveRight();
    }

    void OnMouseUp()
    {
        PlayerController.instance.PlayAniPlayer(false);
    }
}
