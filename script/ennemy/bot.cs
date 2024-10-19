using UnityEngine.AI;
using UnityEngine;

public class bot : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;

    [Header("à associer")]
    GameObject player; // le joueur

    [Space]

    [Header("caractéristiques du bot")]
    [SerializeField] ennemy ennemyData;


    private float distanceArret; // distace à laquelle s'arrête le bot
    public bool enMarche; // savoir si le put est en marche
    private int PV; // la santé du joueur

    bool mort = false; // pour savoir si le bot est mort

    private void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        distanceArret = nav.stoppingDistance;
        player = GameObject.FindGameObjectWithTag("Player");
        nav.speed = ennemyData.vitesse;
        PV = ennemyData.PV;
    }

    private void Start()
    {
        // on va initialiser les infos à l'animator
        anim.SetInteger("type Attaque", (int)ennemyData.typeAttaque);
        anim.SetInteger("type celebration", (int)ennemyData.celebrationParDefaut);
    }

    private void Update()
    {
        if (mort == false)
        {
            anim.SetFloat("vitesse", nav.velocity.magnitude);
            if (anim.applyRootMotion == true)
            {
                if (nav.velocity.magnitude > ennemyData.vitesse - 1)
                {
                    //print("re applyRootMotion");
                    anim.applyRootMotion = false;
                }
            }

            nav.SetDestination(player.transform.position); // car le bot va en permanence vers le joueur

            if (nav.remainingDistance >= distanceArret) // le bot est en marche
            {
                enMarche = true;
            }

            if (nav.remainingDistance <= distanceArret)
            {
                if (nav.velocity.magnitude > 0.2f && enMarche)
                {
                    OnPathEnd();
                    enMarche = false; // car le bot s'est arrêté
                }
            }
        }
    }

    /// <summary>
    /// est appelé pour savoir quelle est la distance entre le joueur et le bot
    /// </summary>
    /// <returns></returns>
    public float calculPlayerDistance()
    {
         float distance;
         distance = Vector3.Distance(player.transform.position, this.transform.position);
         return distance;
    }

    /// <summary>
    /// est appelé quand le joueur arrive à destination
    /// </summary>
    void OnPathEnd()
    {
        //print("L'agent est arrivé à destination");

    }

    public ennemy GetEnnemyData()
    {
        return ennemyData;
    }

    /// <summary>
    /// est appelé quand le joueur attaque ce bot
    /// </summary>
    public void OnBotBeHurt(int xpEnMoins)
    {
        PV -= xpEnMoins;

        if (PV <= 0) // si le bot n'a plus de vie ?
        {
            mort = true;
            Destroy(this.GetComponent<NavMeshAgent>());
            Destroy(this.GetComponent<attaque>());

            anim.applyRootMotion = true;
            anim.SetBool("en vie", false);
            anim.SetTrigger("mort");

            GameObject.Find("Canvas").GetComponent<ManageSpawn>().DelateBotFromTheList(this.gameObject);

            // l'animation de mort dure 3 secondes 10
            // d'où :
            transform.Translate(0, 0.3f, 0); //car si non le bot rentre dans le sol
            StartCoroutine(disparitionBot());

        }
    }

    System.Collections.IEnumerator disparitionBot()
    {
        anim.SetFloat("vitesse", 0);
        yield return new WaitForSeconds(3.5f);
        anim.speed = 0;
        //Destroy(GetComponentInChildren<Animator>());
        yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
    }
    /// <summary>
    /// est appelé quand le bot attaque le joueur
    /// </summary>
    public void BotAttackPlayer()
    {
        // on attaque je joueur 
        transform.LookAt(player.transform);
        //anim.applyRootMotion = true;
        anim.SetFloat("vitesse", 0);
        if (mort == false)
        {
            anim.SetTrigger("attaque");
        }
    }
}

