using Assets.Scripts.GameLogic.ActorComponents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatDirectorMono : MonoBehaviour
{
    // subscribed to event that gets CombatPOCO from InputManage
    private Dictionary<string, ParticleSystem> _animationAssets;

    [SerializeField]
    private ActorId[] actorIds;

    public void Awake()
    {
        _animationAssets = new Dictionary<string, ParticleSystem>() {
            { "ReadyAttack", Resources.Load<ParticleSystem>("FX/ReadyAttack") },
            { "BasicAttack", Resources.Load<ParticleSystem>("FX/BasicAttack") }
        };

        ServiceProvider.InputManager.CombatEvent.AddListener(Run);
    }

    public void Run()
    {
        StartCoroutine(PlayRenderQueue());
    }

    public IEnumerator PlayRenderQueue()
    {
        var rqeQueue = ServiceProvider.InputManager.RenderQueue;

        while (rqeQueue.Count > 0)
        {
            var rqe = rqeQueue.Dequeue();
            var anim = _animationAssets[rqe.Animation];
            var duration = anim.GetComponent<ParticleSystem>().main.duration;

            var animSpawned = new List<ParticleSystem>();

            foreach (var actor in actorIds)
            {
                if(rqe.TargetCardIds.Contains(actor.Id))
                {
                    animSpawned.Add(Instantiate(anim, actor.transform));
                }
            }

            yield return new WaitForSeconds(duration);

            foreach (var animObj in animSpawned)
            {
                Destroy(animObj);
            }
        }
    }
}
