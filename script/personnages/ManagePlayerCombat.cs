using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ManagePlayerCombat : MonoBehaviour
{
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

    [Header("Canvas")]
    [SerializeField] Slider IndicRechargementArme;
    // liste des éléments Canvas

    public void Attaque()
    {
        if (canAttaque == true)
        {
            GetComponentInChildren<Animation>().Play("attack");
            StartCoroutine(TempsPlayerReAttaque());
            canAttaque = false; // on ne peut pas attaquer 2 fois de 

            int ptAttaques = (int)GetComponent<deplacementMy>().GetValueCompetance(competance.pointsAttaque); // on appelle myDeplacement, qui centralise les compétances du joueur dans cette scène

            // on va enlever de l'PV aux bot dans le tigger du joueur
            foreach (GameObject bot in enemiesInTrigger)
            {
                if (bot != null) // car le bot peut être mort
                {
                    bot.GetComponentInChildren<bot>().OnBotBeHurt(ptAttaques);
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
        IndicRechargementArme.value = 0;
        float temps = GetComponent<deplacementMy>().GetValueCompetance(competance.vitesse_attaque);
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(AnimateSlider(temps - 0.8f));
        yield return new WaitForSeconds(temps-0.8f);
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
        StartCoroutine(FinApplause());
    }

    IEnumerator FinApplause()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);

        while (GetComponent<AudioSource>().volume > 0)
        {
            GetComponent<AudioSource>().volume -= 0.03f;
            yield return new WaitForSeconds(0.2f);
        }

    }

    private IEnumerator AnimateSlider(float duration)
    {
        float elapsedTime = 0f;
        float startValue = IndicRechargementArme.value; // Valeur de départ (0)
        float endValue = 1f; // Valeur finale (1)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            IndicRechargementArme.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null; // Attendre la prochaine frame
        }

        IndicRechargementArme.value = endValue; // S'assurer que le slider atteint exactement 1
    }
}


