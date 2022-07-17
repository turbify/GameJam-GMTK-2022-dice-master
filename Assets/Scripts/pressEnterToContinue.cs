using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pressEnterToContinue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
     private string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SwitchScene();
        }
    }
        public void SwitchScene()
{
    SceneManager.LoadScene(sceneName);
}
}
