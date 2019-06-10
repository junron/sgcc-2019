using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{

    public float speed = 3.0f;
    public bool yFrozen = false;
    // Should actually be called randomize
    public bool fsm = false;
    private Vector3 currentState = Vector3.up;
    private Vector3[] possibleStates = {Vector3.up,Vector3.down,Vector3.right,Vector3.left};
    private Rigidbody2D rb2d;
    [SerializeField] private Collider2D e;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        rb2d.freezeRotation = true;
        if(fsm){
            Time.timeScale = 0.5f;
            this.rb2d.AddForce(this.currentState*300);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!fsm){
            float horizontal = Input.GetAxis("Horizontal");
            float vert = this.yFrozen ? 0 : Input.GetAxis("Vertical");
            transform.position += new Vector3(horizontal,vert,0)*Time.deltaTime*speed;
        }
    }
    void FixedUpdate()
    {
        if(fsm){
            Vector3 closestPoint = e.ClosestPoint(transform.position);
            float distance = Vector3.Distance(closestPoint, transform.position);
            // If it hits something...
            if (distance<0.55)
            {
                Debug.Log("Hit");
                Vector3 newState = this.currentState;
                while(newState==currentState){
                    int choice = Random.Range(0,4);
                    Debug.Log(choice);
                    newState = this.possibleStates[choice];
                }
                this.currentState = newState;
                this.rb2d.AddForce(this.currentState*300);
            }
        }
    }
    public void FreezeY(){
        this.yFrozen = true;
    }
}
