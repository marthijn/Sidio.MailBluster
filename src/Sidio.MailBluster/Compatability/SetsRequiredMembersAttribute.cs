﻿#if !NET7_0_OR_GREATER

// ReSharper disable once CheckNamespace
namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
internal sealed class SetsRequiredMembersAttribute : Attribute {}

#endif