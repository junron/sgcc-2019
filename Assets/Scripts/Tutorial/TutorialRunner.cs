﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class TutorialRunner : MonoBehaviour
{
    [SerializeField] private GameObject blob;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject pMask;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Camera cam;
    Tutorial t;
    

    void Start()
    {
        TutorialMask blobMask = new TutorialMask(blob,false,3,2);
        TutorialMask coinMask = new TutorialMask(coin,true,2);
        TutorialMask exitMask = new TutorialMask(exit,true,2);
        TutorialMask pathMask = new TutorialMask(null,pMask);
        MainCharacterController blobScript = blob.GetComponent<MainCharacterController>();
        blobScript.FreezeY();
        t = new Tutorial(tutorialText,"MainScene",cam);
        t.Add(new TutorialComponent(blobMask,"This is Blob. You can control it using the arrow keys."));
        t.Add(
            new TutorialComponent(
            coinMask,
            "This is a coin. Collect coins to gain points!"
            )
        );
        t.Add(
            new InteractiveTutorialComponent(
            pathMask,
            "Lets try out the controls! Press the right arrow key to move blob to the coin!",
            "Yay! You did it!",
            (Action callback)=>{
                
                GameObject.Find("coin").GetComponent<Coin>().setCallback(()=>{
                    // Interactive component cleanup
                    callback();
                });
            }
            )
        );
        t.Add(
            new TutorialComponent(
            exitMask,
            "This is the exit. Navigate Blob to the exit to complete the game!",
            afterInit:()=>{
                blob.GetComponent<Transform>().position = new Vector3(4,3.5f,0);
            }
            )
        );
        t.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            t.Next();
        }
        if(Input.GetKeyDown("q")){
            Application.Quit();
        }
    }
}