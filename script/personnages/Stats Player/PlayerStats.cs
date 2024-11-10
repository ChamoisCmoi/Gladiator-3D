using UnityEngine;

[CreateAssetMenu(fileName = "player Data", menuName = "player/player Data")]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// la vitesse à laquelle avance le joueur
    /// </summary>
    public float vitesse;
    /// <summary>
    /// la vitese à laquelle recule le joueur
    /// </summary>
    public float vitesseRecul;
    /// <summary>
    /// combien le joueur enlève de PV par attaque
    /// </summary>
    public int pointsAttaques;
    /// <summary>
    /// le nombre de points de vie
    /// </summary>
    [Range(10,1000)]public int PV;
    /// <summary>
    /// nombre de secondes entre chaque attaque
    /// </summary>
    public float tempsReAttaque;
    /// <summary>
    /// le pourcentage de chance d'obtenir la meilleur récompence
    /// </summary>
    [Range(1, 100)] public int chance;
    /// <summary>
    /// combien coute l'amélioration pour obtenir chaque equipement
    /// </summary>
    public int[] coutAmelioration = new int[5];
    /// <summary>
    /// le nombre d'xp requis pour avoir débloquer ce niveau
    /// </summary>
    public int XpRequis;

}
