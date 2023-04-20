using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumMove : Enemy
{
    private Rigidbody2D opossum;
    public Transform LeftPoint, RightPoint;
    private float LeftPosition, RightPosition;
    private bool isFaceLeft = true;
    private float Speed = 1F;


    GameObject PlayerHead;
    private float R = 4;
    private float distance;

    protected override void Start()
    {
        base.Start();
        opossum = this.GetComponent<Rigidbody2D>();
        LeftPosition = LeftPoint.position.x;
        RightPosition = RightPoint.position.x;
        Destroy(LeftPoint.gameObject);
        Destroy(RightPoint.gameObject);
        PlayerHead = GameObject.Find("PlayerHead");
    }

    void Update()
    {
        MoveMent();

        if (Mathf.Abs(transform.position.y - PlayerHead.transform.position.y) <= 0.5f)
        {
            distance = transform.position.x - PlayerHead.transform.position.x;
            if (distance <= R)
                MoveToPlayer();
        }
        else
        {
            MoveMent();
        }
    }
    private void MoveToPlayer()
    {
        Debug.Log("！靠近敌人");
        if (transform.position.x - PlayerHead.transform.position.x > 0)  //敌人在右边  me   敌
        {

            if (!isFaceLeft)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                isFaceLeft = true;
            }
            opossum.velocity = new Vector2(-Speed * 3, opossum.velocity.y);
        }
        else     //敌人   me
        {
            if (isFaceLeft)
            {
                isFaceLeft = false;
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            opossum.velocity = new Vector2(Speed * 3, opossum.velocity.y);
        }
    }

    private void MoveMent()
    {
        if (!this.isDeathing)
        {
            if (isFaceLeft)
            {
                opossum.velocity = new Vector2(-Speed, opossum.velocity.y);
                if (this.transform.position.x <= LeftPosition)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                    isFaceLeft = false;
                }
            }
            else
            {
                opossum.velocity = new Vector2(Speed, opossum.velocity.y);
                if (this.transform.position.x >= RightPosition)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                    isFaceLeft = true;
                }
            }
        }
    }
}
