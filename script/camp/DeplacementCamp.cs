using UnityEngine;

public class DeplacementCamp : MonoBehaviour
{
    // Variables de sensibilité pour la souris et la vitesse de déplacement
    public float mouseSensitivity = 100f;
    public float moveSpeed = 5f;

    // Pour gérer la rotation de la caméra
    private float xRotation = 0f;

    // L'objet représentant le joueur (souvent un GameObject parent de la caméra)
    public Transform playerBody;

    void Start()
    {
        // Verrouille le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Gestion de la souris pour la rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limite la rotation verticale à 90°

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);  // Rotation autour de l'axe Y pour la direction

        // Gestion du mouvement avec ZQSD ou les flèches directionnelles
        float moveX = Input.GetAxis("Horizontal"); // Gère Q/D ou les flèches gauche/droite
        float moveZ = Input.GetAxis("Vertical");   // Gère Z/S ou les flèches haut/bas

        Vector3 move = transform.right * moveX + transform.forward * moveZ; // Déplacement dans la direction de la caméra
        playerBody.position += move * moveSpeed * Time.deltaTime; // Applique le déplacement au joueur
    }
}
