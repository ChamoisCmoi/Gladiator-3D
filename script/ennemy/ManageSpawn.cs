using UnityEngine;
using System.Collections.Generic;

public class ManageSpawn : MonoBehaviour
{
    [SerializeField] ennemy[] ennemys;

    [SerializeField] Transform spawPoint;

    List<GameObject> botsEnJeu = new List<GameObject>();


    /// <summary>
    /// est appelé pour faire apparaitre un bot
    /// </summary>
    public void SpawEnnemy(int num)
    {
       GameObject bot = Instantiate(ennemys[num].modele3D,spawPoint);
        botsEnJeu.Add(bot);
    }

    private void Start()
    {
        SpawEnnemy(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SpawEnnemy(0);
        }
    }

    /// <summary>
    /// est appelé par le bot quand il meurt
    /// </summary>
    public void DelateBotFromTheList(GameObject bot)
    {
        botsEnJeu.Remove(bot);

        if(botsEnJeu.Count == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerCombat>().OnBotGain(); // car il ne reste plus de bots en jeu
        }
    }
}
