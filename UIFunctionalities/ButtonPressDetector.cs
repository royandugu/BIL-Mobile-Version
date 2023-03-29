using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonPressDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private bool buttonDown=false;
    [SerializeField]
    private byte value;
    public void OnPointerDown(PointerEventData eventData){
        buttonDown=true;
    }
    public void OnPointerUp(PointerEventData eventData){
        buttonDown=false;
    }
    private void Update(){
        if(buttonDown==true){
            if(value==0) Player.PlayerMoveSwitch(true,false,false,false);
            else if(value==1) Player.PlayerMoveSwitch(false,true,false,false);
            else if(value==2) Player.PlayerMoveSwitch(false,false,true,false);
            else if(value==3) Player.PlayerMoveSwitch(false,false,false,true);
        }
    }
}
