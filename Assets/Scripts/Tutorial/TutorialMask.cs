using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialMask{
    public GameObject mask;
    public GameObject gObject;
    private bool isCircle;
    private float xScale, yScale;

    public TutorialMask(GameObject gObject,bool isCircle, float xScale, float yScale) {
        this.gObject = gObject;
        this.isCircle = isCircle;
        this.xScale = xScale;
        this.yScale = yScale;
        createMask();
    }
    public TutorialMask(GameObject gObject, bool isCircle, float scale=2){
        this.gObject = gObject;
        this.isCircle = isCircle;
        this.xScale = scale;
        this.yScale = scale;
        createMask();
    }

    public TutorialMask(GameObject gObject,GameObject mask){
        this.mask = mask;
        this.gObject = gObject;
    }

    private void createMask(){
        string spriteType = this.isCircle ? "Circle" : "Square";
        this.mask = new GameObject(this.gObject.name+"Mask");
        SpriteRenderer spriteRenderer = this.mask.AddComponent<SpriteRenderer>();
        Sprite s = Resources.Load<Sprite>("Sprites/"+spriteType);
        spriteRenderer.sprite = s;
        this.mask.transform.parent = this.gObject.transform;
        this.mask.transform.localPosition = new Vector3(0,0,0);
        this.mask.transform.localScale = new Vector3(this.xScale,this.yScale,0);
        Shader sMask = Shader.Find("Custom/Mask");
        spriteRenderer.material.shader = sMask;
    }
    
}
