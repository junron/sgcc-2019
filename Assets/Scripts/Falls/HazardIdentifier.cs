using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardIdentifier : MonoBehaviour
{
  [SerializeField] private List<GameObject> hazards;
  [SerializeField] private string[] texts;
  [SerializeField] private int taskStartStringNo;
  [SerializeField] private Text text;
  private bool taskCompleted;
  private int hazardsIdentified;
  private int textNo;

  private void Start()
  {
    text.text = texts[0];
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Space)) return;
    //  Block until task is completed;
    if (textNo == taskStartStringNo && !taskCompleted) return;
    textNo++;
    if (textNo >= texts.Length)
    {
      if (textNo == texts.Length) this.gameObject.GetComponent<HazardRemover>().Activate();
      Destroy(this);
      return;
    }

    text.text = texts[textNo];
    if (textNo != taskStartStringNo) return;
    foreach (GameObject hazard in hazards)
    {
      Clickable c = hazard.GetComponent<Clickable>();
      c.clicked = false;
      c.callback = () =>
      {
        if (c.clicked) return;
        hazardsIdentified++;
        taskCompleted = hazardsIdentified == hazards.Count;
        if (taskCompleted)
        {
          text.text = "Congratulations! You have identified all hazards!";
        }
        else
        {
          text.text = $"{hazardsIdentified} hazards identified!";
        }
      };
    }
  }
}