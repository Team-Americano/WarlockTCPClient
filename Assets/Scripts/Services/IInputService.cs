using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public interface IInputService
{
    GameState Game { get; set; } // This needs to change
    Queue<RenderQueueEntry> RenderQueue { get; set; }
    UnityEvent DrawEvent { get; set; }
    UnityEvent UpdateUiEvent { get; set; }
    UnityEvent DraftEvent { get; set; }
    UnityEvent CombatEvent { get; set; }

    Task HandleCommand(Packet packet);
    Task Test(Packet packet);
    Task Hello(Packet packet);
    Task DrawCards(Packet packet);
    Task DraftActors(); //------------
    Task AcknowlegdeDraft(Packet packet);
    Task PartyReposition(Packet packet);
    Task StartCombatPhase(Packet packet);
}
