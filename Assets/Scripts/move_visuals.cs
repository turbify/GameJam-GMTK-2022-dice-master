using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_visuals : MonoBehaviour
{
    public GameObject spritesChildren;
    public SpriteRenderer[] vis = new SpriteRenderer[4];
    private dice_system ds;

    public sprite_database sdb;

    private void Start()
    {
        spritesChildren = this.gameObject.transform.GetChild(2).gameObject;
        ds = this.GetComponent<dice_system>();

        int i = 0;
        foreach (Transform child in spritesChildren.transform)
        {
            vis[i] = child.GetComponent<SpriteRenderer>();
            i++;
        }

        updateVisuals();
    }

    public void updateVisuals()
    {
        ds.checkWallValues();
        int dpos = ds.dicePos;

        vis[0].sprite = sdb.gfx[ds.downV - 1];
        vis[1].sprite = sdb.gfx[ds.upV - 1];
        vis[2].sprite = sdb.gfx[ds.leftV - 1];
        vis[3].sprite = sdb.gfx[ds.rightV - 1];

        foreach(SpriteRenderer ss in vis)
        {
            ss.enabled = true;
        }

        if (dpos < 4) { vis[0].enabled = false; }
        if (dpos > 6) { vis[1].enabled = false; }
        if (dpos == 3 || dpos == 6 || dpos == 9) { vis[2].enabled = false; }
        if (dpos == 1 || dpos == 4 || dpos == 7) { vis[3].enabled = false; }
    }

    public void hideVisuals()
    {
        foreach (SpriteRenderer ss in vis)
        {
            ss.enabled = false;
        }
    }
}
