using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager GameManager;
    public Text health;
    public Text armor;
    public Text attack;
    public Text move;

    public Text health2;
    public Text armor2;
    public Text attack2;
    public Text move2;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTurnEnd()
    {
        GameManager.TurnEnd();
    }

    public void ShowInfo(Piece _piece)
    {
        if (_piece.playerNumber == 1)
        {
            health.text = _piece.health.ToString();
            armor.text = _piece.armor.ToString();
            attack.text = _piece.damage.ToString();
            move.text = _piece.moveRange.ToString();
        }
        else if (_piece.playerNumber == 2)
        {
            health2.text = _piece.health.ToString();
            armor2.text = _piece.armor.ToString();
            attack2.text = _piece.damage.ToString();
            move2.text = _piece.moveRange.ToString();
        }
    }
    public void ClearInfo()
    {

    }
}
