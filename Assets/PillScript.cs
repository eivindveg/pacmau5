using System.Diagnostics.CodeAnalysis;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class PillScript : MonoBehaviour
{
    // God mode duration in frames, e.g 600 for 10 seconds.
    public const int GodDuration = 600;

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public void OnTriggerEnter(Component other)
    {
        if (other.tag == "Player")
        {
            if (this.tag == "ScorePill")
            {
                ScoreScript.CurrentScore++;
                PillMonitor.EatPill(this.gameObject);
            }
            else if (this.tag == "AmmoPill")
            {
               var actorScript = other.gameObject.GetComponent<PacMau5ActorScript>();
               actorScript.AddAmmo(1);
            }
            else if (this.tag == "LargePill")
            {
                var actorScript = other.gameObject.GetComponent<PacMau5ActorScript>();
                actorScript.TriggerGodMode(GodDuration);
            }
            // ReSharper disable once AccessToStaticMemberViaDerivedType
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        // IF SCORE OBJECT: REGISTER THIS PILL AS A SCORE OBJECT FOR THE COUNTER
    }
    
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
    }
}
