using System;

namespace Symphony.Xiph.Vorbis.Wrapper
{
	internal class VorbisDspState
	{
	}

	internal struct vorbis_dsp_state
	{
		private short analysisp;
		private IntPtr vi;

		private float[][] pcm;
		private float[][] pcmret;
		private short pcm_storage;
		private short pcm_current;
		private short pcm_returned;

		private short preextrapolate;
		private short eofflag;

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