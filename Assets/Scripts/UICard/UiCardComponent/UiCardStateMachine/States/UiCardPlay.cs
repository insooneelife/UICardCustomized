using Patterns.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UICard;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace UICard
{
	public class UiCardPlay : UiBaseCardState
	{

		public UiCardPlay(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters)
			: base(handler, fsm, parameters)
		{ }

		public override void OnEnterState()
		{
			_handler.MoveTo(_handler.Hand.PlayTransform.position, _parameters.MovementSpeed * 2);
		}

		public override void OnExitState()
		{
		}
	}
}


