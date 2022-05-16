using System;
using System.Collections.Generic;
using System.Text;

namespace ChildScheduler.Extensions
{
	public static class IntExtensions
	{
		public static int RoundToLowestHundreds(this int i)
		{
			return (int)Math.Floor(i / 100.0) * 100;
		}
	}
}
