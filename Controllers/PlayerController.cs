using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private Animator animController;
    private string animationState;
    private Vector2 direction;
    private AnimationController aController;
    private float speed = 4, xPressValue, yPressValue;
    public byte turnDir = 2, phaseValue = 1, collisionTurnDir = 2;
    private bool hasCollided = false, touchStart = false;
    private Vector2 pointA;
    [SerializeField]
    private Transform innerCircle, outerCircle;
    private Vector2 pointB;
    //Unity built-ins
    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();
        aController = new AnimationController("idleX");

        animController.speed = 0.5f;

        if (BasicGameDetails.isOld)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "GameInfo");
            string jsonString = System.IO.File.ReadAllText(filePath);
            GameSaver.SaveFormat sf = JsonUtility.FromJson<GameSaver.SaveFormat>(jsonString);
            Player.xCord = sf.PlayerInfo.xCord;
            Player.yCord = sf.PlayerInfo.yCord;
            Player.mentalHealth = sf.PlayerInfo.mentalHealth;
            Player.pSConv = sf.PlayerInfo.pSConv;
            Player.sSConv = sf.PlayerInfo.sSConv;
        }
        else if (!BasicGameDetails.isTempOld && !BasicGameDetails.isOld)
        {
            Player.xCord = 0;
            Player.yCord = 0;
            Player.mentalHealth = 50;
            Player.pSConv = 0;
            Player.sSConv = 0;
        }
        transform.position = new Vector3(Player.xCord, Player.yCord, 0);
    }
    private void Update()
    {
        if (SceneLoader.GetCurrentScene() == "MainMenu") Destroy(this);

        //PlayerShop entry from PlayerBedRoom
        if (SceneLoader.GetCurrentScene() == "PlayerShop" && SceneStateKeeper.currentScene == "PBedRoom")
        {
            transform.position = new Vector3(0, 0, 0);
            SceneStateKeeper.currentScene = "PlayerShop";
        }
       else DontDestroyOnLoad(this);

        if(Input.GetKeyDown(KeyCode.A)) Debug.Log(direction);
       MovePlayer();
    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            direction = Vector2.ClampMagnitude(offset, 1.0f);
            
            transform.Translate(direction * speed * Time.deltaTime);
            Player.xCord = transform.position.x;
            Player.yCord = transform.position.y;


            innerCircle.transform.position=new Vector2(pointA.x+direction.x,pointA.y+direction.y);

        }
        else
        {
            innerCircle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        hasCollided = true;
        collisionTurnDir = turnDir;
    }

    public void AnimatePlayer(){
        
        if(direction.x>=-0.2 && direction.x<=0.2 && direction.y!=0){
            
            if(direction.y>0){
                aController.ChooseAnimationState(animController,"walkYNeg");              
            }  
            
            else if(direction.y<0){
                aController.ChooseAnimationState(animController,"walkYPos");
            }      
        }
        else if(direction.y>=-0.2 && direction.y<=0.2 && direction.x!=0){
            
            if(direction.x>0){
                //Animate player in x-right                
            }  
    
            else if(direction.x<0){
                //ANimate player in x-left
            }      
        }
        else{
            if(direction.x>0 && direction.y>0){
                //animate in right top
            }
            else if(direction.x>0 && direction.y>0){
                //animate in right top
            }
            else if(direction.x>0 && direction.y>0){
                //animate in right top
            }
            else if(direction.x>0 && direction.y>0){
                //animate in right top
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (collisionTurnDir == 0)
        {
            if (yPressValue == -1)
            {
                hasCollided = true;
                aController.ChooseAnimationState(animController, "idleYPos");
            }
            else hasCollided = false;
        }
        else if (collisionTurnDir == 1)
        {
            if (yPressValue == 1)
            {
                hasCollided = true;
                aController.ChooseAnimationState(animController, "idleYNeg");
            }
            else hasCollided = false;
        }
        else
        {
            if (playerSprite.flipX)
            {
                if (xPressValue == -1)
                {
                    hasCollided = true;
                    aController.ChooseAnimationState(animController, "idleX");
                    playerSprite.flipX = true;
                }
                else hasCollided = false;
            }
            else
            {
                if (xPressValue == 1)
                {
                    hasCollided = true;
                    aController.ChooseAnimationState(animController, "idleX");
                }
                else hasCollided = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        hasCollided = false;
    }
    //User-defined
    public void FlipPlayer(bool condition)
    {
        if (condition)
        {
            if (xPressValue < 0) playerSprite.flipX = true;
            else playerSprite.flipX = false;
        }
        else playerSprite.flipX = true;
    }

    // public void AnimatePlayer()
    // {
    //     if (xPressValue == 0 && yPressValue == 0)
    //     {
    //         if (turnDir == 0) aController.ChooseAnimationState(animController, "idleYPos");
    //         else if (turnDir == 1) aController.ChooseAnimationState(animController, "idleYNeg");
    //         else if (turnDir == 2) aController.ChooseAnimationState(animController, "idleX");
    //     }
    //     else
    //     {
    //         if (xPressValue != 0 && yPressValue != 0)
    //         {
    //             turnDir = 2;
    //             FlipPlayer(true);
    //             aController.ChooseAnimationState(animController, "walkX");
    //         }
    //         else
    //         {
    //             if (yPressValue > 0)
    //             {
    //                 turnDir = 1;
    //                 aController.ChooseAnimationState(animController, "walkYNeg");
    //             }
    //             if (yPressValue < 0)
    //             {
    //                 turnDir = 0;
    //                 aController.ChooseAnimationState(animController, "walkYPos");
    //             }
    //             if (yPressValue == 0)
    //             {
    //                 turnDir = 2;
    //                 FlipPlayer(true);
    //                 aController.ChooseAnimationState(animController, "walkX");
    //             }
    //         }
    //     }
    // }
    public void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {

            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            innerCircle.transform.position = pointA;
            outerCircle.transform.position = pointA;

            innerCircle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;

        } 
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else touchStart = false;
        AnimatePlayer();
    }

}
