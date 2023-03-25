using UnityEngine;
public class Customer : MonoBehaviour {
    private void Start() {
        transform.position=new Vector3(0,0,0); //Generation point
    }
    private void Update() {
        /*Go left till a certain point and go upwards , if (a certain distance already has 
        an another customer waiting, they stand behind)
        */
        transform.position-=new Vector3(1,0,0)*Time.deltaTime*5;
    }
}