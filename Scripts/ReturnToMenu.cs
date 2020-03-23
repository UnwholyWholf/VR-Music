using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour {

    public KeyCode input = KeyCode.JoystickButton6;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(input)) //Back
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
