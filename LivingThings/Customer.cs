using UnityEngine;
public class Customer : MonoBehaviour {
    [SerializeField]
    

    private void Update() {
        /*Go left till a certain point and go upwards , if (a certain distance already has 
        an another customer waiting, they stand behind the customer and the value kati paxadi basne depends upon ShopStats.currentCustomerCount)
        */
        transform.position+=new Vector3(0,1,0)*Time.deltaTime*3;
    }
}