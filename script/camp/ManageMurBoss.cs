using UnityEngine;

public class ManageMurBoss : MonoBehaviour
{

    [SerializeField] EtatAffrontementBoss[] etatAffrontementBoss;


    [SerializeField] Material[] imagesBoss;

    [SerializeField] GameObject[] panelLancementEnsembleNiveau;

    private void Start()
    {
        if (etatAffrontementBoss == null)
        {
            etatAffrontementBoss = new EtatAffrontementBoss[imagesBoss.Length];
        }

        // on met toutes les images des boss en sombre
        int i = 0;
        foreach (Material images in imagesBoss)
        {
            Color color;

            switch (etatAffrontementBoss[i])
            {
                case EtatAffrontementBoss.nonDebloque:
                    if (ColorUtility.TryParseHtmlString("#5C5C5C", out color))
                    {
                        images.color = color;
                    }
                    else
                    {
                        Debug.LogWarning("la couleur n'a pas pu être changée");
                    }
                    break;
                case EtatAffrontementBoss.debloque:
                    if (ColorUtility.TryParseHtmlString("#FFFFFF", out color))
                    {
                        images.color = color;
                    }
                    else
                    {
                        Debug.LogWarning("la couleur n'a pas pu être changée");
                    }
                    break;
                case EtatAffrontementBoss.battu:
                    if (ColorUtility.TryParseHtmlString("#CB3D3D", out color))
                    {
                        images.color = color;
                    }
                    else
                    {
                        Debug.LogWarning("la couleur n'a pas pu être changée");
                    }
                    break;
                default:
                    Debug.LogError("valeur non attendue");
                    break;
            }

            i++;

        }
    }

    /// <summary>
    /// est appelé quand le joueur touche une affiche d'un boss
    /// </summary>
    /// <param name="inImage"></param>
    public void OnPlayerTouchAffiche(int idImage)
    {
        print(idImage);

        panelLancementEnsembleNiveau[idImage].SetActive(true);
    }

    /// <summary>
    /// pour connaitre la variable des états des boss, surtout en vue de la sauvegarde
    /// </summary>
    /// <returns></returns>
    public EtatAffrontementBoss[] GetEtatAffrontementBoss()
    {
        return etatAffrontementBoss;
    }

    /// <summary>
    /// est appelé quand le joueur clique sur un niveau pour le lancer
    /// </summary>
    /// <param name="numNiv"></param>
    public void OnLancerNiveau(int numNiv)
    {
        PlayerPrefs.SetInt("numNivLance", numNiv);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }


}

public enum EtatAffrontementBoss
{
    nonDebloque,
    debloque,
    battu
}
