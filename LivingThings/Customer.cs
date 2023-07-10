using UnityEngine;
public class Customer : MonoBehaviour {
    [SerializeField]
    private Transform limit,startPoint;

    private void Start(){
        transform.position=startPoint.position;
    }
    private void Update() {
        if(transform.position.y<=limit.position.y-ShopStats.currentCustomerCount){
            transform.position+=new Vector3(0,1,0)*Time.deltaTime*3;
        }
        //else limit = transform.position , pani garna milyo (so arko kati paxadi basxa herna milxa)
    }
}