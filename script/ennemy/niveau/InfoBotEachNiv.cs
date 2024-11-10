using UnityEngine;

[CreateAssetMenu(fileName = "niv Data", menuName = "nav components/niv Data")]
public class InfoBotEachNiv : ScriptableObject
{
    /// <summary>
    /// l'argent que le joueur gagne la premi�re fois qu'il r�ussit le niveau (le raid)
    /// </summary>
    public int gainPrincipal;
    /// <summary>
    /// l'argent que le joueur gagne quand il refait le niveau (le raid)
    /// </summary>
    public int gainSecondaire;
    /// <summary>
    /// indique le nombre de vagues dans le niveau;
    /// </summary>
    public int nombreDeVagues;
    /// <summary>
    /// les donn�es concernant chaque vague
    /// </summary>
    public Vague[] vagues;


}

[System.Serializable]
public class Vague
{
    /// <summary>
    /// le nombre d'xp gagn� pour cette vague
    /// </summary>
    public int XpParVague;
    /// <summary>
    /// le temps entre chauqe apparition de monstre dans la vague
    /// </summary>
    [Range(0.1f, 3)] public float delaisApparitionEntreChaqueMonstre;

    [Space(10)]
    /// <summary>
    /// les monstres pr�sents dans cette vague
    /// </summary>
    public nombreMonstre[] monstresPresents;

}

[System.Serializable]
public class nombreMonstre
{
    [Space()]
    public TypeMonstre monstre;
    public int combienDeCeMonstre;
}