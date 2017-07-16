using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOperation : MonoBehaviour {

    Animator anim;
    Rigidbody2D rigid2D;

    bool grounded = true;
    public Transform groundcheck;
    float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public float speed = 10f;

	void Start () {
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

	}

    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            grounded = false;
            rigid2D.AddForce(new Vector3(0, 500));
        }
    }
        
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundcheck.position, checkRadius, groundLayer);
        anim.SetBool("jump", !grounded);
        float input = Input.GetAxis("Horizontal");
        Vector2 playerScale = this.transform.localScale;

        if (grounded && input == 0){
            anim.SetBool("stand", true);
        }

        else if(input > 0){
            rigid2D.velocity = new Vector2(input * speed, rigid2D.velocity.y);

            //转向右边
            Vector2 turnRight = new Vector2(Mathf.Abs(playerScale.x), playerScale.y);
            this.transform.localScale = turnRight;

            anim.SetBool("stand", false);

        }

        else if(input < 0)
        {
            rigid2D.velocity = new Vector2(input * speed, rigid2D.velocity.y);

            //转向左边
            Vector2 turnLeft = new Vector2(-Mathf.Abs(playerScale.x), playerScale.y);
            this.transform.localScale = turnLeft;

            anim.SetBool("stand", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.transform.localRotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 0);
        }
	}

}
