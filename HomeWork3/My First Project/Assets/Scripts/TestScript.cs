using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject[] myGameObject;
    public float speed;


    void Start()
    {

    }

    //Update is called once per frame

    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical);

        for (int i = 0; i < myGameObject.Length; i++)
        {
            if (myGameObject[i] != null)
            {
                myGameObject[i].transform.position =
                    myGameObject[i].transform.position + movementVector * speed * Time.deltaTime;

                Vector3 pos = myGameObject[i].transform.position;

                if (pos.x > 5f)
                {
                    pos.x = 5f;
                }
                else if(pos.x < -5f)
                {
                    pos.x = -5f;
                }
                if (pos.z > 5f)
                {
                    pos.z = 5f;
                }    
                else if(pos.z < -5f)
                {
                    pos.z = -5f;
                }
                myGameObject[i].transform.position = pos;
            }

        }
    }
}
