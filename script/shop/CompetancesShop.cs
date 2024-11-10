using UnityEngine;
using TMPro;

public class CompetancesShop : MonoBehaviour
{
    const int nombreCompetances = 5;

    /// <summary>
    /// le panel d'entrainement
    /// </summary>
    [SerializeField] GameObject panelShop;
    /// <summary>
    /// le niveau acuel pour chaque compétance
    /// </summary>
    [SerializeField] int[] niveauCourrant = new int[nombreCompetances];
    /// <summary>
    /// le texte qui affiche chaque prix
    /// </summary>
    [SerializeField] TMP_Text[] textPrix = new TMP_Text[nombreCompetances];
    [SerializeField] TMP_Text[] textCurrentNiv = new TMP_Text[nombreCompetances];

    /// <summary>
    /// correspond aux playerprefs qui référencent les stats des niveaux
    /// </summary>
    [SerializeField] PlayerStats[] playerStats;

    #region console d'erreur
    [SerializeField] GameObject console;
    [SerializeField] TMP_Text txtConsole;
    void PutConsole(string message)
    {
        console.SetActive(true);
        txtConsole.text = message;
    }
    #endregion

    private void Start()
    {
        panelShop.SetActive(false);
        console.SetActive(false);
    }

    public void OpenClosePanel()
    {
        panelShop.SetActive(!panelShop.activeSelf);

        for (int i = 0; i < 5; i++)
        {
            niveauCourrant[i] = GameObject.Find("Canvas").GetComponent<saveData>().GetOneCompetance((competance)i);
        }
        ActualizePanel();
    }
    /// <summary>
    /// pour actualiser le panel d'achat
    /// </summary>
    void ActualizePanel()
    {
        for (int i = 0; i < textPrix.Length; i++) 
        {
            niveauCourrant[i] = GameObject.Find("Canvas").GetComponent<saveData>().GetOneCompetance((competance)i);
            if(niveauCourrant[i]== 0)
            {
                textCurrentNiv[i].text = "Niveau initial";
            }
            else
            {
                textCurrentNiv[i].text = (niveauCourrant[i] + 1).ToString();
            }
            
            if (playerStats.Length > niveauCourrant[i] + 1) // si ce niveau existe
            {
                if (GameObject.Find("Canvas").GetComponent<ManageCapital>().GetExperience() > playerStats[niveauCourrant[i] + 1].XpRequis) // si assez d'xp
                {
                    textPrix[i].text = playerStats[niveauCourrant[i] + 1].coutAmelioration[i].ToString();
                }
                else
                {
                    textPrix[i].text = "il vous manque " + (playerStats[niveauCourrant[i] + 1].XpRequis - GameObject.Find("Canvas").GetComponent<ManageCapital>().GetExperience()).ToString() + " points d'expériences";
                }
            }
            else
            {
                textPrix[i].text = "MAX";
            }
        }
    }
    /// <summary>
    /// est appelé par un button pour améliorer une compétence
    /// </summary>
    /// <param name="competance"></param>
    public void WantLoLevelUp(int competance)
    {
        if(playerStats[niveauCourrant[(int)competance] + 1].XpRequis < GameObject.Find("Canvas").GetComponent<ManageCapital>().GetExperience()) // si assez d'expérience
        {
            if(playerStats[niveauCourrant[(int)competance] + 1].coutAmelioration[(int)competance] <= GameObject.Find("Canvas").GetComponent<ManageCapital>().GetExperience()) // si assez d'argent
            {
                GameObject.Find("Canvas").GetComponent<ManageCapital>().AddCerterces(-playerStats[niveauCourrant[(int)competance] + 1].coutAmelioration[(int)competance]); // on enlève l'argent
                GameObject.Find("Canvas").GetComponent<saveData>().SetOneCompetance((competance)competance, niveauCourrant[(int)competance] + 1); // on met à jour la nouvelle compétence
                ActualizePanel();
            }
            else
            {
                Debug.Log("pas assez d'argent");
                PutConsole("Vous n'avez pas assez de sesterces pour payer notre maitre ! Il ne peut pas vous former pour si peu...");
            }
        }
        else
        {
            Debug.Log("pas assez d'XP");
            PutConsole("Vous n'avez pas assez de points d'expérience pour débloquer la prochaine amélioration, faites plus de niveaux pour débloquer plus !");
        }
    }
}

