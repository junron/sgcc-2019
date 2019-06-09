using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TutorialComponent : ITutorialComponent{
    public GameObject mask {get; private set;}
    public GameObject gObject {get; private set;}
    public Text textOutput { get; set; }
    private string text;
    private Action afterInit;
    public bool completed {get; set;} = true;

    public TutorialComponent(TutorialMask mask, string text,Action afterInit=null) {
        this.mask = mask==null ? null : mask.mask;
        this.gObject = mask==null ? null : mask.gObject;
        this.text = text;
        this.afterInit = afterInit;
    }
    public TutorialComponent(string text) : this(null,text) {}
    public TutorialComponent(TutorialMask mask): this(mask,null) {}

    public void Show(){
        Time.timeScale = 0;
        if(this.mask) this.mask.SetActive(true);
        if (this.textOutput != null && this.text != null) this.textOutput.text = this.text;
        if(this.afterInit!=null){
            this.afterInit();
        }
    }
    public void Hide(){
        Time.timeScale = 1;
        if(this.mask) this.mask.SetActive(false);
        if (this.textOutput != null && this.text != null ) this.textOutput.text = "";
    }
}
