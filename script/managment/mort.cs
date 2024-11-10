using UnityEngine.SceneManagement;
using UnityEngine;

public class mort : MonoBehaviour
{



    private GameObject ATH; // pour avoir le game object du game object contenant le script ATH
    private ATH scriptATH; // stock le script de l'ATH du joueur


    private void Awake()
    {
        ATH = GameObject.Find("ATH");

        if (ATH != null)
        {
            scriptATH = ATH.GetComponent<ATH>();
        }
        else
        {
            Debug.LogWarning("le script ATH n'existe pas");
        }

    }


    void Update()
    {
        if (scriptATH.getSanteAffiche() <= 0)
        {
            PlayerPrefs.SetInt("mort", 1);
            SceneManager.LoadScene(0); // le joueur a perdu :-(
        }
    }
}
