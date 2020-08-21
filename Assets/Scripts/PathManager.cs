using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<GameObject> OpenList;
    public List<GameObject> ClosedList;

    public GameObject StartNode;
    public GameObject EndNode;
    // Start is called before the first frame update
    void Start()
    {
        OpenList = new List<GameObject>();
        ClosedList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClearNode()
    {
        foreach (var item in ClosedList)
        {
            item.GetComponent<Cell>().PathDisplayer.CancleNode();
            item.GetComponent<Cell>().parentNode = null;
        }
        foreach (var item in OpenList)
        {
            item.GetComponent<Cell>().PathDisplayer.CancleNode();
            item.GetComponent<Cell>().parentNode = null;
        }
               
        OpenList.Clear();
        ClosedList.Clear();
    }

    public void FindPath()
    {
        ClearNode();
        OpenList.Add(StartNode);
        for (int i = 0; i < 100; i++)
        {
            GameObject current = LowestFCost(OpenList);
            OpenList.Remove(current);
            ClosedList.Add(current);
            if (current == EndNode)
            {
                Debug.Log("Path finished");
                ShowPathWay();
                return;
            }

            List<GameObject> neighbours = current.GetComponent<Cell>().GetNeighbour();
            foreach (var cell in neighbours)
            {
                if (cell.GetComponent<Cell>().playerNumber != 0 || FindIn(cell, ClosedList))
                {
                    continue; 
                }
                if (!FindIn(cell,OpenList))
                {
                    cell.GetComponent<Cell>().CalculateCost();
                    cell.GetComponent<Cell>().parentNode = current;
                    OpenList.Add(cell);
                }
            }
        }
        
        
    }

    public GameObject LowestFCost(List<GameObject> _cells)
    {
        int f = 1000;
        GameObject output = _cells[0];
        foreach (var cell in _cells)
        {
            if (cell.GetComponent<Cell>().FCost<=f)
            {
                f = cell.GetComponent<Cell>().FCost;
                output = cell;
            }            
        }
        return output;
    }

    public bool FindIn(GameObject gb, List<GameObject> _cells)
    {
        foreach (var cell in _cells)
        {
            if (gb == cell)
            {
                return true;
            }
        }
        return false;
    }

    public void ShowPathWay()
    {
        GameObject begin = EndNode;
        GameObject last = EndNode;
        GameObject next;
        for (int i = 0; i < 1000; i++)
        {
            
            next = begin.GetComponent<Cell>().parentNode;
            if (i == 0)
            {
                begin.GetComponent<Cell>().PathDisplayer.arrow.SetActive(true);
                if (next.transform.position.y>begin.transform.position.y)
                {
                    begin.GetComponent<Cell>().PathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (next.transform.position.x > begin.transform.position.x)
                {
                    begin.GetComponent<Cell>().PathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else if (next.transform.position.x < begin.transform.position.x)
                {
                    begin.GetComponent<Cell>().PathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                }
                else if (next.transform.position.y < begin.transform.position.y)
                {
                    begin.GetComponent<Cell>().PathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            else
            {
                if (last.transform.position.y > begin.transform.position.y)
                {
                    if (next.transform.position.y < begin.transform.position.y)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.line.SetActive(true);
                    }
                    else if (next.transform.position.x > begin.transform.position.x)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    else if (next.transform.position.x < begin.transform.position.x)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    }
                }
                else if (last.transform.position.y < begin.transform.position.y)
                {
                    if (next.transform.position.y > begin.transform.position.y)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.line.SetActive(true);
                    }
                    else if (next.transform.position.x > begin.transform.position.x)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);                        
                    }
                    else if (next.transform.position.x < begin.transform.position.x)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    }
                }
                else if (last.transform.position.x > begin.transform.position.x)
                {
                    if (next.transform.position.x < begin.transform.position.x)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.line.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    else if (next.transform.position.y > begin.transform.position.y)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    else if (next.transform.position.y < begin.transform.position.y)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);                       
                    }
                }
                else if (last.transform.position.x < begin.transform.position.x)
                {
                    if (next.transform.position.x > begin.transform.position.x)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.line.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    }
                    else if (next.transform.position.y > begin.transform.position.y)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    }
                    else if (next.transform.position.y < begin.transform.position.y)
                    {
                        begin.GetComponent<Cell>().PathDisplayer.corner.SetActive(true);
                        begin.GetComponent<Cell>().PathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    }
                }
                
            }
            
            last = begin;
            begin = begin.GetComponent<Cell>().parentNode;
            if (begin == StartNode)
            {
                return;
            }
        }
    }
}
