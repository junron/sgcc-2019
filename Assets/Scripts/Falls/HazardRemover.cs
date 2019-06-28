using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardRemover : MonoBehaviour
{
  [SerializeField] private List<GameObject> hazards;
  [SerializeField] private string[] texts;
  [SerializeField] private int taskStartStringNo;
  [SerializeField] private Text text;
  [SerializeField] private List<GameObject> mitigations;
  [SerializeField] private List<GameObject> replacementSprites;
  [SerializeField] private GameObject mits;
  [SerializeField] private GameObject bg;
  [SerializeField] private HealthBar health;
  [SerializeField] private GameObject exit;
  [SerializeField] private Rigidbody2D player;

  private bool taskCompleted;
  private int hazardsRemoved;
  private int textNo;
  private bool activated;

  public void Activate()
  {
    Debug.Log("Activated");
    text.text = texts[0];
    activated = true;
  }

  private void Update()
  {
    if (!activated) return;
    if (!Input.GetKeyDown(KeyCode.Space)) return;
    //  Block until task is completed
    if (textNo == taskStartStringNo && !taskCompleted) return;
    textNo++;
    if (textNo >= texts.Length)
    {
      return;
    }

    text.text = texts[textNo];
    if (textNo == 7)
    {
      exit.SetActive(true);
      player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    if (textNo != taskStartStringNo) return;
    mits.SetActive(true);
    bg.SetActive(true);
    int i = 0;
    foreach (GameObject hazard in hazards)
    {
      DropTarget dt = hazard.GetComponentInChildren<DropTarget>();
      Droppable answer = mitigations[i].GetComponent<Droppable>();
      int i1 = i;
      dt.AddCallback(mitigation =>
      {
        if (answer == mitigation)
        {
          hazardsRemoved++;
          text.text = $"That's right! {hazardsRemoved} hazards removed!";
          Destroy(mitigations[i1]);
          if (replacementSprites[i1] == null || replacementSprites[i1] == null)
          {
            Destroy(hazard);
          }
          else
          {
            if (hazard != replacementSprites[i1])
            {
              Debug.Log("hmm");
              Destroy(hazard);
              replacementSprites[i1].SetActive(true);
            }
          }

        }
        else
        {
          mitigation.MoveHome();
          health.barValue -= 10;
          Variables.health -= 10;
          text.text = "That's not the correct answer. Try again!";
        }

        taskCompleted = hazardsRemoved == hazards.Count;
        if (!taskCompleted) return;
        text.text = "Congratulations! You have removed all hazards!";
        mits.SetActive(false);
        bg.SetActive(false);
      });
      i++;
    }
  }
}