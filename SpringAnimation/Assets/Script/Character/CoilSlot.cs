using UnityEngine;

public class CoilSlot : MonoBehaviour
{
    public KeyCode action;
    public AttackBehavior actualAttack;

    public void ChangeWeapon()
    {
        actualAttack = transform.GetChild(0).GetComponent<AttackBehavior>();
        actualAttack.StartMethods(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(action) && actualAttack != null)
        {
            actualAttack.Action();
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("tefstttttt");
        actualAttack?.UpdateMethods();
    }
    
}
