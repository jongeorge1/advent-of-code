// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "StyleCop.CSharp.SpacingRules",
    "SA1000:Keywords should be spaced correctly",
    Justification = "Doesn't match up with latest syntax",
    Scope = "module")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.SpacingRules",
    "SA1010:Opening square brackets should be spaced correctly",
    Justification = "Doesn't match up with latest syntax",
    Scope = "module")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1405:Debug.Assert should provide message text",
    Justification = "Not worth it",
    Scope = "module")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1402:File may only contain a single type",
    Justification = "Sometimes it's better for AOC solutions",
    Scope = "module")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.NamingRules",
    "SA1313:Parameter names should begin with lower-case letter",
    Justification = "Conflicts with other rules",
    Scope = "module")]
