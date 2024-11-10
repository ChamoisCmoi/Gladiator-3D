using UnityEngine;

public class Quit : MonoBehaviour
{

    ////////////////////////// ceci est utilis� dans plusieurs sc�nes ////////////////////////////////

    public void Quitter()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetButtonDown("fullsceen"))
        {
            Screen.fullScreen = Screen.fullScreen ? Screen.fullScreen = true : Screen.fullScreen = false;
            print(Screen.fullScreen);
        }
    }

    private void Start()
    {
        Screen.fullScreen = true;
    }

}
