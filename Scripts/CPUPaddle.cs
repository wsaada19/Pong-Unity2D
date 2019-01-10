using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPaddle : MonoBehaviour {

    private GameObject ball;
    public float changeDirectionTime;
    public float speed;

    private Vector2 currentSpeed;
    private Rigidbody2D rgbd;

	// Use this for initialization
	void Start () {
        rgbd = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        currentSpeed = Vector2.zero;


	}
	
	// Update is called once per frame
	void Update () {
        rgbd.velocity = currentSpeed;
	}

    private void ChangeDirection(){
        float yOfBall = ball.transform.position.y;
        Vector2 vel = rgbd.velocity;
        if(gameObject.transform.position.y < yOfBall){
            vel.y = speed;
        } else {
            vel.y = -speed;

        }
        currentSpeed = vel;

    }

    public void StartCPUPaddle(){
        InvokeRepeating("ChangeDirection", 1f, changeDirectionTime);
    }

    public void EndCPUPaddle(){
        CancelInvoke("ChangeDirection");
        transform.position = new Vector3(19.5f, 0);
        currentSpeed = Vector2.zero;
    }
}
