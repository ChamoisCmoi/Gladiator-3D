using UnityEngine.SceneManagement;
using UnityEngine;

public class startMenu : MonoBehaviour
{

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (Input.GetKeyDown(KeyCode.S)) && (Input.GetKeyDown(KeyCode.KeypadEnter)) && (Input.GetKeyDown(KeyCode.Return)))
        {

            loadScene();

        }

        if ((Input.GetKeyDown(KeyCode.Q)))
        {
            quit();
        }
    }

    public void loadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        print("l'application quitte");
    }


}
