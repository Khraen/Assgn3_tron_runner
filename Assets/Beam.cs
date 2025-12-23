using UnityEngine;

public class Beam : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void EndBeam()
  {
        gameObject.SetActive(false);
  }
  public void TurnColliderOff()
  {
    GetComponent<BoxCollider2D>().enabled = false;
  }
  public void TurnColliderOn()
  {
    GetComponent<BoxCollider2D>().enabled = true;
  }
}
