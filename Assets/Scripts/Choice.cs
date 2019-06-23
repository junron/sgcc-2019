using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice
{
    [SerializeField] private Text choice1;
    [SerializeField] private Text choice2;
    [SerializeField] private Text choice3;
    [SerializeField] private Text choice4;
    [SerializeField] private Text choiceText;
    [SerializeField] private GameObject choiceBg;
    public ArrayList lst = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        SetDescription("boop");
    }

    // Update is called once per frame
    void Update()
    {
        if (!choiceBg.activeSelf) 
        {
            return;
        }

    }

    public void Hide()
    {
        choiceBg.SetActive(false);
    }

    public void Show()
    {
        choiceBg.SetActive(true);
    }

    public void SetChoices(string text1, string text2, string text3, string text4){
        choice1.text = text1;
        choice2.text = text2;
        choice3.text = text3;
        choice4.text = text4;
    }
    public void SetDescription(string text)
    {
        choiceText.text = text;
    }

    //I gave up trying to ge the selection to work if user presses another key
    //So if you have a better way help lel
    public int SelectChoice() 
    {
        int input = 5;
        while (input == 5)
        { //apparently something is wrong here what
            input = Select();
        }
        choiceBg.SetActive(false);
        return input;
    }

    int Select() //use numbers to select choices
    {
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            return 2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            return 3;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            return 4;
        }
        else
        {
            return 5;
        }
        
    }

    
}
