using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {

    public Text lapCount;
    public Text Speed;
    public Text place;
    public Transform person;
    public Transform Ai1;
    public Transform Ai2;
    public PlayerCollisions playerCol;
    public int placement = 3;
    public Controls player;
    public float distanceBetween;
    public AIRacer ai1;
    public AIRacer ai2;
    public PlayerRacer playrace;
    Vector3 Distance;

    // Use this for initialization
    void Start () {
        playerCol = GameObject.Find("Player").GetComponent<PlayerCollisions>();
        player = GameObject.Find("Player").GetComponent<Controls>();
        ai1 = GameObject.Find("AI1").GetComponent<AIRacer>();
        ai2 = GameObject.Find("AI2").GetComponent<AIRacer>();
        playrace = GameObject.Find("Player").GetComponent<PlayerRacer>();
	}

    void PositionRank()
    {
        


    }

        // Update is called once per frame
        void Update ()
    {
        //PositionRank();
        lapCount.text = ("Lap: " + playerCol.LapNumber + "/3");
        Speed.text = ("" + Mathf.Round((player.speed * 2.2f)) + " KM/H");
        place.text = ("" + placement);
	}
}
