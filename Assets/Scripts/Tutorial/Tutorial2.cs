using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial2 : MonoBehaviour
{
    [SerializeField] private GameObject blob;
    [SerializeField] private Text textOutput;
    Choice choice = new Choice();
    
    private ArrayList textLst = new ArrayList();
    private int index = 0;
    public static bool complete = false;

    // Start is called before the first frame update
    void Start()
    {
        MainCharacterController blobScript = blob.GetComponent<MainCharacterController>();
        blobScript.FreeY();        
        choice.Add("charcoal","wood","chicken","spinach","Let's add some vegetables to the soup!","4441");
        choice.Add("dynamite", "tomato","pork","salt","Let's add more vegetables!","4132");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!complete)
        {
            return;
        }
        choice.Show();
        int x = choice.SelectChoice(); 
        //do some action
        choice.Next();
    }

    
}
