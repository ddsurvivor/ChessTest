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


    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        TurnText = GameObject.Find("TurnText").GetComponent<Animator>();

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
        foreach (var cell in cells)
        {
            int range = selected.GetComponent<Piece>().moveRange;
            if (Mathf.Abs(cell.transform.position.x - selected.transform.position.x) + Mathf.Abs(cell.transform.position.y - selected.transform.position.y) <= range && cell.GetComponent<Cell>().playerNumber == 0)
            {
                cell.GetComponent<Cell>().movabel = true;
                cell.GetComponent<Cell>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }
    }
    public void CloseMoveRange()
    {
        foreach (var cell in moveList)
        {
            cell.GetComponent<Cell>().movabel = false;
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
        foreach (var cell in moveList)
        {
            cell.GetComponent<Cell>().movabel = false;
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

    
    
}
