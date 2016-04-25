using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour
{

    public CameraScript camera;
    public int count = 3;
    public float countRate;
    public int countFactor = 1;
    public bool canStart = false;
    public Text countdown;


    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        canStart = false;
        count = 3;
    }

    // Update is called once per frame
    void Update()
    {
        StartRound();
    }
    public void StartRound()
    {
        countRate += Time.deltaTime;

        if (canStart == false)
        {
            countdown.gameObject.SetActive(true);
            if (countRate > countFactor)
            {
                count -= 1;
                countRate = 0;
            }
            countdown.text = ("" + count);
            if (count < 1)
            {
                countdown.text = ("GO!");
                

            }
            if(count < 0)
            {
                countdown.gameObject.SetActive(false);
                canStart = true;
            }

        }
    }
}

