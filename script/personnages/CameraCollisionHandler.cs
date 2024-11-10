using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    public Transform target; // Le joueur ou le personnage cible que la caméra doit suivre
    public float minDistance = 2.0f; // Distance minimale entre la caméra et le personnage
    public float maxDistance = 6.0f; // Distance maximale entre la caméra et le personnage
    public float zoomSpeed = 10.0f; // Vitesse de zoom lorsqu'il y a un obstacle
    public LayerMask obstacleMask; // Les couches que la caméra doit éviter (ex: murs, objets statiques)

    private Vector3 defaultOffset; // Offset initial entre la caméra et le personnage
    private float currentDistance; // Distance actuelle de la caméra au personnage

    void Start()
    {
        // Calculer l'offset par défaut entre la caméra et le personnage
        defaultOffset = transform.position - target.position;
        currentDistance = defaultOffset.magnitude; // Distance initiale de la caméra
    }

    void LateUpdate()
    {
        // Calculer la position cible de la caméra (avant ajustement des collisions)
        Vector3 desiredCameraPos = target.position + defaultOffset.normalized * currentDistance;

        // Faire un raycast depuis le personnage vers la position désirée de la caméra
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredCameraPos, out hit, obstacleMask))
        {
            // Si un mur ou un obstacle est détecté (mais PAS un ennemi)
            if (!hit.collider.CompareTag("Enemy"))
            {
                // Rapprocher la caméra si un obstacle est trouvé (sans tenir compte des ennemis)
                currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }
        }
        else
        {
            // Si aucun obstacle n'est détecté, retourner à la distance par défaut
            currentDistance = Mathf.Lerp(currentDistance, maxDistance, Time.deltaTime * zoomSpeed);
        }

        // Appliquer la nouvelle position à la caméra
        transform.position = target.position + defaultOffset.normalized * currentDistance;

        // Toujours regarder vers le personnage
        transform.LookAt(target);
    }
}
