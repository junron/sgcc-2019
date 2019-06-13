using UnityEngine;

public class TutorialMask
{
  public GameObject mask;
  public GameObject gObject;
  private bool isCircle;
  private float xScale, yScale;

  public TutorialMask(GameObject gObject, bool isCircle, float xScale, float yScale)
  {
    this.gObject = gObject;
    this.isCircle = isCircle;
    this.xScale = xScale;
    this.yScale = yScale;
    createMask();
  }

  public TutorialMask(GameObject gObject, bool isCircle, float scale = 2)
  {
    this.gObject = gObject;
    this.isCircle = isCircle;
    xScale = scale;
    yScale = scale;
    createMask();
  }

  public TutorialMask(GameObject gObject, GameObject mask)
  {
    this.mask = mask;
    this.gObject = gObject;
  }

  private void createMask()
  {
    string spriteType = isCircle ? "Circle" : "Square";
    mask = new GameObject(gObject.name + "Mask");
    SpriteRenderer spriteRenderer = mask.AddComponent<SpriteRenderer>();
    Sprite s = Resources.Load<Sprite>("Sprites/" + spriteType);
    spriteRenderer.sprite = s;
    mask.transform.parent = gObject.transform;
    mask.transform.localPosition = new Vector3(0, 0, 0);
    mask.transform.localScale = new Vector3(xScale, yScale, 0);
    Shader sMask = Shader.Find("Custom/Mask");
    spriteRenderer.material.shader = sMask;
  }
}