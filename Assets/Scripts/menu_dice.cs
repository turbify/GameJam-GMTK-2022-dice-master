using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class menu_dice : MonoBehaviour
{
    AudioSource audioData;
    public int moveSpeed = 5;
    public float tileDistance = 2;
    public bool jump = false;

    public Vector3 targetVector;
    public Vector3 spawnVector;

    public GameObject visualChildren;
    public GameObject rotatorChildren;

    public GameObject sc;
    public screenChanger scc;

    public AudioMixer mix;

  [SerializeField]
  private string sceneName;
     private void Update()
    {
        Movement();

        if (Vector3.Distance(transform.position, targetVector) < 0.2f)
            Controls();
    }

    private void Start()
    {
        visualChildren = this.gameObject.transform.GetChild(0).gameObject;
        rotatorChildren = this.gameObject.transform.GetChild(1).gameObject;

        //sc = GameObject.Find("Screenchanger");
        //scc = sc.GetComponent<screenChanger>();

        spawnVector = targetVector;
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

    void Movement()
    {
        if (jump && transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 30, transform.position.z);
            if (transform.position.y > 1) jump = false;
        }
        else
        {
            if (transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
            }
        }



        transform.position = Vector3.Lerp(transform.position, targetVector, Time.deltaTime * moveSpeed);
        visualChildren.transform.rotation = Quaternion.Lerp(visualChildren.transform.rotation, rotatorChildren.transform.rotation, Time.deltaTime * moveSpeed);
    }

    void audioPlay()
    {
    audioData = GetComponent<AudioSource>();
    audioData.Play();
    }

    string lastMove;
    float volm;
    void Controls()
    {
        if (targetVector != spawnVector)
        {
            if (lastMove == "up") { RollDice("up"); SwitchScene(); }
            if (lastMove == "down") { RollDice("up"); Application.Quit(); }
            if (lastMove == "right") { RollDice("left"); mix.GetFloat("MusicVol", out volm); mix.SetFloat("MusicVol",  volm += 4); }
            if (lastMove == "left") { RollDice("right"); mix.GetFloat("MusicVol", out volm); mix.SetFloat("MusicVol", volm -= 4); }
        }
        else
        {
            bool moved = false;
            if (Input.GetKeyDown("up"))
            {
                RollDice("up");
                lastMove = "up";
                moved = true;
            }
            if (Input.GetKeyDown("down") && !moved)
            {
                RollDice("down");
                lastMove = "down";
                moved = true;
            }
            if (Input.GetKeyDown("right") && !moved)
            {
                RollDice("right");
                lastMove = "right";
                moved = true;
            }
            if (Input.GetKeyDown("left") && !moved)
            {
                RollDice("left");
                lastMove = "left";
                moved = true;
            }
        }
    }

    public void SwitchScene()
{
    SceneManager.LoadScene(sceneName);
}
}
