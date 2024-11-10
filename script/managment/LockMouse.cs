using UnityEngine;

public class LockMouse : MonoBehaviour
{
    public float doubleClickTime = 0.3f; // Temps maximum entre deux clics pour être considéré comme un double clic
    private float lastClickTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Vérifie si le bouton gauche de la souris est cliqué
        {
            if (Time.time - lastClickTime < doubleClickTime)
            {
                OnDoubleClick(); // Appelle la fonction de double clic
            }
            lastClickTime = Time.time; // Met à jour le temps du dernier clic
        }

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
        {
             Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnDoubleClick()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
