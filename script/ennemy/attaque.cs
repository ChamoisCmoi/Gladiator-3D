using UnityEngine;
using System.Collections;
public class attaque : MonoBehaviour
{

    [Header("�l�ments � remplir")]
    public int numBot; // le num�ro donn� � ce bot, ne va pas forc�ment servir (gadjet)

    [Space]

    [Header("�l�ments � ratacher")]
     

    [Header("attaque")]
    private float tempsReAttac; // le temps au bout du quel le bot va repouvoir attaquer
    private float tempsBotInTriggerPlayerToAttack; // le temps o� le bot doit �tre dans le trigger du joueur pour pouvoir l'attaquer
    private int pointsAttaques; // la force d'attaque de l'ennemy



    /////////////////////////////////////////////////////////////////// en private //////////////////////////////////////////////////////
    private bool InTriggerPlayer = false; // pour savoir si le joueur se trouver dans le trigger du joueur

    private GameObject ATH; // pour avoir le game object du game object contenant le script ATH
    private ATH scriptATH; // stock le script de l'ATH du jouer

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

        ennemy data = GetComponent<bot>().GetEnnemyData();
        tempsReAttac = data.tempsReAttac;
        tempsBotInTriggerPlayerToAttack = data.tempsBotInTriggerPlayerToAttack;
        pointsAttaques = data.pointsAttaques;


    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider repere)
    {
        if (repere.tag == "Player")
        {
            StartCoroutine(pouvoirAttac(tempsReAttac,true));
            InTriggerPlayer = true;
        }
    }

    private void OnTriggerExit(Collider repere)
    {
        if (repere.tag == "Player")
        {
            InTriggerPlayer = false;
        }
    }
    IEnumerator pouvoirAttac(float temps,bool firstTime) 
    {
        if (firstTime)
        {
            yield return new WaitForSeconds(tempsBotInTriggerPlayerToAttack); // attend pour la premi�re fois
        }

        if (InTriggerPlayer)
        {
            GetComponent<bot>().BotAttackPlayer();
            scriptATH.changeSante(-pointsAttaques); // on met moins car on enl�ve de la vie 
        }

        yield return new WaitForSeconds(temps);

        if (InTriggerPlayer == true)
        {
            StartCoroutine(pouvoirAttac(tempsReAttac,false));
        }
    }
}
