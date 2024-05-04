using System;

[Serializable]
public class FSMState
{
    public string ID;
    public FSMAction[] Actions;
    public FSMTransition[] Transitions;


    public void UpdateState(EnemyAI enemyAI)
    {
        ExecuteActions();
        ExecuteTransitions(enemyAI);
    }

    public void ExecuteActions()
    {
        for (int i = 0; i< Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(EnemyAI enemyAI)
    {
        if (Transitions == null || Transitions.Length <= 0) return;
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool value = Transitions[i].Decision.Decide();
            if (value)
            {
                enemyAI.ChangeState(Transitions[i].TrueState);
            }
            else
            {
                enemyAI.ChangeState(Transitions[i].FalseState);
            }
        }
    }
}
