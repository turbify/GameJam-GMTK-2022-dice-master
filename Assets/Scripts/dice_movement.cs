using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class dice_movement : MonoBehaviour
{
    AudioSource audioData;
    public int moveSpeed = 5;
    public int tileDistance = 2;
    public bool jump = false;

    public Vector3 targetVector;

    public GameObject visualChildren;
    public GameObject rotatorChildren;

    private void Update()
    {
        Movement();
    }

    private void Start()
    {
        visualChildren = this.gameObject.transform.GetChild(0).gameObject;
        rotatorChildren = this.gameObject.transform.GetChild(1).gameObject;
    }

    public void RollDice(string direction)
    {
        switch (direction)
        {
            case "up":
                targetVector = new Vector3(targetVector.x, targetVector.y, targetVector.z + tileDistance);
                rotatorChildren.transform.Rotate(90, 0, 0, Space.World);
                audioPlay();
                //jump = true;
                break;
            case "down":
                targetVector = new Vector3(targetVector.x, targetVector.y, targetVector.z - tileDistance);
                rotatorChildren.transform.Rotate(-90, 0, 0, Space.World);
                audioPlay();
                break;
            case "right":
                targetVector = new Vector3(targetVector.x + tileDistance, targetVector.y, targetVector.z);
                rotatorChildren.transform.Rotate(0, 0, -90, Space.World);
                audioPlay();
                break;
            case "left":
                targetVector = new Vector3(targetVector.x - tileDistance, targetVector.y, targetVector.z);
                rotatorChildren.transform.Rotate(0, 0, 90, Space.World);
                audioPlay();
                break;
        }
    }
    void audioPlay()
    {
    audioData = GetComponent<AudioSource>();
    audioData.Play();
    }
    void Movement()
    {
        if(jump && transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 30, transform.position.z);
            if (transform.position.y > 1) jump = false;
        }
        else
        {
            if(transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
            }
        }

        

        transform.position = Vector3.Lerp(transform.position, targetVector, Time.deltaTime * moveSpeed);
        visualChildren.transform.rotation = Quaternion.Lerp(visualChildren.transform.rotation, rotatorChildren.transform.rotation, Time.deltaTime * moveSpeed);
    }
}
