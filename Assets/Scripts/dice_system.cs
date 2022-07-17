using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice_system : MonoBehaviour
{
    public sprite_database sdb;
    private GameObject spritesChildren;
    private move_visuals mvs;

    public board_create board;

    public int[] wallsValues = new int[6];
    private SpriteRenderer[] walls = new SpriteRenderer[6];
    private dice_movement dcm;

    public int dicePos;

    private void Awake()
    {
        dicePos = 5;
    }

    private void Start()
    {
        spritesChildren = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        dcm = GetComponent<dice_movement>();
        mvs = GetComponent<move_visuals>();

        int i = 0;
        foreach (Transform child in spritesChildren.transform)
        {
            walls[i] = child.transform.GetComponent<SpriteRenderer>();
            wallsValues[i] = i + 1;
            i++;
        }

        checkWallValues();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, dcm.targetVector) < 0.5f)
        {
            Controlls();
        }
    }


    public GameObject top_, up_, down_, left_, right_;
    public int topV, upV, downV, leftV, rightV;
    public void checkWallValues()
    {
        foreach (Transform wll in spritesChildren.transform)
        {
            if(top_ != null)
            {
                if(wll.transform.position.y > top_.transform.position.y)
                {
                    top_ = wll.gameObject;
                }
            }
            else { top_ = wll.gameObject; }

            if (up_ != null)
            {
                if (wll.transform.position.z > up_.transform.position.z)
                {
                    up_ = wll.gameObject;
                }
            }
            else { up_ = wll.gameObject; }

            if (down_ != null)
            {
                if (wll.transform.position.z < down_.transform.position.z)
                {
                    down_ = wll.gameObject;
                }
            }
            else { down_ = wll.gameObject; }

            if (right_ != null)
            {
                if (wll.transform.position.x > right_.transform.position.x)
                {
                    right_ = wll.gameObject;
                }
            }
            else { right_ = wll.gameObject; }

            if (left_ != null)
            {
                if (wll.transform.position.x < left_.transform.position.x)
                {
                    left_ = wll.gameObject;
                }
            }
            else { left_ = wll.gameObject; }

            topV = znajdzWartoscSciany(top_);
            upV = znajdzWartoscSciany(up_);
            downV = znajdzWartoscSciany(down_);
            rightV = znajdzWartoscSciany(right_);
            leftV = znajdzWartoscSciany(left_);
        }

        int znajdzWartoscSciany(GameObject wll_)
        {
            int i = 0;
            foreach (Transform wll in spritesChildren.transform)
            {
                if(wll.gameObject == wll_)
                {
                    return wallsValues[i];
                }
                i++;
            }

                return 420;
        }
    }

    private string previousMove;
    private bool attacking = false;

    private void Controlls()
    {
        mvs.updateVisuals();

        if (board.tileType[dicePos-1] == "enemy")
        {
            checkWallValues();

            enemy_system es = board.enemies[dicePos - 1].GetComponent<enemy_system>();

            if (previousMove == "up") 
            {
                if (es.health <= topV)
                {
                    board.killEnemy(dicePos-1);
                }
                else { moveD("down"); }
            }

            if (previousMove == "down")
            {
                if (es.health <= topV)
                {
                    board.killEnemy(dicePos - 1);
                }
                else { moveD("up"); }
            }
            

            if (previousMove == "right")
            {
                if (es.health <= topV)
                {
                    board.killEnemy(dicePos - 1);
                }
                else { moveD("left"); }
            }
            

            if (previousMove == "left")
            {
                if (es.health <= topV)
                {
                    board.killEnemy(dicePos - 1);
                }
                else { moveD("right"); }
            }

            attacking = false;

        }

        bool moveLock = false;

        if (Input.GetKeyDown("up") && !attacking)
        {
            if (dicePos > 3)
            {
                board.countMove((dicePos - 1)-3);
                switch (board.tileType[(dicePos-1)-3])
                {
                    case "blank":
                        moveD("up");
                        break;
                    case "enemy":
                        moveD("up");
                        attacking = true;
                        previousMove = "up";
                        break;
                }
                moveLock = true;
            }
        }
        if (Input.GetKeyDown("down") && !attacking && !moveLock)
        {
            if (dicePos < 7)
            {
                board.countMove((dicePos - 1)+3);
                switch (board.tileType[(dicePos - 1) + 3])
                {
                    case "blank":
                        moveD("down");
                        break;
                    case "enemy":
                        moveD("down");
                        attacking = true;
                        previousMove = "down";
                        break;
                }
                moveLock = true;
            }
        }
        if (Input.GetKeyDown("right") && !attacking && !moveLock)
        {
            if (dicePos != 3 && dicePos != 6 && dicePos != 9)
            {
                board.countMove((dicePos - 1)+1);
                switch (board.tileType[(dicePos - 1) + 1])
                {
                    case "blank":
                        moveD("right");
                        break;
                    case "enemy":
                        moveD("right");
                        attacking = true;
                        previousMove = "right";
                        break;
                }
                moveLock = true;
            }
        }
        if (Input.GetKeyDown("left") && !attacking && !moveLock)
        {
            if (dicePos != 1 && dicePos != 4 && dicePos != 7)
            {
                board.countMove((dicePos - 1)-1);
                switch (board.tileType[(dicePos - 1) - 1])
                {
                    case "blank":
                        moveD("left");
                        break;
                    case "enemy":
                        moveD("left");
                        attacking = true;
                        previousMove = "left";
                        break;
                }
                moveLock = true;
            }
        }

        void moveD(string direction)
        {
            switch (direction)
            {
                case "up":
                    dcm.RollDice("up");
                    dicePos -= 3;
                    break;
                case "down":
                    dcm.RollDice("down");
                    dicePos += 3;
                    break;
                case "right":
                    dcm.RollDice("right");
                    dicePos += 1;
                    break;
                case "left":
                    dcm.RollDice("left");
                    dicePos -= 1;
                    break;
            }
            mvs.hideVisuals();

        }
    }

    void checkTile()
    {

    }
}
