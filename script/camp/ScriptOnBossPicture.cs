using UnityEngine;

public class ScriptOnBossPicture : MonoBehaviour
{

    ManageMurBoss manage;

    public int id;

    void Start()
    {
        manage = GameObject.Find("mur des boss").GetComponent<ManageMurBoss>();
    }

    private void OnMouseDown()
    {
        manage.OnPlayerTouchAffiche(id);
    }

}
