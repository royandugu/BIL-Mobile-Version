using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField]
    private float limit;
    private float xPressValue,yPressValue;
    private PlayerController playerController;
    private void OnCollisionEnter2D(Collision2D other)
    {
        playerController=other.gameObject.GetComponent<PlayerController>();
        playerController.hasCollided=true;
        
        xPressValue = transform.position.x;
        yPressValue = transform.position.y;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(playerController.hasCollided) playerController.MovePlayer(false);
        if (other.transform.position.y <= yPressValue)
        {
            if (playerController.direction.y < 0.2) playerController.hasCollided=false;
            else
            {
                playerController.aController.ChooseAnimationState(playerController.animController, "idleYNeg");
                playerController.hasCollided = true;
            }
        }
        else if (other.transform.position.y >= transform.position.y)
        {
            if (playerController.direction.y > -0.2) playerController.hasCollided=false;
            else
            {
                playerController.aController.ChooseAnimationState(playerController.animController, "idleYPos");
                playerController.hasCollided = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        playerController.hasCollided = false;
    }

}
