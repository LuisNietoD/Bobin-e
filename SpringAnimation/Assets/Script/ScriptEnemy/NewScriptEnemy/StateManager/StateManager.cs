using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public RangeAttack range;
    BaseState currentState;
    public Patrolling patrolling = new Patrolling();
    public Chasing chasing = new Chasing();
    public BirdCatchRat birdCatchRat = new BirdCatchRat();
    public Attacking attacking = new Attacking();


    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolling;

        currentState.EnterState(this, GetComponent<Enemy>());


    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this,GetComponent<Enemy>());
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;

        state.EnterState(this, GetComponent<Enemy>());
    }
}
