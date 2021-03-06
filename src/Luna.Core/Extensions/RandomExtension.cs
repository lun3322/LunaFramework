﻿using System;

namespace Luna.Extensions
{
    public static class RandomExtension
    {
        public static T OneOf<T>(this Random @this, params T[] values)
        {
            return values[@this.Next(values.Length)];
        }
    }
}