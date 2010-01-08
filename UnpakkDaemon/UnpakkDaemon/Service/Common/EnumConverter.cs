using System;

namespace UnpakkDaemon.Service.Common
{
	public static class EnumConverter
	{
		public static T2 ConvertEnumValue<T1, T2>(T1 fromEnumValue)
		{
			return (T2) Enum.Parse(typeof(T2), fromEnumValue.ToString());
		}
	}
}
