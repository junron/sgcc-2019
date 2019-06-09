using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{

    public float speed = 3.0f;
    public bool yFrozen = false;
    private Rigidbody2D rb2d;  
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vert = this.yFrozen ? 0 : Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal,vert,0)*Time.deltaTime*speed;
    }
    public void FreezeY(){
        this.yFrozen = true;
    }
}
