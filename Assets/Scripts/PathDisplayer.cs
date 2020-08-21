using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathDisplayer : MonoBehaviour
{
    public GameObject Canvas;
    public Text GCostText;
    public Text HCostText;
    public Text FCostText;    

    public GameObject line;
    public GameObject arrow;
    public GameObject corner;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CancleNode()
    {
        Canvas.SetActive(false);
        line.SetActive(false);
        arrow.SetActive(false);
        corner.SetActive(false);        
    }

    public void ShowCost(int _g, int _h, int _f)
    {
        Canvas.SetActive(true);
        GCostText.text = _g.ToString();
        HCostText.text = _h.ToString();
        FCostText.text = _f.ToString();
    }
}
