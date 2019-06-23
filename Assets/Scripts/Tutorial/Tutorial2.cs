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
        choice.SetDescription("Let's add some vegetables to the soup!");
        choice.SetChoices("charcoal","wood","chicken","spinach");
        
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
        if (x == 4)
            Next();

    }

    void Next(){
        if (index == textLst.Count)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainScene");
        }
        choice.SetDescription("Let's add more vegetables!");
        choice.SetChoices("dynamite", "tomato","pork","salt");

    }
}
