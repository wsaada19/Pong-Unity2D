using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public KeyCode startPoint;
    public bool isGameActive;

    public float exitPoint;

    //public float yBound;
    public float speed;

    public Vector2 currentVelocity;

    private Rigidbody2D rgd2d;


	// Use this for initialization
	void Start () {
        rgd2d = gameObject.GetComponent<Rigidbody2D>();
        currentVelocity = Vector2.zero;
        isGameActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().hasEnded)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().StartNewGame();
            }
        }


        if(!isGameActive){

            if(Input.GetKey(startPoint)){
                isGameActive = true;
                BeginPoint();
            }

        } else {

            Vector2 position = transform.position;

            //if (position.y > yBound)
            //{
            //    currentVelocity = Vector2.Reflect(currentVelocity, new Vector2(0, -1));

            //}
            //if (position.y < -yBound)
            //{
            //    currentVelocity = Vector2.Reflect(currentVelocity, new Vector2(0, 1));

            //}

            if (position.x > 19.5)
            {
                HandlePointScored();
            }

            if (position.x < -19.5)
            {
                HandlePointScored();
            }

            rgd2d.velocity = currentVelocity * speed;
        }

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Paddle" || collision.collider.tag == "CPUPaddle"){
            Vector2 vel;
            vel.x = rgd2d.velocity.x;
            vel.y = (rgd2d.velocity.y / 2) + (collision.collider.attachedRigidbody.velocity.y / 3);
            vel.Normalize();
            currentVelocity = vel;
        } else if(collision.collider.tag == "Border"){
            if(transform.position.x > 0){
                currentVelocity = Vector2.Reflect(currentVelocity, new Vector2(0, -1));
            } else {
                currentVelocity = Vector2.Reflect(currentVelocity, new Vector2(0, 1));

            }

        }
       

        //Vector2 normalVector;
        //if (currentVelocity.x < 0){
        //    normalVector = new Vector2(1, 0);
        //} else {
        //    normalVector = new Vector2(1, 0);
        //}
        //currentVelocity = Vector2.Reflect(currentVelocity, new Vector2(1, 0));

    }

    private void HandlePointScored(){
        if(transform.position.x > 0){
            //increase CPU score

            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().IncreasePlayerScore();
        } else {
            //increase player score
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().IncreaseCPUScore();
        }

        transform.position = new Vector2(0, 4);
        currentVelocity = Vector2.zero;
        isGameActive = false;
        GameObject.FindGameObjectWithTag("CPUPaddle").GetComponent<CPUPaddle>().EndCPUPaddle();
    }

    private void BeginPoint(){
        currentVelocity = new Vector2(-1, -1);
        currentVelocity.Normalize();
        GameObject.FindGameObjectWithTag("CPUPaddle").GetComponent<CPUPaddle>().StartCPUPaddle();
    }

}
