using UnityEngine;

public class movePerso : MonoBehaviour
{
    [Header("informations de déplacement")]
    [SerializeField] private float vitesse;
    [SerializeField] private int vitesseturn;
    // Sensibilité de la souris
    public float mouseSensitivity = 100;

        [Space]

    [Header("avancer avec la sourie")]
    [SerializeField] private float zoomSpeed = 10f; // vitesse du zoom


    void Update()
    {
        // Gestion du zoom avec la molette de la souris
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            scrollInput *= zoomSpeed;
        }


        if ((Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.Z)) || scrollInput != 0f))
        {
            this.transform.Translate(0, 0, vitesse * Time.deltaTime * (scrollInput + 1));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0, 0, (-vitesse / 2) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(0, (-vitesseturn) * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, (vitesseturn) * Time.deltaTime, 0);
        }


        // Récupère les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Applique la rotation horizontale
        transform.Rotate(0, mouseX, 0);


    }
}
