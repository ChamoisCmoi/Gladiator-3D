using UnityEngine;
using TMPro;
public class UI : MonoBehaviour
{

    private bool ombre = true;


    float deltaTime = 0.0f; // sert dans le calcul des fps
    [SerializeField] TMP_Text textFPS;

    private void Awake()
    {
        QualitySettings.SetQualityLevel(2, true);
        ombre = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            quitter();
        }*/

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            choixOmbre();
        }

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // sert dans le calcul des fps
    }

    public void quitter()
    {
        print("l'application s'est fermée");
        Application.Quit();
    }

    private void choixOmbre()
    {
        if (ombre == true)
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

    void OnGUI() // sert dans le calcul des fps
    {
        float fps = Mathf.RoundToInt(1.0f / deltaTime);
        textFPS.text = fps + " fps";
    }
}
