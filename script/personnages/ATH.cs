using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ATH : MonoBehaviour
{
    [SerializeField] private TMP_Text affichage_pourcent; 
    [SerializeField] private bool canChange;
    [SerializeField] private Slider sliderATH;


    [SerializeField] [Range(0, 100)] private int sante; // la sante, hors affichage
    [Range(-100, 100)] private float afficheSante; // la sante affich�e
    void Awake()
    {
        canChange = true;
        afficheSante = 100;
        sante = 100;
        actualiseAffichage();
    }


    void Update()
    {

        // pour la b�ta

        if (Input.GetKeyDown(KeyCode.O))
        {
            if(sante >= 10)
            {
                sante -= 10;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (sante <= 90)
            {
                sante += 10;
            }
        }


        ////////////////////////////////////////////////////////////


        if ((int)afficheSante != sante) // pour changer la sant� affich�e
        {

            if (afficheSante > sante) // pour baisser la sant�
            {
                if (canChange == true)
                {
                    StartCoroutine(changeSanteAttente(-1));
                }
            }
            if (afficheSante < sante) // pour augmenter la sant�
            {
                if (canChange == true)
                {
                    StartCoroutine(changeSanteAttente(1));
                }
            }
        }
    }

    private IEnumerator changeSanteAttente(int signe)
    {
        canChange = false;
        yield return new WaitForSeconds(0);
        afficheSante += signe*0.2f;
        canChange = true;

        actualiseAffichage();
    }

    private void actualiseAffichage() // pour actualiser l'affichage text et grahique(slider) pour la sant� 
    {
        sliderATH.value = afficheSante; // on actualise l'affichage graphique
        affichage_pourcent.text = ((int)afficheSante+ "%"); // on actualise l'affiche text
    }

    public void changeSante(int nombre) // pour que les autres scripts puisse changer la valeur de la sant�
    {
        if (sante <= 100 || sante >= 0)
        {
            sante += nombre;
        }
        else
        {
            Debug.LogWarning("limite de sant� atteint");
        }
        
    }
    public float getSanteAffiche() // pour que les autres scripts puisse avoir acc�s � la valeur de la sant�
    {
        return afficheSante;
    }
}
