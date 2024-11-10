using UnityEngine;

public class DeplacementCamp : MonoBehaviour
{
    // Variables de sensibilit� pour la souris et la vitesse de d�placement
    public float mouseSensitivity = 100f;
    public float moveSpeed = 5f;

    // Pour g�rer la rotation de la cam�ra
    private float xRotation = 0f;

    // L'objet repr�sentant le joueur (souvent un GameObject parent de la cam�ra)
    public Transform playerBody;

    void Start()
    {
        // Verrouille le curseur au centre de l'�cran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Gestion de la souris pour la rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limite la rotation verticale � 90�

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);  // Rotation autour de l'axe Y pour la direction

        // Gestion du mouvement avec ZQSD ou les fl�ches directionnelles
        float moveX = Input.GetAxis("Horizontal"); // G�re Q/D ou les fl�ches gauche/droite
        float moveZ = Input.GetAxis("Vertical");   // G�re Z/S ou les fl�ches haut/bas

        Vector3 move = transform.right * moveX + transform.forward * moveZ; // D�placement dans la direction de la cam�ra
        playerBody.position += move * moveSpeed * Time.deltaTime; // Applique le d�placement au joueur
    }
}
