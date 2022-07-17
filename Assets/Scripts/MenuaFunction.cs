using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuaFunction : MonoBehaviour
{
[SerializeField]
    private GameObject DicePrefab;
    [SerializeField]
    private GameObject BlockPrefab;
        [SerializeField]
    private GameObject PlayPrefab;
        [SerializeField]
    private GameObject ExitPrefab;
        [SerializeField]
    private GameObject TitlePrefab;
           [SerializeField]
    private GameObject volumeUpPrefab;
           [SerializeField]
    private GameObject volumeDownPrefab;
    private Transform[] map = new Transform[5];
    GameObject player;
    dice_system playerSystem;
    // Start is called before the first frame update
    void Awake()
    {
        CreateMenua();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void CreateMenua()
    {
        player = Instantiate(DicePrefab);
        player.transform.position = new Vector3(0,-0.38f,0);
        playerSystem = player.GetComponent<dice_system>();
       // playerSystem.board = this;

        var  beginign = Instantiate(BlockPrefab);
        beginign.transform.localPosition = new Vector3(0,0,0);
        map[0] = beginign.transform;


        var play = Instantiate(BlockPrefab);
        play.transform.localPosition = new Vector3(0,0,1.8f);
        map[1] = play.transform;
        var playSign = Instantiate(PlayPrefab);
        playSign.transform.localPosition = new Vector3(0,0.01f,1.8f);


        var exit = Instantiate(BlockPrefab);
        exit.transform.localPosition = new Vector3(0,0,-1.8f);
        map[2] = exit.transform;
        var exitSign = Instantiate(ExitPrefab);
        exitSign.transform.localPosition = new Vector3(0,0.01f,-1.8f);

        var volumeUp = Instantiate(BlockPrefab);
        volumeUp.transform.localPosition = new Vector3(1.8f,0,0);
        map[3] = volumeUp.transform;
        var evolumeUpSign = Instantiate(volumeUpPrefab);
        evolumeUpSign.transform.localPosition = new Vector3(1.8f,0.01f,0);

        var volumeDown = Instantiate(BlockPrefab);
        volumeDown.transform.localPosition = new Vector3(-1.8f,0,0);
        map[4] = volumeDown.transform;
        var volumeDownSign = Instantiate(volumeDownPrefab);
        volumeDownSign.transform.localPosition = new Vector3(-1.8f,0.01f,0);


        var titleSign = Instantiate(TitlePrefab);
        titleSign.transform.localPosition = new Vector3(-4.5f,0,2.8f);
    }
}
