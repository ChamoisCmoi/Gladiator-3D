using UnityEngine;
using System.IO;

public class saveData : MonoBehaviour
{
    private GameObject personnage;

    private float[] coordonnee = new float[6];

    private string separation = "µ";



    private void Awake()
    {
        savePos();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            savePos();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            getPos();
        }
    }

    void savePos()
    {

        personnage = GameObject.Find("personnage");

        coordonnee[0] = personnage.transform.position.x;
        coordonnee[1] = personnage.transform.position.y;
        coordonnee[2] = personnage.transform.position.z;
        coordonnee[3] = personnage.transform.rotation.x;
        coordonnee[4] = personnage.transform.rotation.y;
        coordonnee[5] = personnage.transform.rotation.z;


        string toSave = string.Join(separation, coordonnee);
        File.WriteAllText(Application.dataPath + "/sauvegardeDesPositions.txt", toSave);
        print("sauvegarde des positions effectuées");
    }

    void getPos()
    {
        string toRefrech = File.ReadAllText(Application.dataPath + "/sauvegardeDesPositions.txt");
        string[] content = toRefrech.Split(new[] { separation }, System.StringSplitOptions.None);

        personnage.transform.position = new Vector3( float.Parse(content[0]) , float.Parse(content[1]), float.Parse(content[2]) );

        float rotateX = float.Parse(content[3]);
        float rotateY = float.Parse(content[4]);
        float rotateZ = float.Parse(content[5]);

        personnage.transform.Rotate(rotateX, rotateY, rotateZ);
    }
}
