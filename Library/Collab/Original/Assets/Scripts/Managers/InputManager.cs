using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

// Tests 100
// Draw Phase 200
// Draft Phase 300
// Reposition Phase 400
// Combat Phase 500
public enum CommandId
{
    test = 42,
    hello = 100,
    draw = 200,
    draft = 300,
    acknowlegdeDraft = 301,
    partyReposition = 400,
    acknowlegdeReposition = 401,
    combat = 500
}

public class InputManager : IInputService
{
    private Dictionary<CommandId, Command> _commands;

    public delegate Task Command(Packet packet);

    public GameState Game { get; set; } = new GameState(); // This needs to change
    public Queue<RenderQueueEntry> RenderQueue { get; set; } = new Queue<RenderQueueEntry>();

    public UnityEvent DrawEvent { get; set; }
    public UnityEvent UpdateUiEvent { get; set; }
    public UnityEvent DraftEvent { get; set; }
    public UnityEvent CombatEvent { get; set; }

    public InputManager()
    {
        _commands = new Dictionary<CommandId, Command>()
        {
            { CommandId.test, Test },
            { CommandId.hello, Hello },
            { CommandId.draw, DrawCards },
            //{ CommandId.draft, DraftActors },
            { CommandId.acknowlegdeDraft, AcknowlegdeDraft },
            { CommandId.partyReposition, PartyReposition },
            { CommandId.combat, StartCombatPhase }
        };

        DrawEvent = new UnityEvent();
        UpdateUiEvent = new UnityEvent();
        DraftEvent = new UnityEvent();
        CombatEvent = new UnityEvent();
    }

    public Task HandleCommand(Packet packet)
    {
        var commandId = (CommandId)packet.CommandId;

        if (_commands.ContainsKey(commandId))
        {
            _commands[commandId].Invoke(packet);
        }

        return Task.FromResult(0);
    }

    public Task Test(Packet packet)
    {
        TestPOCO poco = (TestPOCO)ServiceProvider.NetworkManager.GetPocoFromPacket(packet);
        Debug.Log(poco.Line);

        ServiceProvider.NetworkManager.SendPacket(packet);

        return Task.FromResult(0);
    }

    public Task Hello(Packet packet)
    {
        ServiceProvider.PlayerManager.SetHomePlayer(packet.PlayerId);

        return Task.FromResult(0);
    }

    public Task DrawCards(Packet packet)
    {
        // ===================== Steps To Update Hand ===============
        // 1. Convert packet to POCO
        // 2. Get the new array from packet
        // 3. Update the player's hand array/list
        // 4. Render the new list to the view

        var poco = JsonUtility.FromJson<DrawPOCO>(packet.POCOJson);

        Player homePlayer = ServiceProvider.PlayerManager.GetPlayer(packet.PlayerId);

        homePlayer.Hand = poco.Hand;
        Game.Round = poco.Round;

        DrawEvent.Invoke();

        return Task.FromResult(0);
    }

    public Task DraftActors()
    {
        // ================= Steps to Draft =================
        // 1. Convert packet to POCO
        // 2. update hand array
        // 3. update party array
        // 4. update Mana
        // 7. update ui elements
        // 8. send updated info to server

        Player homePlayer = ServiceProvider.PlayerManager.GetHomePlayer();

        DraftEvent.Invoke();
        

        UpdateUiEvent.Invoke();


        DraftPOCO outputPoco = new DraftPOCO
        {
            Hand = homePlayer.Hand,
            Party = homePlayer.Party,
            Mana = homePlayer.Mana
        };

        Packet packet = new Packet
        {
            CommandId = (short)CommandId.draft,
            PlayerId = homePlayer.PlayerId,
            POCOJson = JsonUtility.ToJson(outputPoco)
        };

        ServiceProvider.NetworkManager.SendPacket(packet);

        return Task.FromResult(0);
    }

    public Task AcknowlegdeDraft(Packet packet)
    {
        var poco = JsonUtility.FromJson<DraftPOCO>(packet.POCOJson);

        var player = ServiceProvider.PlayerManager.GetHomePlayer();

        player.Party = poco.Party;
        player.Hand = poco.Hand;
        player.Mana = poco.Mana;

        DraftEvent.Invoke();
        UpdateUiEvent.Invoke();

        return Task.FromResult(0);
    }

    public Task PartyReposition(Packet packet)
    {
        // ============== Steps for Repo ======================
        // 1. convert packet to POCO
        // 2. update party
        // 3. render party change
        // 4. send update to server

        throw new System.NotImplementedException();
    }

    public Task StartCombatPhase(Packet packet)
    {
        var poco = JsonUtility.FromJson<CombatPOCO>(packet.POCOJson);

        foreach (var rqe in poco.RenderQueueEntries)
        {
            RenderQueue.Enqueue(rqe);
        }

        CombatEvent.Invoke();

        return Task.FromResult(0);
    }

}
