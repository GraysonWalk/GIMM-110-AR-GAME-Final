// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Playables;
//
// public class GameStartState : RecognitionBaseState
// {
//     private RecognitionGameManager gameManager;
//     private readonly GameObject _systems;
//     private readonly List<ARTarget> _targets = new List<ARTarget>();
//     private readonly List<ARTarget> _activatedTargets = new List<ARTarget>();
//     public GameStartState(RecognitionGameManager gamemanager, GameObject  systems) 
//     {  
//         this.gameManager = gamemanager;
//         _systems = systems; 
//     
//     }
//     
//     public override void OnEnter()
//     {
//         var targets = _systems.GetComponentsInChildren<ARTarget>();
//         _targets.AddRange(targets);
//         foreach (var target in targets)
//         {
//             target.OnActivated += OnTargetActivated;
//         }
//     }
//
//     private void OnTargetActivated(ARTarget target)
//     {
//         if (_activatedTargets.Count >= 3)
//         {
//             gameManager.StateMachine.ChangeState(PlayState);
//         }
//         else
//         {
//             _activatedTargets.Add(target);
//         }
//             
//     }
// }

/*
 * MOVED TO INITIALIZE STATE
 */

