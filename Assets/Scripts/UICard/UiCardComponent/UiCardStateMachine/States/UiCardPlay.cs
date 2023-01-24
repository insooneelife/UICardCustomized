using Patterns.StateMachine;
using System.Collections;
using System.Collections.Generic;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Tools.UI.Card 
{
	public class UiCardPlay : UiBaseCardState
	{

		public UiCardPlay(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters)
			: base(handler, fsm, parameters)
		{

		}

		public override void OnEnterState()
		{
			Handler.MoveTo(Handler.Hand.PlayTransform.position, Parameters.MovementSpeed * 2);
		}

		public override void OnExitState()
		{
		}		
	}
}


