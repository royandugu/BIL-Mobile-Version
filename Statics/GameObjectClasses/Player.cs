public static class Player{
    public static float mentalHealth,xCord=0,yCord=0,pSConv=0,sSConv=0,selfConv=0;
    public static bool canMove=true,canMoveLeft=false,cMoveRight=false,canMoveDown=false,canMoveUp=false;
    public static void ChangeMentalHealth(float parameter){
        mentalHealth+=parameter;
    }
    public static void PlayerMoveSwitch(bool canMoveLeft,bool canMoveRight,bool canMoveUp,bool canMoveDown){
        canMoveLeft=canMoveLeft;
        canMoveRight=canMoveRight;
        canMoveUp=canMoveUp;
        canMoveDown=canMoveDown;
    }
}