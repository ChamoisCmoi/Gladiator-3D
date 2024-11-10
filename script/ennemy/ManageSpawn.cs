using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ManageSpawn : MonoBehaviour
{
    [SerializeField] ennemy[] ennemys;

    [SerializeField] Transform spawPoint;

    List<GameObject> botsEnJeu = new List<GameObject>();

    /// <summary>
    /// les données pour chauque niveau
    /// </summary>
    [SerializeField] InfoBotEachNiv[] nivData;

    /// <summary>
    /// correspond au niveau joué
    /// </summary>
    int currentNiv;
    [Range(1,3)] int vagueEnCours = 1;

    [Space(20)]
    [Header("panel gagne")]
    [SerializeField] GameObject panelGagne;

    /// <summary>
    /// est appelé pour faire apparaitre un bot
    /// </summary>
    public void SpawEnnemy(int num)
    {
       GameObject bot = Instantiate(ennemys[num].modele3D,spawPoint);
        botsEnJeu.Add(bot);
    }

    private void Start()
    {
        panelGagne.SetActive(false);

        if (PlayerPrefs.HasKey("numNivLance"))
        {
            currentNiv = PlayerPrefs.GetInt("numNivLance");
            StartCoroutine(LancementBotForUneVague(0)); // on lance les bots
        }
        else
        {
            Debug.LogWarning("aucun viveau ne peut être lancé, donc lancement par defaut");
            SpawEnnemy(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SpawEnnemy(0);
        }
    }

    /// <summary>
    /// est appelé par le bot quand il meurt
    /// </summary>
    public void DelateBotFromTheList(GameObject bot)
    {
        botsEnJeu.Remove(bot);

        if(botsEnJeu.Count == 0)
        {
            vagueEnCours++;
            if (vagueEnCours-1 == nivData[currentNiv-1].nombreDeVagues)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerCombat>().OnBotGain(); // car il ne reste plus de bots en jeu

                //print("<color=red>fin du niveau</color>");
                AffichePanelGagne();
            }// le niveau est terminé
            else
            {
                LancementBotForUneVague(vagueEnCours); // on lance la vague suivante
            }
        }
    }
    /// <summary>
    /// est appelé pour lancer les bots de la vague en cours
    /// </summary>
    /// <returns></returns>
    IEnumerator LancementBotForUneVague(int tempsAttenteAvantProchaineVague)
    {
        yield return new WaitForSeconds(tempsAttenteAvantProchaineVague);

        List<int> botToLauch = new List<int>();

        for (int i = 0; i < nivData[currentNiv-1].vagues.Length ; i++) // pour chaque type de monstre
        {
            for (int j = 0; j < nivData[currentNiv-1].vagues[vagueEnCours-1].monstresPresents[i].combienDeCeMonstre; j++) // pour chaque monstre de type i
            {
                botToLauch.Add(((int)nivData[currentNiv-1].vagues[vagueEnCours-1].monstresPresents[i].monstre));
            }
        }

        // on les lance dans un ordre aléatoire
        for (int i = 0; i < botToLauch.Count; i++)
        {
            System.Random random = new System.Random();
            SpawEnnemy(botToLauch[random.Next(0, botToLauch.Count)]);
            yield return new WaitForSeconds(nivData[currentNiv - 1].vagues[vagueEnCours - 1].delaisApparitionEntreChaqueMonstre); // on attend avant de lancer un autre bot
        }
    }

    /// <summary>
    /// est appelé à la fin du niveau si le joueur gagne
    /// </summary>
    void AffichePanelGagne()
    {
        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        //////////////////////////////////////////////////////////////////////

        panelGagne.SetActive(true); // on allume le panel de fin du nivau

        int xpTotal = 0;
        if (PlayerPrefs.GetInt("niveau" + currentNiv) == 0) // si le niveau n'a jamais été réussi
        {
            for (int i = 0; i < nivData[currentNiv - 1].vagues.Length; i++)
            {
                xpTotal += nivData[currentNiv - 1].vagues[i].XpParVague;
            }
        }
        else
        {
            print("<color=green>niveau déjà reussi</color>");
        }

        int pieces = 0;
        if (PlayerPrefs.GetInt("niveau" + currentNiv) == 0) // niveau non
        {
            pieces = nivData[currentNiv - 1].gainPrincipal;
        }
        else if (PlayerPrefs.GetInt("niveau" + currentNiv) == 1) // niveau déjà réussi
        {
            pieces = nivData[currentNiv - 1].gainSecondaire;
        }
        else
        {
            Debug.LogWarning("valeur non attendue");
        }

        GetComponent<FinNiveau>().FinDuNiveau(pieces, xpTotal);

        PlayerPrefs.SetInt("niveau" + PlayerPrefs.GetInt("numNivLance"), 1); // on dit que le niveau a été réussi
    }
}
