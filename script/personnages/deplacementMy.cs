using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class deplacementMy : MonoBehaviour
{
    [Header("informations de déplacement")]
    [SerializeField] private float vitesse; // la vitesse du joueur
    [SerializeField] private float vitesseRecul; // la vitesse du joueur qui recule
    [SerializeField] private int vitesseRotation; // vitesse de rotation du joueur
    [SerializeField] private float hauteurJump; // la vitesse du joueur
    //private float xRotation = 0f; // Rotation verticale (haut/bas)

    [Space]

    [Header("objets de référence pour les raycasts")]
    [SerializeField] [Range(1,5)] private float modifLenghRaycast = 1; 
    [SerializeField] private GameObject raycatDevant;
    [SerializeField] private GameObject raycatSauter;
    [SerializeField] private GameObject raycatSol;

    [Space]

    [Header("scripts à ratacher")]
    [SerializeField] private ajusteCamera cameras;
    [SerializeField] private Slider sensibilityRotation;


    private Animation persoAnimate; // les animations du personnage

    private bool isJump = false; // savoir si le joueur est en train de sauter, sert pour ne pas que le joueur saute en permanance
    private bool canJump = true; // si le joueur peut sauter, sert pour savoir si il y a assez de hauteur pour que le joueur puisse sauter
    private bool canMarcher = true; // si le joueur a le droit d'avancer

    public RaycastHit hit; // variable de stokage du raycast

    [Header ("compétances par niveau")]
    // pour le changement des compétances par niveau

    [Range(1,20)][SerializeField] private int currentNiveauVitesse;
    [Range(1, 20)] [SerializeField] private int currentNiveauVitesseRecul;
    [Range(1, 20)] [SerializeField] private int currentNiveauPointsAttaques;
    [Range(1, 20)] [SerializeField] private int currentNiveauPV;
    [Range(1, 20)] [SerializeField] private int currentNiveauTempsReAttaque;
    [Range(1, 20)] [SerializeField] private int currentNiveauChance;

    [SerializeField] private PlayerStats[] competanceParNiveau;

    private void Awake()
    {
        persoAnimate = GetComponent<Animation>();

        // on récupère la vitesse de rotation favorite du joueur
        if (PlayerPrefs.HasKey("senivityRotation"))
        {
            vitesseRotation = (int)PlayerPrefs.GetInt("senivityRotation");
            sensibilityRotation.value = vitesseRotation;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(raycatDevant.transform.position, transform.forward * (vitesse / 8) * modifLenghRaycast, Color.red);
        Debug.DrawRay(raycatSauter.transform.position, transform.up * hauteurJump*3* modifLenghRaycast, Color.blue);
        Debug.DrawRay(raycatSol.transform.position, transform.up *(-1)*0.3f* modifLenghRaycast, Color.cyan);

        if (Physics.Raycast(raycatDevant.transform.position, transform.forward, out hit, vitesse/8)) // si le joueur peut avancer
        {
            canMarcher = false;
        }
        else
        {
            canMarcher = true;
        }

        if (Physics.Raycast(raycatSauter.transform.position, transform.up, out hit, hauteurJump*3* modifLenghRaycast)) // si le joueur peut sauter
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z)) && canMarcher) // si il avance
        {
            transform.Translate (0,0,vitesse * Time.deltaTime);
            if (Physics.Raycast(raycatSol.transform.position, transform.up * (-1), out hit, 0.3f* modifLenghRaycast)) // si le joueur touche le sol, alors l'animation de "marcher" se joue
            {
                //print("touche sol");
                persoAnimate.Play("walk");
            }
        }


        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))) // si il recule
        {
            this.transform.Translate(0,0,-vitesseRecul * Time.deltaTime);
            persoAnimate.Play("walk");
        }
        if (Input.GetKey(KeyCode.None)) /// si il ne fait rien
        {
            persoAnimate.Play("idle");
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump && canMarcher)
        {
            if ((isJump == false) && Physics.Raycast(raycatSol.transform.position, transform.up*(-1), out hit, 0.3f* modifLenghRaycast))
            {
                StartCoroutine(jump(3));
                isJump = true;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))) // si il tourne à gauche
        {
            this.transform.Rotate(0, -vitesseRotation*Time.deltaTime/5, 0);
            this.transform.Translate(0, 0, vitesse/3 * Time.deltaTime);
            persoAnimate.Play("walk");
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))) // si il tourne à droite
        {
            this.transform.Rotate(0, vitesseRotation*Time.deltaTime/5, 0);
            this.transform.Translate(0, 0, vitesse / 3 * Time.deltaTime);
            persoAnimate.Play("walk");
        }

        // pour se déplacer à gauche au droite

        // Récupération de l'entrée utilisateur (flèches gauche/droite ou touches Q/D)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // On crée un vecteur de déplacement sur l'axe X
        Vector3 movement = new Vector3(horizontalInput, 0, 0);

        // On déplace l'objet avec la vitesse spécifiée et le deltaTime pour être indépendant du framerate
        transform.Translate(movement * vitesse * Time.deltaTime/5);
        if(movement.x > 0.5f)
        {
            persoAnimate.Play("walk");
        }


        // les mouvements de la sourie

        // Récupère les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * vitesseRotation * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * vitesseRotation * Time.deltaTime;

        // Rotation vers le haut et le bas (sur l'axe X)
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite la rotation verticale (haut/bas)

        // Applique la rotation verticale au joueur (caméra ou personnage)
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotation horizontale (gauche/droite) autour de l'axe Y
        transform.Rotate(Vector3.up * mouseX);
        /////////////////////////////////////////////////////////////////////////////////////////////////////



    }

    IEnumerator jump(int hauteur)
    {
        // on change la camera (on met la principale)
        int nbCourrant = (int)cameras.camEnCours;
        cameras.cameras[nbCourrant].SetActive(false);
        cameras.cameras[0].SetActive(true);

        // on saute
        for (int i = 0; i <= hauteur; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.Translate(0, hauteurJump, 0);

            if (!Input.GetKey(KeyCode.UpArrow)) // si le perso ne marche pas, on met l'animation pour qu'il saute
            {
                persoAnimate.Play("charge");
            }
            else
            {
                persoAnimate.Play("idle");
            }
        }

        // on peut ressauter
        yield return new WaitForSeconds(1);
        isJump = false;

        persoAnimate.Play("idle"); // pour en pas que le perso soit figé si le joueur n'appuie sur rien...

        // on remet la camera classique
        cameras.cameras[0].SetActive(false);
        cameras.cameras[nbCourrant].SetActive(true);

    }

    /// <summary>
    /// est appelé pour changer la sansibilité de la rtotation du joueur (par un slider)
    /// </summary>
    public void OnChangeSensibilityRotation()
    {
        vitesseRotation = (int)sensibilityRotation.value;
        PlayerPrefs.SetInt("senivityRotation", vitesseRotation);
    }

}

