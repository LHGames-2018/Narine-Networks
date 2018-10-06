using System;
using System.Collections.Generic;
using LHGames.Helper;

public class Brain
{
	public States currentState;
	States savedLastState;
	
	public void UpdateState()
	{
		currentState.Update();
	}
	
	public void ExitCurrentState()
	{
		currentState.ExitCurrentState();
	}
	
	public void SetNewState(States states)
	{
		if(currentState != null)
		{
			savedLastState = currentState;
		}
		
		if(CheckIfLastState())
		{
			return;
		}
		
		currentState = states;
		currentState.StartStates();
	}
	
	public string GenerateAction()
	{
		return "Halo 3 gros bb";
	}
	
    bool CheckIfLastState()
	{
		if(currentState == savedLastState)
		{
			return true;
		}
		
		return false;
	}
}

