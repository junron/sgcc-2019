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
    private ArrayList _choice1lst = new ArrayList();
    private ArrayList _choice2lst = new ArrayList();
    private ArrayList _choice3lst = new ArrayList();
    private ArrayList _choice4lst = new ArrayList();
    private ArrayList _descriptionlst = new ArrayList();
    public ArrayList correctlst = new ArrayList(); 
    //accessible to other functions so can be changed depending on situation
    public int index = 0; 
    //intended to be used so you can differentiate between different events in a scene
    int size = 0;


    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    public void Hide()
    {
        choiceBg.SetActive(false);
    }

    public void Show()
    {
        choiceBg.SetActive(true);
    }

    public void Add(string text1, string text2, string text3, string text4, string description, string results)
    { 
        AddChoices(text1,text2,text3,text4);
        AddDescription(description);
        AddOutcomes(results);
        size++;
    }

    void AddChoices(string text1, string text2, string text3, string text4){
        _choice1lst.Add(text1);
        _choice2lst.Add(text2);
        _choice3lst.Add(text3);
        _choice4lst.Add(text4);
    }
    void AddDescription(string text)
    {
        _descriptionlst.Add(text);
    }

    void AddOutcomes(string text)
    {//results are given like "1234" to say choice 1 is correct, 2 is correct(?), 3 is incorrect, 4 is wrong
    //so can be given like "1112" or whatever if first 3 is correct and last is correct(?)
        correctlst.Add(text);
    }


    //I gave up trying to ge the selection to work if user presses another key
    //So if you have a better way help lel
    public int SelectChoice() 
    {
        int input = 5;
        while (input == 5)
        {
            input = Select();
        }
        choiceBg.SetActive(false);
        string outcomes = (string) correctlst[index];
        char[] hax = outcomes.ToCharArray();
        int result = int.Parse(hax[input - 1].ToString());
        return result;
    }

    int Select()
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
    public bool Next(){
        if (index == size - 1)
        {
            return false;
        }
        index++;
        return true;
    }
}
