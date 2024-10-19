using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagePlayerCombat : MonoBehaviour
{
    [SerializeField] Arme[] armes;
    [SerializeField] int IndexArmeUtilise;

    /// <summary>
    /// indique si le joueur a en ce moment le droit d'attaquer
    /// </summary>
    private bool canAttaque = true;
    /// <summary>
    /// au bout de combien de temps le joueur peut réattaquer
    /// </summary>
    [SerializeField] int secondeUntilPlayerReAttaque;


    // Liste pour stocker les ennemis dans le trigger
    private List<GameObject> enemiesInTrigger = new List<GameObject>();

    public void Attaque()
    {
        if (canAttaque == true)
        {
            GetComponentInChildren<Animation>().Play("attack");
            StartCoroutine(TempsPlayerReAttaque());
            canAttaque = false; // on ne peut pas attaquer 2 fois de 

            // on va enlever de l'PV aux bot dans le tigger du joueur
            foreach (GameObject bot in enemiesInTrigger)
            {
                if (bot != null) // car le bot peut être mort
                {
                    bot.GetComponentInChildren<bot>().OnBotBeHurt(armes[IndexArmeUtilise].puissance);
                }
            }
        }
        else
        {
            Debug.Log("On ne peut pas attaquer");
        }
        

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attaque();
        }
    }


    IEnumerator TempsPlayerReAttaque()
    {
        yield return new WaitForSeconds(secondeUntilPlayerReAttaque);
        canAttaque = true;
    }


    // Lorsque quelque chose entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet a le tag "Enemy"
        if (other.CompareTag("ennemy"))
        {
            // Ajoute l'ennemi à la liste
            enemiesInTrigger.Add(other.gameObject);
        }
    }

    // Lorsque quelque chose sort du trigger
    private void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet a le tag "Enemy"
        if (other.CompareTag("ennemy"))
        {
            // Retire l'ennemi de la liste
            enemiesInTrigger.Remove(other.gameObject);
        }
    }

    // Retourne la liste des ennemis dans le trigger
    public GameObject[] GetEnemiesInTrigger()
    {
        return enemiesInTrigger.ToArray();
    }

    public void OnBotGain()
    {
        GetComponentInChildren<Animation>().Play("victory"); 
    }
}

[System.Serializable]
/// <summary>
/// défini l'arme actuellement utilisée par le joueur
/// </summary>
public class Arme
{
    public string nom;
    public int puissance;
}


