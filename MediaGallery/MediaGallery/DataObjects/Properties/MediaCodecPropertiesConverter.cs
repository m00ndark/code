using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace MediaGallery.DataObjects.Properties
{
	public class MediaCodecPropertiesConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(MediaCodecProperties))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(String) && value is MediaCodecProperties)
			{
				MediaCodecProperties mediaCodecProperties = (MediaCodecProperties) value;
				List<string> codecTypes = mediaCodecProperties.Codecs.Select(mediaCodec => mediaCodec.Type.ToString()).ToList();
				codecTypes.Sort();
				return codecTypes.Aggregate((a, b) => (a + ", " + b));
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}