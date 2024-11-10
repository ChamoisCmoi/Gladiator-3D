using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] int numDeLaSceneToCharger;

    public void ChargerScene()
    {
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(numDeLaSceneToCharger);
    }
}
