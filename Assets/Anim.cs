using System.Diagnostics.CodeAnalysis;

using UnityEngine;

[RequireComponent(typeof(Animator))]

// ReSharper disable once CheckNamespace
public class Anim : MonoBehaviour
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "Reviewed. Suppression is OK here.")][SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
    // ReSharper disable once InconsistentNaming
    public Animator anim;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
    public GameObject Obj;
    private readonly int runState = Animator.StringToHash("isMoving");

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "Reviewed. Suppression is OK here.")]

    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        this.anim.SetBool(runState, this.Obj.rigidbody.IsSleeping());
    }
}
