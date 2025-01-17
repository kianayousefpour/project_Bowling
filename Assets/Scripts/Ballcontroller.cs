using UnityEngine;

public class Ballcontroller : MonoBehaviour
{
    private  Rigidbody BallRigidbody;
    private  BallManager BallManager;
    [SerializeField] private float moveSpeed;

    private bool canShoot;

    private void Start(){
        GetComponentValues();
    }

    private void Update(){
        HandelShootInput();
    }
    private void FixedUpdate(){
        Shoot();
    }

    private void HandelShootInput(){
        if(Input.GetMouseButtonDown(0)){
            canShoot = true;
        }
    }
    private void Shoot(){
        moveSpeed=15;
        if(canShoot){
            BallRigidbody.AddForce(Vector3.forward * moveSpeed);
        }
    }

    private void OnCollisionExit(Collision other){

        if(other.gameObject.CompareTag("Pin"))
        {
            BallManager.SetActiveBall();
            canShoot = false;
            BallRigidbody.isKinematic = true;
            transform.SetParent(other.gameObject.transform);
        }

    }
    private void GetComponentValues(){
        BallRigidbody = GetComponent<Rigidbody>();
        BallManager = GameObject.FindObjectOfType<BallManager>();
    }
}
