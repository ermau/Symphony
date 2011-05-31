//
// MIT License
// Copyright ©2011 Eric Maupin
// All rights reserved.

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Runtime.InteropServices;
using Symphony.Xiph.Ogg;

namespace Symphony.Xiph.Vorbis.Wrapper
{
	internal static class Vorbis
	{
		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool vorbis_synthesis_idheader (ref ogg_packet op);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_commentheader_in (ref vorbis_info vi, ref vorbis_comment vc, ref ogg_packet op);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_synthesis_init (ref vorbis_dsp_state v, ref vorbis_info vi);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_synthesis_restart (ref vorbis_dsp_state v);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_synthesis_headerin (ref vorbis_info vi, ref vorbis_comment vc, ref ogg_packet op);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_synthesis (ref vorbis_block vorbisBlock, ref ogg_packet op);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_synthesis_blockin (ref vorbis_dsp_state v, ref vorbis_block vb);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_synthesis_pcmout (ref vorbis_dsp_state v, out float[][] pcm);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern long vorbis_packet_blocksize (ref vorbis_info vi, ref ogg_packet op);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int vorbis_block_init (ref vorbis_dsp_state v, ref vorbis_block vb);

		internal static void ThrowIfError (this int result)
		{
			if (Enum.IsDefined (typeof (OV), result))
				throw new VorbisException (result);
		}
	}

	internal struct vorbis_block
	{
		
	}

// ReSharper disable InconsistentNaming
	internal enum OV
		: int
	{
		FALSE = -1,
		EOF = -2,
		HOLE = -3,

		EREAD = -128,
		EFAULT = -129,
		EIMPL = -130,
		EINVAL = -131,
		ENOTVORBIS = -132,
		EBADHEADER = -133,
		EVERSION = -134,
		ENOTAUDIO = -135,
		EBADPACKET = -136,
		EBADLINK = -137,
		ENOSEEK = -138
	}
// ReSharper restore InconsistentNaming

	internal class VorbisException
		: Exception
	{
		public VorbisException (int error)
			: base ("OV_" + (OV)error)
		{
			Error = (OV)error;
		}

		public OV Error
		{
			get;
			private set;
		}
	}
}