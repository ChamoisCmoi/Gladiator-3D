using UnityEngine;
using TMPro;

public class FinNiveau : MonoBehaviour
{

    [SerializeField] GameObject PanelFin;

    [Header("chance")]
    [SerializeField] TMP_Text pourcentageChance;
    [SerializeField] TMP_Text textReussi;

    [Header("pieces")]
    [SerializeField] TMP_Text piecesObtenues;

    [Header("experience")]
    [SerializeField] TMP_Text xpObtenus;

    /// <summary>
    /// est appelé pour gérer la fin du niveau
    /// </summary>
    /// <param name="piecesGagnee"></param>
    /// <param name="xpGagnes"></param>
    public void FinDuNiveau(int piecesGagnee, int xpGagnes)
    {
        if (PanelFin.activeSelf)
        {
            bool gainDouble = GainDouble();
            // affichage si gain double obtenu
            if (gainDouble)
            {
                textReussi.text = "obtenu";
            }
            else
            {
                textReussi.text = "non obtenu";
            }

            piecesObtenues.text = piecesGagnee.ToString();
            xpObtenus.text = xpGagnes.ToString();

            PlayerPrefs.SetInt("certerces",PlayerPrefs.GetInt("certerces") + piecesGagnee);
            PlayerPrefs.SetInt("experience", PlayerPrefs.GetInt("experience") + xpGagnes);
        }
        else
        {
            Debug.LogError("<color=red>La fonction pour afficher le panel est appelé, mais le panel n'est pas ouverts !</color>");
        }
    }

    /// <summary>
    /// est appelé uniquement par FinDuNiveau()
    /// </summary>
    /// <returns></returns>
    bool GainDouble()
    {
        float chance = (GameObject.Find("personnage Variant").GetComponent<deplacementMy>().GetValueCompetance(competance.chance) / 100);
        pourcentageChance.text = (chance*100).ToString() + "%"; // affichage de la chance

        System.Random random = new System.Random();
        float randomNumber = (float)random.NextDouble(); // Génère un nombre entre 0.0 et 1.0

        if (randomNumber < chance) // si le nombre tiré au hasard est inférieur au pourcentage de chnace du joueur
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
