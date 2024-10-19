using UnityEngine;

public class ScriptOnClickLancerNiv : MonoBehaviour
{

    [SerializeField] int numNiv;

    private void OnMouseDown()
    {
        GameObject.Find("mur des boss").GetComponent<ManageMurBoss>().OnLancerNiveau(numNiv);
    }
}
