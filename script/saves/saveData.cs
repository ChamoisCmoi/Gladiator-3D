using UnityEngine;

public class saveData : MonoBehaviour
{
    public const int nombreDeNiveaux = 20;

    /// <summary>
    /// correspond à si les monstres sont encore en vie
    /// </summary>
    [SerializeField] bool[] niveauReussi = new bool[nombreDeNiveaux];
    /// <summary>
    /// correspond au niveau auquel est chaque competance      :
    /// 0 = pts attaques, 
    /// 1 = PV, 
    /// 2 = vitesse(marche),  
    /// 3 = vitesse (temps ReAttack),  
    /// 4 = chance 
    /// </summary>
    [SerializeField] int[] armes = new int[5]; //0 = pts attaques, 1 = PV, 2 = vitesse(marche), 3 = vitesse (attaque), 4 = chance

    private void Awake()
    {
        // on récupère les données enregistrées
        for (int i = 0; i < niveauReussi.Length; i++)
        {
            if (PlayerPrefs.HasKey("niveau" + (i + 1).ToString()))
            {
                if (PlayerPrefs.GetInt("niveau" + (i + 1).ToString()) == 0)
                {
                    niveauReussi[i] = false;
                }
                else
                {
                    niveauReussi[i] = true;
                }
            }
        }

        for (int i = 0; i < armes.Length; i++)
        {
            if (PlayerPrefs.HasKey("armes" + i.ToString()))
            {
                armes[i] = PlayerPrefs.GetInt("armes" + i.ToString(),1);
            }
        }
    }
    /// <summary>
    /// est appelé pour sauvegarder des données
    /// </summary>
    public void Save()
    {
        // on sauvegarde les données des niveaux
        for (int i = 0; i < niveauReussi.Length; i++)
        {
            int value;
            if (niveauReussi[i])
            {
                value = 1; // niveau réussi
            }
            else
            {
                value = 0; // niveau perdu
            }
            PlayerPrefs.SetInt("niveau" + i.ToString(), value);
        }
        // on sauvegarde les données des armes courrantes
        for (int i = 0; i < armes.Length; i++)
        {
            PlayerPrefs.SetInt("armes" + i.ToString(), armes[i]);
        }

        // pour que cela enregistre dans le navigateur
        PlayerPrefs.Save();
    }
    /// <summary>
    /// est appelé pour connaitre le niveau d'une compétance
    /// </summary>
    /// <param name="competance"></param>
    /// <returns></returns>
    public int GetOneCompetance(competance competance)
    {
        return armes[(int)competance];
    }

    public void SetOneCompetance(competance competance,int newLevel)
    {
        if (newLevel == armes[(int)competance] + 1)
        {
            armes[(int)competance] = newLevel;
        }
        else
        {
            Debug.LogWarning("le niveau a augmenter de plus que 1");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < armes.Length; i++)
            {
                armes[i]++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < armes.Length; i++)
            {
                armes[i]--;
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}

public enum competance
{
    pointsAttaque,
    PV,
    vitesse_marche,
    vitesse_attaque,
    chance
}
