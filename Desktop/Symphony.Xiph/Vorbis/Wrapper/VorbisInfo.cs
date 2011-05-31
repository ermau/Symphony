using System;
using System.Runtime.InteropServices;

namespace Symphony.Xiph.Vorbis.Wrapper
{
	internal class VorbisInfo
	{
		public VorbisInfo()
		{
			vorbis_info_init (ref this.info);
		}

		private vorbis_info info = new vorbis_info();

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_info_init (ref vorbis_info vi);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_info_clear (ref vorbis_info vi);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_info_blocksize (ref vorbis_info vi, short zo);
	}

	internal struct vorbis_info
	{
		public short version;
		public short channels;
		public int rate;

		public int bitrate_upper;
		public int bitrate_nominal;
		public int bitrate_lower;
		public int bitrate_window;

		public IntPtr codec_setup;
	}
}