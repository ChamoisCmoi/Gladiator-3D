using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private bool ombre = true;
    private float deltaTime = 0.0f; // sert dans le calcul des fps
    [SerializeField] TMP_Text textFPS;

    private void Awake()
    {
        QualitySettings.SetQualityLevel(2, true);
        ombre = true;
        StartCoroutine(UpdateFPS());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Tab))
        {
            choixOmbre();
        }*/

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // sert dans le calcul des fps
    }

    private System.Collections.IEnumerator UpdateFPS()
    {
        while (true)
        {
            float fps = Mathf.RoundToInt(1.0f / deltaTime);
            fps = Mathf.Clamp(fps, 20, 60);
            textFPS.text = fps + " fps";
            yield return new WaitForSeconds(1f); // Attendre 1 seconde avant de mettre à jour à nouveau
        }
    }

    public void quitter()
    {
        print("l'application s'est fermée");
        Application.Quit();
    }

    private void choixOmbre()
    {
        if (ombre)
        {
            QualitySettings.SetQualityLevel(0, true);
            ombre = false;
        }
        else
        {
            QualitySettings.SetQualityLevel(2, true);
            ombre = true;
        }
    }
}
