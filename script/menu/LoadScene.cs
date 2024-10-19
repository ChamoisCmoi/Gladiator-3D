using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] int numDeLaSceneToCharger;

    public void ChargerScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(numDeLaSceneToCharger);
    }
}
