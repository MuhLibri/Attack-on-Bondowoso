using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttack : MonoBehaviour
{

    public bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackBegin() {
        attacking = true;
    }

    void AttackEnd() {
        attacking = false;
    }
}
