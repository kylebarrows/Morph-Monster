using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFormController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite sprite;

    public void Awake()
    {
        sprite = Resources.Load<Sprite>("Mace");
    }
}
