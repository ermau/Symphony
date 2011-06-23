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

namespace Symphony.Xiph.Flac.Wrapper
{
	[StructLayout (LayoutKind.Explicit)]
	internal unsafe struct StreamMetadata
	{
		
		
		[DllImport ("libflac.dll", CallingConvention = CallingConvention.Cdecl)]
		internal extern static bool FLAC__metadata_get_streaminfo (string path, out StreamMetadata streaminfo);

		[DllImport ("libflac.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FLAC__metadata_get_tags (string path, StreamMetadata** tags);

		[DllImport ("libflac.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FLAC__metadata_get_cuesheet (string path, StreamMetadata** cuesheet);

		[DllImport ("libflac.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FLAC__metadata_get_picture (string filename, StreamMetadata** picture, Picture.Type type,
		                                                        string mime_type, IntPtr description, uint max_width,
		                                                        uint max_height, uint max_depth, uint max_colors);

		public static StreamMetadata GetMetaData (string path)
		{
			if (path == null)
				throw new ArgumentNullException ("path");

			throw new NotImplementedException();
		}

		internal enum Type
		{
			StreamInfo = 0,
			Padding = 1,
			Application = 2,
			Seektable = 3,
			VorbisComment = 4,
			CueSheet = 5,
			Picture = 6,
			Undefined = 7
		}

		[FieldOffset (0)] internal Type type;
		[FieldOffset (1)] internal bool is_last;
		[FieldOffset (2)] internal uint length;

		[FieldOffset (3)] internal StreamInfo stream_info;
		[FieldOffset (3)] internal Padding padding;
		[FieldOffset (3)] internal Application application;
		[FieldOffset (3)] internal SeekTable seek_table;
		[FieldOffset (3)] internal VorbisComment vorbis_comment;
		[FieldOffset (3)] internal CueSheet cue_sheet;
		[FieldOffset (3)] internal Picture picture;
		[FieldOffset (3)] internal Unknown unknown;

		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct StreamInfo
		{
			internal uint min_blocksize;
			internal uint max_blocksize;
			internal uint sample_rate;
			internal uint channels;
			internal uint bits_per_sample;
			internal ulong total_samples;
			internal fixed byte md5sum [16];
		}

		[StructLayout (LayoutKind.Sequential)]
		internal struct Padding
		{
			internal int dummy;
		}

		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct Application
		{
			internal fixed byte id [4];
		}

		[StructLayout (LayoutKind.Sequential)]
		internal struct SeekPoint
		{
			internal ulong sample_number;
			internal ulong stream_offset;
		}

		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct SeekTable
		{
			internal uint num_points;
			internal SeekPoint *points;
		}
		
		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct VorbisComment
		{
			internal Entry vendor_string;
			internal uint num_comments;
			internal Entry* comments;

			[StructLayout(LayoutKind.Sequential)]
			public unsafe struct Entry
			{
				internal uint length;
				internal byte* entry;
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct CueSheet
		{
			internal fixed byte media_catalog_number [129];
			internal ulong lead_in;
			internal bool is_cd;
			internal Track* tracks;
			 
			[StructLayout (LayoutKind.Sequential)]
			public unsafe struct Index
			{
				internal ulong offset;
				internal byte number;
			}

			[StructLayout (LayoutKind.Explicit)]
			public unsafe struct Track
			{
				[FieldOffset (0)] internal ulong offset;
				[FieldOffset (4)] internal byte number;
				[FieldOffset (5)] internal fixed byte isrc [13];

				[FieldOffset (18)] internal uint type;
				[FieldOffset (18)] internal uint pre_emphasis;
				[FieldOffset (22)] internal byte num_indices;

				[FieldOffset (23)] internal Index* indices;
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct Picture
		{
			internal Type type;
			internal byte *mime_type;
			internal byte* description;
			internal uint width;
			internal uint height;
			internal uint depth;
			internal uint colors;
			internal uint data_length;
			internal byte* data;

			public enum Type
			{
				Other = 0,
				FileIconStandard = 1,
				FileIcon = 2,
				FrontCover = 3,
				BackCover = 4,
				LeafletPage = 5,
				Media = 6,
				LeadArtist = 7,
				Artist = 8,
				Conductor = 9,
				Band = 10,
				Composer = 11,
				Lyricist = 12,
				RecordingLocation = 13,
				DuringRecording = 14,
				DuringPerformance = 15,
				VideoScreenCapture = 16,
				Fish = 17,
				Illustration = 18,
				BandLogoType = 19,
				PublisherLogoType = 20,
				Undefined
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		internal unsafe struct Unknown
		{
			internal byte* data;
		}
	}
}