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
    [SerializeField] private Collider2D[] targets;
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
            float minDistance = 1000f;
            Vector3 closest = Vector3.zero;
            foreach (Collider2D target in targets)
            {
                if(target==null){
                    continue;
                }
                Vector3 closestPoint = target.ClosestPoint(transform.position);
                float distance = Vector3.Distance(closestPoint, transform.position);
                if(distance<minDistance){
                    minDistance = distance;
                    closest = closestPoint;
                }
            }
            if(closest==Vector3.zero){
                return;
            }
            float distanceX = Mathf.Abs(closest.x-transform.position.x);
            float distanceY = Mathf.Abs(closest.y-transform.position.y);
            Debug.Log(distanceX+" "+distanceY);
            if(distanceX>distanceY){
                // Move horizontally first
                int horizontal = closest.x > transform.position.x ? 1 :-1;
                transform.position += new Vector3(horizontal,0,0)*Time.deltaTime*speed;
            }else{
                int vertical = closest.y > transform.position.y ? 1 :-1;
                transform.position += new Vector3(0,vertical,0)*Time.deltaTime*speed;
            }
        }
    }
    public void FreezeY(){
        this.yFrozen = true;
    }
}
