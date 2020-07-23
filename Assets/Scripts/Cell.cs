using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool movabel;
    public GameObject moveCell;
    public GameObject attackCell;

    public int playerNumber;

    private GameManager GameManager;
    private AudioManager AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log("this is a cell 这是一个单元格");
        Debug.Log(" this is a " + tag + " positon: " + transform.position.x + "," + transform.position.y);
        
        if (movabel)
        {
            GameManager.selected.GetComponent<Piece>().Move(transform.position.x, transform.position.y);
            AudioManager.audioMove.Play();
        }
        else
        {
            AudioManager.audioClick.Play();
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
}
