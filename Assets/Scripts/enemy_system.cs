using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class enemy_system : MonoBehaviour
{
    public int health;
    public GameObject spritesChildren;
    public GameObject visualChildren;
    public GameObject[] sprites = new GameObject[6];

    public bool isOnMiddle = false;


    private void Awake()
    {
        visualChildren = this.gameObject.transform.GetChild(0).gameObject;
        spritesChildren = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    private void Start()
    {
        if (!isOnMiddle)
        {
            health = Random.Range(1, 7);
        }
        else
        {
            health = Random.Range(1, 5);
        }

        findwall(health);
    }

    public void death()
    {
         Destroy(this.gameObject);
    }

    GameObject top_;
    void findwall(int val)
    {
        int i = 0;
        foreach (Transform child in spritesChildren.transform)
        {
            sprites[i] = child.gameObject;
            i++;
        }

        while (top_ != sprites[val - 1])
        {
            visualChildren.transform.Rotate(90, 0, 0, Space.World);
            checkTopWall();

            if (top_ != sprites[val - 1])
            {
                visualChildren.transform.Rotate(0, 0, 90, Space.World);
                checkTopWall();
            } else { break; }
        }

        void checkTopWall()
        {
            foreach (Transform wll in spritesChildren.transform)
            {
                if (top_ != null)
                {
                    if (wll.transform.position.y > top_.transform.position.y)
                    {
                        top_ = wll.gameObject;
                    }
                }
                else { top_ = wll.gameObject; }
            }
        }
    }
}

