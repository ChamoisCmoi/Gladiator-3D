using UnityEngine;
using System;

public class ajusteCamera : MonoBehaviour
{

    public GameObject[] cameras;

    public changeCamera camEnCours;

    public RaycastHit hit; // l'obstacle d�tect� par le raycast

    private GameObject joueur; // pour avoir le game object joueur (direction du raycast)
    [SerializeField] private Collider colliderplayerTrigger;
    [SerializeField] private Collider colliderplayer;
    [SerializeField] private Collider colliderwater;

    public bool avanceCamera; // pour savoir si la camera avance quand elle croise un mur

    private bool avanceCam = false; // pour savoir si la camera a fait un zoom pour �viter un mur
    private float distanceAvanceCam; // pour savoir de quelle distance a �t� avancer la camera

    private void Awake()
    {

        joueur = GameObject.Find("d�partRaycatSaut"); // pour avoir la t�te du joueur

        foreach (GameObject camera in cameras) // d�active toutes les cameras
        {
            camera.SetActive(false);
        }

        camEnCours = (changeCamera)1;
        cameras[(int)camEnCours].SetActive(true);
    }

    void Update()
    {
        if (/*(Input.GetMouseButtonDown(0)) ||*/ (Input.GetKeyDown(KeyCode.Return)))
        {
            changeCam();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // pour retourner � la position classique de la camera
        {
            retourAuNearNormal();
        }

    }

    private void FixedUpdate()
    {
        /*if (avanceCam == true && avanceCamera == true)
        {
            foreach (GameObject camera in cameras)
            {
                if (camera.activeSelf)
                {

                    if (Physics.Raycast(camera.transform.position, joueur.transform.position, out hit, Vector3.Distance(camera.transform.position, joueur.transform.position)))
                    {
                        
                    }
                    else
                    {
                        retourAuNearNormal();
                        print("retour de la cam");
                    }
                }
            }
        }*/

        if (avanceCamera == true)
        {
            foreach (GameObject camera in cameras)
            {
                if (camera.activeSelf)
                {
                    Debug.DrawLine(camera.transform.position, joueur.transform.position, Color.green, Vector3.Distance(camera.transform.position, joueur.transform.position));

                    if (Physics.Raycast(camera.transform.position, joueur.transform.position, out hit, Vector3.Distance(camera.transform.position, joueur.transform.position)))
                    {
                        if (hit.collider)
                        {
                            Camera camCompnent;
                            camCompnent = camera.GetComponent<Camera>();
                            distanceAvanceCam = hit.distance;
                            camCompnent.nearClipPlane =  distanceAvanceCam;
                            avanceCam = true;
                            Debug.Log("avance de la cam");
                   
                        }
                    }
                    else
                    {
                        retourAuNearNormal();
                        print("retour de la cam");
                    }
                }
            }
        }
    }

    private void changeCam() // pour passer � la camera suivante
    {
        if (avanceCam == true)
        {
            retourAuNearNormal();
        }

        cameras[(int)camEnCours].SetActive(false);
        int nbCourrant = (int)camEnCours;
        nbCourrant++;
        if ((nbCourrant) >= Enum.GetNames(typeof(changeCamera)).Length)
        {
            print("on revient � 0");
            nbCourrant = 0;
        }
        camEnCours = (changeCamera)nbCourrant;
        cameras[nbCourrant].SetActive(true);

    }

    void retourAuNearNormal()
    {
        avanceCam = false;
        Camera camCompnent;
        camCompnent = cameras[(int)camEnCours].GetComponent<Camera>();
        
        if(camCompnent.nearClipPlane != 0.3f)
        {
            camCompnent.nearClipPlane = 0.3f;
        }
        
    }
}

public enum changeCamera
{
    main = 0,
    pr�s = 1,
    erPersonne = 2,
    dessus = 3,

}
