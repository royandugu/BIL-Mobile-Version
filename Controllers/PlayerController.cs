using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    public Animator animController;
    private string animationState;
    public Vector2 direction;
    public AnimationController aController;
    private float speed = 4, xPressValue, yPressValue;
    public byte turnDir = 2, phaseValue = 1, collisionTurnDir = 2;
    private bool touchStart = false;
    public bool hasCollided=false;
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

        if (!hasCollided)
        {
            MovePlayer(true);
            AnimatePlayer();
        }
    }
    public void AnimatePlayer()
    {
        if (direction.x == 0 && direction.y == 0)
        {
            if (turnDir == 0) aController.ChooseAnimationState(animController, "idleYNeg");
            else if (turnDir == 1) aController.ChooseAnimationState(animController, "idleYPos");
            else aController.ChooseAnimationState(animController, "idle");
        }
        else if (direction.x >= -0.2 && direction.x <= 0.2 && direction.y != 0)
        {

            if (direction.y > 0)
            {
                turnDir = 0;
                aController.ChooseAnimationState(animController, "walkYNeg");
            }

            else if (direction.y < 0)
            {
                turnDir = 1;
                aController.ChooseAnimationState(animController, "walkYPos");
            }
        }
        else if (direction.y >= -0.2 && direction.y <= 0.2 && direction.x != 0)
        {
            turnDir = 2;
            if (direction.x < 0) FlipPlayer(true);
            else if (direction.x > 0) FlipPlayer(false);

            aController.ChooseAnimationState(animController, "walkX");
        }
        else
        {
            if (direction.x > 0 && direction.y > 0)
            {
                //animate in right top
            }
            else if (direction.x > 0 && direction.y > 0)
            {
                //animate in right top
            }
            else if (direction.x > 0 && direction.y > 0)
            {
                //animate in right top
            }
            else if (direction.x > 0 && direction.y > 0)
            {
                //animate in right top
            }
            else
            {
            }
        }
    }
    
    //User-defined
    public void FlipPlayer(bool condition)
    {
        playerSprite.flipX = condition;
    }
    public void MovePlayer(bool changePosition)
    {
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log("Move player called");
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
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            Vector2 offset = pointB - pointA;
            direction = Vector2.ClampMagnitude(offset, 1.0f);
            innerCircle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

            if (changePosition) ChangePosition();
        }
        else direction = new Vector2(0, 0);
    }
    public void ChangePosition()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Player.xCord = transform.position.x;
        Player.yCord = transform.position.y;

    }

}
