using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

    private Rigidbody rb;

    private int count;

    private bool isjump;

    public int speed;

    public Text countText;

    public Text wintext;

    public Text timers;

    float tim = 30;

    bool isend;

	// Use this for initialization
	void Start () {
        wintext.text = "";
        timers.text = tim.ToString();

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        isjump = true;
        isend = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isend)
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.Space) && isjump)
            {
                isjump = false;
                rb.AddForce(new Vector3(0, 500, 0));
            }

            if (count >= 11 && tim > 0)
            {
                wintext.text = "You Win!";
                isend = true;
            }
            tim -= Time.deltaTime;
            timers.text = ((int)tim).ToString();

            Vector3 movement = new Vector3(moveH, 0.0f, moveV);
            rb.AddForce(movement*speed);
        }
        else
        {
            if(wintext.text=="")
                wintext.text = "You Lose!";
        }
        if (tim < 0) isend = true;

        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            countText.text = "Count: " + count.ToString();
        }
       // Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "JumpStair")
        {
            isjump = true;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
