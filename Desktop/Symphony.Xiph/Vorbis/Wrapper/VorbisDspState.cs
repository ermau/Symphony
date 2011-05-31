using System;

namespace Symphony.Xiph.Vorbis.Wrapper
{
	internal class VorbisDspState
	{
	}

	internal unsafe struct vorbis_dsp_state
	{
		private int analysisp;
		private IntPtr vi;

		private IntPtr pcm;
		private IntPtr pcmret;
		private int pcm_storage;
		private int pcm_current;
		private int pcm_returned;

		private int preextrapolate;
		private int eofflag;

		private int lW;
		private int W;
		private int nW;
		private int centerW;

		private long granulepos;
		private long sequence;

		private long glue_bits;
		private long time_bits;
		private long floor_bits;
		private long res_bits;

		private IntPtr backend_state;
	}
}