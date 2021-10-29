using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers
{
    public class Utility
    {
		public static DateTime CurrentTime
		{
			get
			{
				DateTime timeToReturn = DateTime.UtcNow.AddHours(1);
				return timeToReturn;
			}
		}
	}
}
