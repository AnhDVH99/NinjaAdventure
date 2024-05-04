using System;


[Serializable]
public class FSMTransition
{
    public FSMDecision Decision; // PlayerInRangeOfAttack -> True or False
    public string TrueState; // CurrentState -> AttackState (example)
    public string FalseState; // CurrentState -> PatrolState (example) 
}
