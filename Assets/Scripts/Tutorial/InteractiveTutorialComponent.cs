using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InteractiveTutorialComponent : ITutorialComponent{
    public GameObject mask {get; private set;}
    public GameObject gObject {get; private set;}
    public Text textOutput { get; set; }
    private string promptText;
    private string completedText;
    private Action<Action> afterInit;
    private float prevTimeScale;
    public bool completed {get; set;}= false;

    public InteractiveTutorialComponent(TutorialMask mask, string promptText,string completedText,
    Action<Action> afterInit ) {
        this.mask = mask.mask;
        this.gObject = mask.gObject;
        this.promptText = promptText;
        this.completedText = completedText;
        this.afterInit = afterInit;
    }

    public void Show(){
        this.prevTimeScale = Time.timeScale;
        Time.timeScale = 1;
        this.mask.SetActive(true);
        this.textOutput.text = this.promptText;
        afterInit(()=>{
            this.textOutput.text = this.completedText;
            this.completed = true;
        });
    }
    public void Hide(){
        Time.timeScale = this.prevTimeScale;
        this.mask.SetActive(false);
        this.textOutput.text = "";
    }
}
