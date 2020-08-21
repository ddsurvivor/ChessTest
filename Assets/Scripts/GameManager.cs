using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject[] pieces;

    public GameObject selected;

    private List<GameObject> moveList;
    private List<GameObject> attackList;

    public int[] pieceCount;

    public int mainPlayer;
    public int nextPlayer;

    private AudioManager AudioManager;
    private Animator TurnText;

    private PathManager PathManager;


    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        TurnText = GameObject.Find("TurnText").GetComponent<Animator>();
        PathManager = GameObject.Find("PathManager").GetComponent<PathManager>();

        moveList = new List<GameObject>();
        attackList = new List<GameObject>();

        SetCount();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CloseAllRanges();
            AudioManager.audioCancel.Play();
        }
        
    }

    public void ShowMoveRange()
    {
        CloseAllRanges();
        
        List<GameObject> now = new List<GameObject>();
        List<GameObject> open = new List<GameObject>();
        List<GameObject> closed = new List<GameObject>();

        now.Add(selected.GetComponent<Piece>().cell);

        int range = selected.GetComponent<Piece>().moveRange;
        for (int i = 0; i < range; i++)
        {
            foreach (var current in now)
            {
                closed.Add(current);
                List<GameObject> neighbours = current.GetComponent<Cell>().GetNeighbour();
                foreach (var neighbour in neighbours)
                {
                    if (neighbour.GetComponent<Cell>().playerNumber != 0 || FindIn(neighbour,closed))
                    {
                        continue;
                    }
                    if (!FindIn(neighbour,open))
                    {
                        open.Add(neighbour);
                        neighbour.GetComponent<Cell>().movable = true;
                        neighbour.GetComponent<Cell>().moveCell.SetActive(true);
                        moveList.Add(neighbour);
                    }
                }
            }
            
            now.Clear();
            foreach (var item in open)
            {
                now.Add(item);
            }
            open.Clear();
        }

    }
    public void CloseMoveRange()
    {
        foreach (var cell in moveList)
        {
            cell.GetComponent<Cell>().movable = false;
            cell.GetComponent<Cell>().moveCell.SetActive(false);
        }
        moveList.Clear();       
    }
    public void ShowAttackRange()
    {
        CloseAllRanges();
        foreach (var cell in cells)
        {
            int range = selected.GetComponent<Piece>().attackRange;
            if (Mathf.Abs(cell.transform.position.x - selected.transform.position.x) + Mathf.Abs(cell.transform.position.y - selected.transform.position.y) <= range &&
              cell.GetComponent<Cell>().playerNumber != selected.GetComponent<Piece>().playerNumber)
            {
                //cell.GetComponent<Cell>().movabel = true;
                cell.GetComponent<Cell>().attackCell.SetActive(true);
                attackList.Add(cell);
                if (cell.GetComponent<Cell>().playerNumber != 0)
                {
                    cell.GetComponent<Cell>().SetAttackable(true);
                }
            }
        }
    }
    public void CloseAttackRange()
    {
        foreach (var cell in attackList)
        {
            //cell.GetComponent<Cell>().movabel = false;
            cell.GetComponent<Cell>().attackCell.SetActive(false);
        }
        attackList.Clear();

    }
    public void CloseAllRanges()
    {
        PathManager.ClearNode();
        foreach (var cell in moveList)
        {
            cell.GetComponent<Cell>().movable = false;
            cell.GetComponent<Cell>().moveCell.SetActive(false);
        }
        moveList.Clear();
        foreach (var cell in attackList)
        {
            //cell.GetComponent<Cell>().movabel = false;
            cell.GetComponent<Cell>().attackCell.SetActive(false);
            cell.GetComponent<Cell>().SetAttackable(false);
        }
        attackList.Clear();
    }

    public void TurnEnd()
    {
        CloseAllRanges();
        selected = null;

        if (mainPlayer == 1)
        {
            TurnText.SetTrigger("RedTurn");
        }
        else if (mainPlayer == 2)
        {
            TurnText.SetTrigger("BlueTurn");
        }

        int t = mainPlayer;
        mainPlayer = nextPlayer;
        nextPlayer = t;

        pieces = GameObject.FindGameObjectsWithTag("Piece");

        foreach (var piece in pieces)
        {
            Piece p = piece.GetComponent<Piece>();
            if (p.playerNumber == mainPlayer)
            {
                p.ReSet();
            }
        }
        

        Debug.Log("Turn end, now player is: " + mainPlayer);
    }
    public void SetCount()
    {
        pieceCount = new int[3];
        pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (var piece in pieces)
        {
            Piece p = piece.GetComponent<Piece>();
            pieceCount[p.playerNumber]++;
        }
    }
    public void PieceDestroy(GameObject _gameObject)
    {
        int playerNumber = _gameObject.GetComponent<Piece>().playerNumber;
        pieceCount[playerNumber]--;
        if (pieceCount[playerNumber] <=0)
        {
            Debug.Log("player " + playerNumber + " lose");
        }
        Destroy(_gameObject);
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

}
