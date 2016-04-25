using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    public GameObject greenShell;
    public AudioSource[] audio;
    public float speed = 0.1f;
    public float reverseSpeed = 0.1f;
    public float rotateSpeed = 0.1f;
    public float pitchSpeed = 0.0f;
    public float VolumeSpeed;
    public Countdown count;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public Rigidbody rb;
    


    // Use this for initialization
    void Start() {
        audio = GetComponents<AudioSource>();
        count = GameObject.Find("CutsceneManager").GetComponent<Countdown>();
        rb = GetComponent<Rigidbody>();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 3, Color.green, 120);
        RaycastHit Hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.forward, out Hit, 1))
        {
            if (Hit.collider.gameObject.name.Contains("Bound"))
            {
                transform.position += -transform.forward * 10 * Time.deltaTime;
                speed = 0;
            }
        }
        if (count.canStart == true)
        {
            Movement();
            Text1.SetActive(true);
            Text2.SetActive(true);
            Text3.SetActive(true);
        }

        if(pitchSpeed > 1.1f)
        {
            pitchSpeed = pitchSpeed - 0.01f;
        } else if(pitchSpeed < 0.5f)
        {
            pitchSpeed = 0.5f;
        }
        if(VolumeSpeed < 0)
        {
            VolumeSpeed = 0;
        } else if (VolumeSpeed > 1)
        {
            VolumeSpeed = 1;
        }
        if (speed >= 25)
        {
            speed = 25;
        }
        else if (speed < 0)
        {
            speed = 0;
        }
        if (reverseSpeed >= 15)
        {
            reverseSpeed = 15;
        }
        if (rotateSpeed >= 2)
        {
            rotateSpeed = 1;
        }
        else if (rotateSpeed < 0)
        {
            rotateSpeed = 0;
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.X))
        {
            pitchSpeed += Time.deltaTime / 5;
            VolumeSpeed += Time.deltaTime / 2;
            audio[0].Play();
            audio[0].pitch = pitchSpeed;
            audio[0].volume = VolumeSpeed;
            speed += Time.deltaTime * 5;
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            VolumeSpeed -= Time.deltaTime * 1.1f;
            pitchSpeed -= Time.deltaTime * 1.1f;
            audio[0].pitch = pitchSpeed;
            audio[0].volume = VolumeSpeed;
            transform.position += transform.forward * speed * Time.deltaTime;
            speed -= Time.deltaTime * 15;
        }
        if (speed > 0)
        {

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotateSpeed += Time.deltaTime;
                transform.Rotate(Vector3.down * rotateSpeed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotateSpeed += Time.deltaTime;
                transform.Rotate(Vector3.up * rotateSpeed);
            }
        }
        if(speed <= 0)
        {
            if(Input.GetKey(KeyCode.C))
            {
               // reverseSpeed += Time.deltaTime;
                transform.position -= transform.forward * 5* Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.LeftArrow))
            {
                // reverseSpeed += Time.deltaTime;
                transform.position -= transform.forward * 5 * Time.deltaTime;
                rotateSpeed += Time.deltaTime;
                transform.Rotate(Vector3.down * rotateSpeed);
            }
            if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.RightArrow))
            {
                // reverseSpeed += Time.deltaTime;
                transform.position -= transform.forward * 5 * Time.deltaTime;
                rotateSpeed += Time.deltaTime;
                transform.Rotate(Vector3.up * rotateSpeed);
            }
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GameObject newShell = Instantiate(greenShell, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity) as GameObject;
            Rigidbody con = newShell.GetComponent<Rigidbody>();
            con.AddRelativeForce(transform.forward * 2000 - con.velocity);
            audio[2].Play();
        }
        if(speed <= 0)
        {
            audio[0].Stop();
            audio[0].volume = 0;
        }

    }
   
}
        

