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

namespace Symphony.Xiph.Vorbis.Wrapper
{
	internal class VorbisComment
	{
		public VorbisComment()
		{
			vorbis_comment_init (ref this.comment);
		}

		private vorbis_comment comment;

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_comment_init (ref vorbis_comment vc);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_comment_add (ref vorbis_comment vc, string comment);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_comment_add_tag (ref vorbis_comment vc, string tag, string contents);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern string vorbis_comment_query (ref vorbis_comment vc, string tag, short count);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern short vorbis_comment_query_count (ref vorbis_comment vc, string tag);

		[DllImport ("libvorbis.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void vorbis_comment_clear (ref vorbis_comment vc);

		
	}

	internal struct vorbis_comment
	{
		public IntPtr user_comments;
		public IntPtr comment_lengths;
		public short comments;
		public IntPtr vendor;
	}
}