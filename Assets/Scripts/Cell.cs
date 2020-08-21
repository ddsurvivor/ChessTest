using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool movable;
    public GameObject moveCell;
    public GameObject attackCell;

    public int playerNumber;

    private GameManager GameManager;
    private AudioManager AudioManager;

    public int GCost;
    public int HCost;
    public int FCost;
    public GameObject parentNode;

    private PathManager PathManager;
    public PathDisplayer PathDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        PathManager = GameObject.Find("PathManager").GetComponent<PathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log("this is a cell 这是一个单元格");

        Debug.Log(" this is a " + tag + " positon: " + transform.position.x + "," + transform.position.y);
        
        if (movable)
        {
            GameManager.selected.GetComponent<Piece>().Move(transform.position.x, transform.position.y);
            AudioManager.audioMove.Play();
        }
        else
        {
            AudioManager.audioClick.Play();
        }

        
    }
    private void OnMouseEnter()
    {
        if (movable)
        {
            PathManager.StartNode = GameManager.selected.GetComponent<Piece>().cell;
            PathManager.EndNode = this.gameObject;
            PathManager.FindPath();            
        }        
    }
    public void SetAttackable(bool _attackable)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Piece"))
            {               
                hit.collider.GetComponent<Piece>().attackable = _attackable;         
            }
        }
    }

    public List<GameObject> GetNeighbour()
    {
        List<GameObject> neighbours = new List<GameObject>();
        Debug.Log("get neighbour");

        Vector2 rayPoint = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(rayPoint + Vector2.up, Vector2.up);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
            Debug.Log(hit.collider.transform.position.x + "," + hit.collider.transform.position.y);
            neighbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.right, Vector2.right);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
            Debug.Log(hit.collider.transform.position.x + "," + hit.collider.transform.position.y);
            neighbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.left, Vector2.left);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
            Debug.Log(hit.collider.transform.position.x + "," + hit.collider.transform.position.y);
            neighbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.down, Vector2.down);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
            Debug.Log(hit.collider.transform.position.x + "," + hit.collider.transform.position.y);
            neighbours.Add(hit.collider.gameObject);
        }

        return neighbours;
    }


    public void CalculateCost()
    {
        GCost = (int)(Mathf.Abs(transform.position.x - PathManager.StartNode.transform.position.x) + Mathf.Abs(transform.position.y - PathManager.StartNode.transform.position.y));
        HCost = (int)(Mathf.Abs(transform.position.x - PathManager.EndNode.transform.position.x) + Mathf.Abs(transform.position.y - PathManager.EndNode.transform.position.y));
        FCost = GCost + HCost;
        //PathDisplayer.ShowCost(GCost, HCost, FCost);
    }
}
