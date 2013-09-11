using System; 

using System.IO; 

using System.Runtime.InteropServices; 

 

 

namespace Croc.Bpc.GsScannerDriver 

{ 

	/// <summary> 

	/// ????? ?????? 

	/// </summary> 

	public enum WorkMode : short 

	{ 

		/// <summary> 

		/// 1 - ??????? ????? (????? ????????????) 

		/// </summary> 

		Work = 1, 

 

 

		/// <summary> 

		/// 0 - ?????????? ????? (????? LineView) 

		/// </summary> 

		Debug = 0 

	} 

 

 

	/// <summary> 

	/// ????? ????????????? ???????? ????? 

	/// </summary> 

	public enum DoubleSheet : short 

	{ 

		/// <summary> 

		/// 1 - ??????? 

		/// </summary> 

		On = 1, 

 

 

		/// <summary> 

		/// 0 - ???????? 

		/// </summary> 

		Off = 0 

	} 

 

 

	/// <summary> 

	/// ?????????? ???????? 

	/// </summary> 

	public enum Marker : short 

	{ 

		/// <summary> 

		/// 1 - ????????? ?????? ? ??????? ????????? 


		/// </summary> 

		On = 1, 

 

 

		/// <summary> 

		/// 0 - ??????? ?????? ? ????????? ????? 

		/// </summary> 

		Off = 0 

	} 

 

 

	/// <summary> 

	/// ????????????? ????????????? ?????? 

	/// </summary> 

	public enum WhiteCoeff : short 

	{ 

		/// <summary> 

		/// 1 - ???????????? 

		/// </summary> 

		On = 1, 

 

 

		/// <summary> 

		/// 0 - ?? ???????????? 

		/// </summary> 

		Off = 0 

	} 

 

 

	/// <summary> 

	/// ????? ????????? ?? ???? 

	/// </summary> 

	public enum TuningMode : short 

	{ 

		/// <summary> 

		/// 1 - ??????? 

		/// </summary> 

		On = 1, 

 

 

		/// <summary> 

		/// 0 - ???????? 

		/// </summary> 

		Off = 0 

	} 

 

 

	/// <summary> 

	/// ????? ????????????? ????? ?? CIS 

	/// </summary> 


	public enum DirtDetection : short 

	{ 

		/// <summary> 

		/// 1 - ??????? 

		/// </summary> 

		On = 1, 

 

 

		/// <summary> 

		/// 0 - ???????? 

		/// </summary> 

		Off = 0 

	} 

 

 

	/// <summary> 

	/// ????????? ???? 

	/// ??????? ??? ????????? ????????? 

	/// </summary> 

	[Flags] 

    public enum Lamps : short 

	{ 

		/// <summary> 

		/// ??????? ???????? 

		/// </summary> 

		RedOn = 1, 

 

 

		/// <summary> 

		/// ??????? ???????? 

		/// </summary> 

		GreenOn = 2, 

 

 

		/// <summary> 

		/// ??? ???????? 

		/// </summary> 

		BothOn = 3, 

 

 

		/// <summary> 

		/// ??????? ????????? 

		/// </summary> 

		RedOff	= 0x10, 

 

 

		/// <summary> 

		/// ??????? ????????? 

		/// </summary> 

		GreenOff = 0x20, 


 
 

		/// <summary> 

		/// ??? ????????? 

		/// </summary> 

		BothOff = 0x30, 

	} 

 

 

	/// <summary> 

	/// ????????? ??? ???????? ???????????? ??????? ???????. 

	/// </summary> 

	[StructLayout(LayoutKind.Sequential)] 

	public struct ScannerProps 

	{ 

		/// <summary> 

		/// ???????? ????????? ??????? 

		/// </summary> 

		/// <param name="data">?????? ?? ????????? ????????</param> 

		public ScannerProps( byte[] data ) 

		{ 

			BinaryReader br = new System.IO.BinaryReader( new MemoryStream( data ) ); 

			WorkMode = (WorkMode)br.ReadInt16(); 

			DpiX0 = br.ReadInt16(); 

			DpiY0 = br.ReadInt16(); 

			DpiX1 = br.ReadInt16(); 

			DpiY1 = br.ReadInt16(); 

			DoubleSheet = (DoubleSheet)br.ReadInt16(); 

			Marker = (Marker)br.ReadInt16(); 

			DoubleSheetLevelL = br.ReadInt16(); 

			WhiteCoeff = (WhiteCoeff)br.ReadInt16(); 

			BinaryThreshold0 = br.ReadInt16(); 

			BinaryThreshold1 = br.ReadInt16(); 

			MinSheetLength = br.ReadInt16(); 

			MaxSheetLength = br.ReadInt16(); 

			DoubleSheetLevelR = br.ReadInt16(); 

			TuningMode = (TuningMode)br.ReadInt16(); 

			MarkerWork = br.ReadInt16(); 

			DirtDetection = (DirtDetection)br.ReadInt16(); 

			OfflineMode = br.ReadInt16(); 

			Lamps = (Lamps)br.ReadInt16(); 

			reserv = new short[21]; 

			for ( int i = 0; i < reserv.Length; i++ ) 

			{ 

				reserv[i] = br.ReadInt16(); 

			} 

			br.Close(); 

		} 

 

 


		/// <summary> 

		/// ?????? ?????? ??? ???????? ????????? ???????? 

		/// </summary> 

		public byte[] Data 

		{ 

			get 

			{ 

				MemoryStream ms = new MemoryStream( 80 ); 

				BinaryWriter bw = new System.IO.BinaryWriter( ms ); 

				bw.Write((short)WorkMode); 

				bw.Write(DpiX0); 

				bw.Write(DpiY0); 

				bw.Write(DpiX1); 

				bw.Write(DpiY1); 

				bw.Write((short)DoubleSheet); 

				bw.Write((short)Marker); 

				bw.Write(DoubleSheetLevelL); 

				bw.Write((short)WhiteCoeff); 

				bw.Write(BinaryThreshold0); 

				bw.Write(BinaryThreshold1); 

				bw.Write(MinSheetLength); 

				bw.Write(MaxSheetLength); 

				bw.Write(DoubleSheetLevelR); 

				bw.Write((short)TuningMode); 

				bw.Write(MarkerWork); 

				bw.Write((short)DirtDetection); 

				bw.Write(OfflineMode); 

				bw.Write((short)Lamps); 

				for ( int i = 0; i < reserv.Length; i++ ) 

				{ 

					bw.Write(reserv[i]); 

				} 

				bw.Flush(); 

				byte[] data = ms.ToArray(); 

				bw.Close(); 

				return data; 

			} 

		} 

 

 

		/// <summary> 

		/// ????? ??????/??????? 

		/// </summary> 

		public WorkMode WorkMode; 

 

 

		/// <summary> 

		/// DPI ?? ??? X (??????? 0) 

		/// </summary> 

		public short	DpiX0; 


 
 

		/// <summary> 

		/// DPI ?? ??? Y (??????? 0) 

		/// </summary> 

		public short	DpiY0; 

 

 

		/// <summary> 

		/// DPI ?? ??? X (??????? 1) 

		/// </summary> 

		public short	DpiX1; 

 

 

		/// <summary> 

		/// DPI ?? ??? Y (??????? 1) 

		/// </summary> 

		public short	DpiY1; 

 

 

		/// <summary> 

		/// ????? ????????????? ???????? ????? 

		/// </summary> 

		public DoubleSheet	DoubleSheet; 

 

 

		/// <summary> 

		/// ?????????? ???????? 

		/// </summary> 

		public Marker	Marker; 

 

 

		/// <summary> 

		/// ??????? ????? ?????? ??????? ???????? ????? 

		/// </summary> 

		public short	DoubleSheetLevelL; 

 

 

		/// <summary> 

		/// ????????????? ????????????? ??????? 

		/// </summary> 

		public WhiteCoeff	WhiteCoeff; 

 

 

		/// <summary> 

		/// ??????? ????? ??????????? ?? ??????? 0 

		/// </summary> 

		public short	BinaryThreshold0; 

 

 


		/// <summary> 

		/// ??????? ????? ??????????? ?? ??????? 1 

		/// </summary> 

		public short	BinaryThreshold1; 

 

 

		/// <summary> 

		/// ??????????? ?????????? ????? ????? 

		/// </summary> 

		public short	MinSheetLength; 

 

 

		/// <summary> 

		/// ???????????? ?????????? ????? ????? 

		/// </summary> 

		public short	MaxSheetLength; 

 

 

		/// <summary> 

		/// ??????? ????? ??????? ??????? ???????? ????? 

		/// </summary> 

		public short	DoubleSheetLevelR; 

 

 

		/// <summary> 

		/// ????? ????????? ?? ???? 

		/// </summary> 

		public TuningMode	TuningMode; 

 

 

		/// <summary> 

		/// ????????? ??????? 

		/// </summary> 

		public short	MarkerWork; 

 

 

		/// <summary> 

		/// ????? ????????????? ????? ?? CIS 

		/// </summary> 

		public DirtDetection	DirtDetection; 

 

 

		/// <summary> 

		/// ????? ?????? ? ????????? 

		/// </summary> 

		public short	OfflineMode; 

 

 

		/// <summary> 

		/// ?????????/?????????? ??????? ? ?????? ???????? 


		/// </summary> 

		public Lamps	Lamps; 

 

 

		/// <summary> 

		/// ?????? 

		/// </summary> 

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=21)] 

		public short[]	reserv; 

	} 

 

 

	/// <summary> 

	/// ??? ????????? ??????? ?????????? ????????? ?? ?????????? ????? 

	/// </summary> 

	public enum PropName : short 

	{ 

		WorkMode			= 0x00, 

		DpiX0				= 0x01, 

		DpiY0				= 0x02, 

		DpiX1				= 0x03, 

		DpiY1				= 0x04, 

		DoubleSheet			= 0x05, 

		Marker				= 0x06, 

		DoubleSheetLevelL	= 0x07, 

		WhiteCoeff			= 0x08, 

		BinaryThreshold0	= 0x09, 

		BinaryThreshold1	= 0x0A, 

		MinSheetLength		= 0x0B, 

		MaxSheetLength		= 0x0C, 

		DoubleSheetLevelR	= 0x0D, 

		TuningMode			= 0x0E, 

		MarkerWork			= 0x0F, 

		DirtDetection		= 0x10, 

		OfflineMode			= 0x11, 

		Lamps				= 0x12, 

	} 

 

 

	/// <summary> 

	/// ????????? ?????????? ??????? 

	/// </summary> 

	struct MotorData 

	{ 

		/// <summary> 

		/// ????????? ?????????? ??????? 

		/// </summary> 

		/// <param name="number">????? ??????</param> 

		/// <param name="onoff">????????/?????????</param> 

		/// <param name="dir">???????????</param> 


		/// <param name="step">????? ?????</param> 

		public MotorData( short number, short onoff, short dir, short step ) 

		{ 

			this.number = number; 

			this.onoff = onoff; 

			this.dir = dir; 

			this.step = step; 

		} 

 

 

		/// <summary> 

		/// ????????? ?????????? ??????? 

		/// </summary> 

		/// <param name="data">?????? ?????????? ?? ????????? ????????</param> 

		public MotorData( byte[] data ) 

		{ 

			BinaryReader br = new System.IO.BinaryReader( new MemoryStream( data ) ); 

			this.number = br.ReadInt16(); 

			this.onoff = br.ReadInt16(); 

			this.dir = br.ReadInt16(); 

			this.step = br.ReadInt16(); 

			br.Close(); 

		} 

 

 

		/// <summary> 

		/// ?????? ??? ??????? ????????? ???????? 

		/// </summary> 

		public byte[] Data 

		{ 

			get 

			{ 

				MemoryStream ms = new MemoryStream( 8 ); 

				BinaryWriter bw = new System.IO.BinaryWriter( ms ); 

				bw.Write(number); 

				bw.Write(onoff); 

				bw.Write(dir); 

				bw.Write(step); 

				bw.Flush(); 

				byte[] data = ms.ToArray(); 

				bw.Close(); 

				return data; 

			} 

		} 

 

 

		/// <summary> 

		/// ????? ?????? 

		/// </summary> 

		short	number; 


 
 

		/// <summary> 

		/// ????????/????????? 

		/// </summary> 

		short	onoff; 

 

 

		/// <summary> 

		/// ??????????? 

		/// </summary> 

		short	dir; 

 

 

		/// <summary> 

		/// ????? ????? 

		/// </summary> 

		short	step; 

	} 

}

