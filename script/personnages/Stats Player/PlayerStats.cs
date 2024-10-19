using UnityEngine;

[CreateAssetMenu(fileName = "player Data", menuName = "player/player Data")]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// la vitesse � laquelle avance le joueur
    /// </summary>
    public float vitesse;
    /// <summary>
    /// la vitese � laquelle recule le joueur
    /// </summary>
    public float vitesseRecul;
    /// <summary>
    /// combien le joueur enl�ve de PV par attaque
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
    /// le pourcentage de chance d'obtenir la meilleur r�compence
    /// </summary>
    [Range(1, 100)] public int chance;

}
