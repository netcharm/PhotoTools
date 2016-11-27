using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NetCharm.Image
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum ContentMaskMode
    {
        Alpha = 0,
        TopLeft = 1,
        BottomRight = 2
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum CropMode
    {
        Selection = 0,
        Opaque = 1,
        TopLeft = 2,
        BottomRight = 3,
        AspectRatio = 4
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum SideType
    {
        //
        // 摘要:
        //     该控件未锚定到其容器的任何边缘。
        None = 0,
        //
        // 摘要:
        //     该控件锚定到其容器的上边缘。
        Top = 1,
        //
        // 摘要:
        //     该控件锚定到其容器的下边缘。
        Bottom = 2,
        //
        // 摘要:
        //     该控件锚定到其容器的左边缘。
        Left = 4,
        //
        // 摘要:
        //     该控件锚定到其容器的右边缘。
        Right = 8
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum CornerRegionType
    {
        None = 0,
        //
        // 摘要:
        //     内容在垂直方向上顶部对齐，在水平方向上左边对齐。
        TopLeft = 1,
        //
        // 摘要:
        //     内容在垂直方向上顶部对齐，在水平方向上居中对齐。
        TopCenter = 2,
        //
        // 摘要:
        //     内容在垂直方向上顶部对齐，在水平方向上右边对齐。
        TopRight = 4,
        //
        // 摘要:
        //     内容在垂直方向上中间对齐，在水平方向上左边对齐。
        MiddleLeft = 16,
        //
        // 摘要:
        //     内容在垂直方向上中间对齐，在水平方向上居中对齐。
        MiddleCenter = 32,
        //
        // 摘要:
        //     内容在垂直方向上中间对齐，在水平方向上右边对齐。
        MiddleRight = 64,
        //
        // 摘要:
        //     内容在垂直方向上底边对齐，在水平方向上左边对齐。
        BottomLeft = 256,
        //
        // 摘要:
        //     内容在垂直方向上底边对齐，在水平方向上居中对齐。
        BottomCenter = 512,
        //
        // 摘要:
        //     内容在垂直方向上底边对齐，在水平方向上右边对齐。
        BottomRight = 1024
    }

    /// <summary>
    /// These constants come from the CIPA DC-008 standard for EXIF 2.3
    /// </summary>
    [Serializable]
    public static class EXIF
    {
        #region EXIF Data Type
        /// <summary>
        /// 
        /// </summary>
        public static class TagType
        {
            /// <summary> 
            /// Specifies that the value data member is an array of bytes.
            /// </summary>
            public const short Byte = 1;

            /// <summary> 
            /// Specifies that the value data member is a null-terminated ASCII string. If you set the type data member of a PropertyItem object to PropertyTagTypeASCII, you should set the length data member to the length of the string including the NULL terminator. For example, the string HELLO would have a length of 6.
            /// </summary>
            public const short ASCII = 2;

            /// <summary> 
            /// Specifies that the value data member is an array of unsigned short (16-bit) integers.
            /// </summary>
            public const short Short = 3;

            /// <summary> 
            /// Specifies that the value data member is an array of unsigned long (32-bit) integers.
            /// </summary>
            public const short Long = 4;

            /// <summary> 
            /// Specifies that the value data member is an array of pairs of unsigned long integers. Each pair represents a fraction; the first integer is the numerator and the second integer is the denominator.
            /// </summary>
            public const short Rational = 5;

            /// <summary> 
            /// Specifies that the value data member is an array of bytes that can hold values of any data type. 
            /// </summary>
            public const short Undefined = 7;

            /// <summary> 
            /// Specifies that the value data member is an array of signed long (32-bit) integers.
            /// </summary>
            public const short SLONG = 9;

            /// <summary> 
            /// Specifies that the value data member is an array of pairs of signed long integers. Each pair represents a fraction; the first integer is the numerator and the second integer is the denominator.
            /// </summary>
            public const short SRational = 10;
        }
        #endregion

        #region EXIF Tag ID
        /// <summary>
        /// 
        /// </summary>
        public static class TagID
        {
            /// <summary>
            /// Exit Property Tag ID Define
            /// </summary>
            public const int GpsVer = 0x0000;
            public const int GpsLatitudeRef = 0x0001;
            public const int GpsLatitude = 0x0002;
            public const int GpsLongitudeRef = 0x0003;
            public const int GpsLongitude = 0x0004;
            public const int GpsAltitudeRef = 0x0005;
            public const int GpsAltitude = 0x0006;
            public const int GpsGpsTime = 0x0007;
            public const int GpsGpsSatellites = 0x0008;
            public const int GpsGpsStatus = 0x0009;
            public const int GpsGpsMeasureMode = 0x000A;
            public const int GpsGpsDop = 0x000B;
            public const int GpsSpeedRef = 0x000C;
            public const int GpsSpeed = 0x000D;
            public const int GpsTrackRef = 0x000E;
            public const int GpsTrack = 0x000F;
            public const int GpsImgDirRef = 0x0010;
            public const int GpsImgDir = 0x0011;
            public const int GpsMapDatum = 0x0012;
            public const int GpsDestLatRef = 0x0013;
            public const int GpsDestLat = 0x0014;
            public const int GpsDestLongRef = 0x0015;
            public const int GpsDestLong = 0x0016;
            public const int GpsDestBearRef = 0x0017;
            public const int GpsDestBear = 0x0018;
            public const int GpsDestDistRef = 0x0019;
            public const int GpsDestDist = 0x001A;
            public const int NewSubfileType = 0x00FE;
            public const int SubfileType = 0x00FF;
            public const int ImageWidth = 0x0100;
            public const int ImageHeight = 0x0101;
            public const int BitsPerSample = 0x0102;
            public const int Compression = 0x0103;
            public const int PhotometricInterp = 0x0106;
            public const int ThreshHolding = 0x0107;
            public const int CellWidth = 0x0108;
            public const int CellHeight = 0x0109;
            public const int FillOrder = 0x010A;
            public const int DocumentName = 0x010D;
            public const int ImageDescription = 0x010E;
            public const int EquipMake = 0x010F;
            public const int EquipModel = 0x0110;
            public const int StripOffsets = 0x0111;
            public const int Orientation = 0x0112;
            public const int SamplesPerPixel = 0x0115;
            public const int RowsPerStrip = 0x0116;
            public const int StripBytesCount = 0x0117;
            public const int MinSampleValue = 0x0118;
            public const int MaxSampleValue = 0x0119;
            public const int XResolution = 0x011A;
            public const int YResolution = 0x011B;
            public const int PlanarConfig = 0x011C;
            public const int PageName = 0x011D;
            public const int XPosition = 0x011E;
            public const int YPosition = 0x011F;
            public const int FreeOffset = 0x0120;
            public const int FreeByteCounts = 0x0121;
            public const int GrayResponseUnit = 0x0122;
            public const int GrayResponseCurve = 0x0123;
            public const int T4Option = 0x0124;
            public const int T6Option = 0x0125;
            public const int ResolutionUnit = 0x0128;
            public const int PageNumber = 0x0129;
            public const int TransferFunction = 0x012D;
            public const int SoftwareUsed = 0x0131;
            public const int DateTime = 0x0132;
            public const int Artist = 0x013B;
            public const int HostComputer = 0x013C;
            public const int Predictor = 0x013D;
            public const int WhitePoint = 0x013E;
            public const int PrimaryChromaticities = 0x013F;
            public const int ColorMap = 0x0140;
            public const int HalftoneHints = 0x0141;
            public const int TileWidth = 0x0142;
            public const int TileLength = 0x0143;
            public const int TileOffset = 0x0144;
            public const int TileByteCounts = 0x0145;
            public const int InkSet = 0x014C;
            public const int InkNames = 0x014D;
            public const int NumberOfInks = 0x014E;
            public const int DotRange = 0x0150;
            public const int TargetPrinter = 0x0151;
            public const int ExtraSamples = 0x0152;
            public const int SampleFormat = 0x0153;
            public const int SMinSampleValue = 0x0154;
            public const int SMaxSampleValue = 0x0155;
            public const int TransferRange = 0x0156;
            public const int JPEGProc = 0x0200;
            public const int JPEGInterFormat = 0x0201;
            public const int JPEGInterLength = 0x0202;
            public const int JPEGRestartInterval = 0x0203;
            public const int JPEGLosslessPredictors = 0x0205;
            public const int JPEGPointTransforms = 0x0206;
            public const int JPEGQTables = 0x0207;
            public const int JPEGDCTables = 0x0208;
            public const int JPEGACTables = 0x0209;
            public const int YCbCrCoefficients = 0x0211;
            public const int YCbCrSubsampling = 0x0212;
            public const int YCbCrPositioning = 0x0213;
            public const int REFBlackWhite = 0x0214;
            public const int Gamma = 0x0301;
            public const int ICCProfileDescriptor = 0x0302;
            public const int SRGBRenderingIntent = 0x0303;
            public const int ImageTitle = 0x0320;
            public const int ResolutionXUnit = 0x5001;
            public const int ResolutionYUnit = 0x5002;
            public const int ResolutionXLengthUnit = 0x5003;
            public const int ResolutionYLengthUnit = 0x5004;
            public const int PrintFlags = 0x5005;
            public const int PrintFlagsVersion = 0x5006;
            public const int PrintFlagsCrop = 0x5007;
            public const int PrintFlagsBleedWidth = 0x5008;
            public const int PrintFlagsBleedWidthScale = 0x5009;
            public const int HalftoneLPI = 0x500A;
            public const int HalftoneLPIUnit = 0x500B;
            public const int HalftoneDegree = 0x500C;
            public const int HalftoneShape = 0x500D;
            public const int HalftoneMisc = 0x500E;
            public const int HalftoneScreen = 0x500F;
            public const int JPEGQuality = 0x5010;
            public const int GridSize = 0x5011;
            public const int ThumbnailFormat = 0x5012;
            public const int ThumbnailWidth = 0x5013;
            public const int ThumbnailHeight = 0x5014;
            public const int ThumbnailColorDepth = 0x5015;
            public const int ThumbnailPlanes = 0x5016;
            public const int ThumbnailRawBytes = 0x5017;
            public const int ThumbnailSize = 0x5018;
            public const int ThumbnailCompressedSize = 0x5019;
            public const int ColorTransferFunction = 0x501A;
            public const int ThumbnailData = 0x501B;
            public const int ThumbnailImageWidth = 0x5020;
            public const int ThumbnailImageHeight = 0x5021;
            public const int ThumbnailBitsPerSample = 0x5022;
            public const int ThumbnailCompression = 0x5023;
            public const int ThumbnailPhotometricInterp = 0x5024;
            public const int ThumbnailImageDescription = 0x5025;
            public const int ThumbnailEquipMake = 0x5026;
            public const int ThumbnailEquipModel = 0x5027;
            public const int ThumbnailStripOffsets = 0x5028;
            public const int ThumbnailOrientation = 0x5029;
            public const int ThumbnailSamplesPerPixel = 0x502A;
            public const int ThumbnailRowsPerStrip = 0x502B;
            public const int ThumbnailStripBytesCount = 0x502C;
            public const int ThumbnailResolutionX = 0x502D;
            public const int ThumbnailResolutionY = 0x502E;
            public const int ThumbnailPlanarConfig = 0x502F;
            public const int ThumbnailResolutionUnit = 0x5030;
            public const int ThumbnailTransferFunction = 0x5031;
            public const int ThumbnailSoftwareUsed = 0x5032;
            public const int ThumbnailDateTime = 0x5033;
            public const int ThumbnailArtist = 0x5034;
            public const int ThumbnailWhitePoint = 0x5035;
            public const int ThumbnailPrimaryChromaticities = 0x5036;
            public const int ThumbnailYCbCrCoefficients = 0x5037;
            public const int ThumbnailYCbCrSubsampling = 0x5038;
            public const int ThumbnailYCbCrPositioning = 0x5039;
            public const int ThumbnailRefBlackWhite = 0x503A;
            public const int ThumbnailCopyRight = 0x503B;
            public const int LuminanceTable = 0x5090;
            public const int ChrominanceTable = 0x5091;
            public const int FrameDelay = 0x5100;
            public const int LoopCount = 0x5101;
            public const int GlobalPalette = 0x5102;
            public const int IndexBackground = 0x5103;
            public const int IndexTransparent = 0x5104;
            public const int PixelUnit = 0x5110;
            public const int PixelPerUnitX = 0x5111;
            public const int PixelPerUnitY = 0x5112;
            public const int PaletteHistogram = 0x5113;
            public const int Copyright = 0x8298;
            public const int ExifExposureTime = 0x829A;
            public const int ExifFNumber = 0x829D;
            public const int ExifIFD = 0x8769;
            public const int ICCProfile = 0x8773;
            public const int ExifExposureProg = 0x8822;
            public const int ExifSpectralSense = 0x8824;
            public const int GpsIFD = 0x8825;
            public const int ExifISOSpeed = 0x8827;
            public const int ExifOECF = 0x8828;
            public const int ExifVer = 0x9000;
            public const int ExifDTOrig = 0x9003;
            public const int ExifDTDigitized = 0x9004;
            public const int ExifCompConfig = 0x9101;
            public const int ExifCompBPP = 0x9102;
            public const int ExifShutterSpeed = 0x9201;
            public const int ExifAperture = 0x9202;
            public const int ExifBrightness = 0x9203;
            public const int ExifExposureBias = 0x9204;
            public const int ExifMaxAperture = 0x9205;
            public const int ExifSubjectDist = 0x9206;
            public const int ExifMeteringMode = 0x9207;
            public const int ExifLightSource = 0x9208;
            public const int ExifFlash = 0x9209;
            public const int ExifFocalLength = 0x920A;
            public const int ExifMakerNote = 0x927C;
            public const int ExifUserComment = 0x9286;
            public const int ExifDTSubsec = 0x9290;
            public const int ExifDTOrigSS = 0x9291;
            public const int ExifDTDigSS = 0x9292;
            public const int ExifFPXVer = 0xA000;
            public const int ExifColorSpace = 0xA001;
            public const int ExifPixXDim = 0xA002;
            public const int ExifPixYDim = 0xA003;
            public const int ExifRelatedWav = 0xA004;
            public const int ExifInterop = 0xA005;
            public const int ExifFlashEnergy = 0xA20B;
            public const int ExifSpatialFR = 0xA20C;
            public const int ExifFocalXRes = 0xA20E;
            public const int ExifFocalYRes = 0xA20F;
            public const int ExifFocalResUnit = 0xA210;
            public const int ExifSubjectLoc = 0xA214;
            public const int ExifExposureIndex = 0xA215;
            public const int ExifSensingMethod = 0xA217;
            public const int ExifFileSource = 0xA300;
            public const int ExifSceneType = 0xA301;
            public const int ExifCfaPattern = 0xA302;

            // extension tags
            public const int ExifCustomRendered = 0xA401;
            public const int ExifExposureMode = 0xA402;
            public const int ExifWhiteBalance = 0xA403;
            public const int ExifDigitalZoomRatio = 0xA404;
            public const int ExifFocalLengthIn35mmFilm = 0xA405;
            public const int ExifSceneCaptureType = 0xA406;
            public const int ExifGainControl = 0xA407;
            public const int ExifContrast = 0xA408;
            public const int ExifSaturation = 0xA409;
            public const int ExifSharpness = 0xA40A;
            public const int ExifSubjectDistanceRange = 0xA40C;

            // XP System Property
            public const int ExifXPTitle = 0x9C9B; // 40091;
            public const int ExifXPComment = 0x9C9C; // 40092;
            public const int ExifXPAuthor = 0x9C9D; // 40093;
            public const int ExifXPKeywords = 0x9C9E; // 40094;
            public const int ExifXPSubject = 0x9C9F; // 40095;
        }
        #endregion

        #region EXIF file type
        public static ImageFormat[] ExifFormat = new ImageFormat[]
        {
            ImageFormat.Exif,
            ImageFormat.Jpeg,
            ImageFormat.Tiff,
            ImageFormat.MemoryBmp
        };
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public static void Add( System.Drawing.Image image, int id, short type, byte[] value )
        {
            try
            {
                if ( image.PropertyItems.Length > 0 )
                {
                    PropertyItem pi = image.PropertyItems[0];
                    pi.Id = id;
                    pi.Type = type;
                    pi.Len = value.Length;
                    pi.Value = value;
                    image.SetPropertyItem( pi );
                }
                else
                {
                    //PropertyItem pi = image.GetPropertyItem(id);
                    PropertyItem pi = (PropertyItem) FormatterServices.GetUninitializedObject( typeof( PropertyItem ) );
                    pi.Id = id;
                    pi.Type = type;
                    pi.Len = value.Length;
                    pi.Value = value;
                    image.SetPropertyItem( pi );
                }
            }
            catch ( ArgumentException )
            {
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="id"></param>
        public static void Remove( System.Drawing.Image image, int id )
        {
            if ( image is System.Drawing.Image && ExifFormat.Contains( image.RawFormat ) )
            {
                image.RemovePropertyItem( id );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="id"></param>
        public static void Change( System.Drawing.Image image, int id, byte[] value )
        {
            if ( image is System.Drawing.Image && ExifFormat.Contains( image.RawFormat ) )
            {
                var pi = image.GetPropertyItem( id );
                pi.Value = value;
                image.SetPropertyItem( pi );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static System.Drawing.Image Clone( System.Drawing.Image src, System.Drawing.Image dst )
        {
            if ( src is System.Drawing.Image && dst is System.Drawing.Image )
            {
                if ( ExifFormat.Contains( src.RawFormat ) && ExifFormat.Contains( dst.RawFormat ) )
                {
                    foreach ( var item in src.PropertyItems )
                    {
                        dst.SetPropertyItem( item );
                    }
                }
            }
            return ( dst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public static void Remove( System.Drawing.Image image )
        {
            if ( image is System.Drawing.Image && ExifFormat.Contains( image.RawFormat ) )
            {
                foreach ( var item in image.PropertyItems )
                {
                    image.RemovePropertyItem( item.Id );
                }
            }
        }

    }

    ///
    /// all used the bitmapmetadata.getquery("[INSERT BELOW]")... warning data type that is returned is not always the same
    /// "they" could have made it much easier...I would still have some hair left in if they did !!
    ///
    [Serializable]
    public static class META
    {
        #region Query Path
        public static class TagID
        {
            // IPTC Tags
            public const string IptcByline = "/app13/irb/8bimiptc/iptc/{str=By-line}";
            public const string IptcBylineTitle = "/app13/irb/8bimiptc/iptc/{str=By-line Title}";
            public const string IptcCaption = "/app13/irb/8bimiptc/iptc/{str=Caption}";
            public const string IptcCity = "/app13/irb/8bimiptc/iptc/{str=City}";
            public const string IptcCopyrightNotice = "/app13/irb/8bimiptc/iptc/{str=Copyright Notice}";
            public const string IptcCountryPrimaryLocationName = "/app13/irb/8bimiptc/iptc/{str=Country/Primary Location Name}";
            public const string IptcCredit = "/app13/irb/8bimiptc/iptc/{str=Credit}";
            public const string IptcDateCreated = "/app13/irb/8bimiptc/iptc/{str=Date Created}";
            public const string IptcDescription = "/app13/irb/8bimiptc/iptc/{str=Description}";
            public const string IptcHeadline = "/app13/irb/8bimiptc/iptc/{str=Headline}";
            public const string IptcKeywords = "/app13/irb/8bimiptc/iptc/{str=Keywords}";
            public const string IptcObjectName = "/app13/irb/8bimiptc/iptc/{str=Object Name}";
            public const string IptcOriginalTransmissionReference = "/app13/irb/8bimiptc/iptc/{str=Original Transmission Reference}";
            public const string IptcRecordVersion = "/app13/irb/8bimiptc/iptc/{str=Record Version}";
            public const string IptcSource = "/app13/irb/8bimiptc/iptc/{str=Source}";
            public const string IptcState = "/app13/irb/8bimiptc/iptc/{str=Province/State}";
            public const string IptcSpecialInstructions = "/app13/irb/8bimiptc/iptc/{str=Special Instructions}";
            public const string IptcWriterEditor = "/app13/irb/8bimiptc/iptc/{str=Writer/Editor}";

            // EXIF Tags
            public const string ExifExposureTime = "/app1/ifd/exif/{ushort=33434}";
            public const string ExifFNumber = "/app1/ifd/exif/{ushort=33437}";
            public const string ExifExposureProg = "/app1/ifd/exif/{ushort=34850}";
            public const string ExifISOSpeed = "/app1/ifd/exif/{ushort=34855}";
            public const string ExifDTOrig = "/app1/ifd/exif/{ushort=36867}";
            public const string ExifDTDigitized = "/app1/ifd/exif/{ushort=36868}";
            public const string ExifShutterSpeed = "/app1/ifd/exif/{ushort=37377}";
            public const string ExifAperture = "/app1/ifd/exif/{ushort=37378}";
            public const string ExifExposureBias = "/app1/ifd/exif/{ushort=37380}";
            public const string ExifMaxAperture = "/app1/ifd/exif/{ushort=37381}";
            public const string ExifMeteringMode = "/app1/ifd/exif/{ushort=37383}";
            public const string ExifLightSource = "/app1/ifd/exif/{ushort=37384}";
            public const string ExifFlash = "/app1/ifd/exif/{ushort=37385}";
            public const string ExifFocalLength = "/app1/ifd/exif/{ushort=37386}";
            public const string ExifUserComment = "/app1/ifd/exif/{ushort=37510}";
            public const string ExifDTSubsec = "/app1/ifd/exif/{ushort=37520}";
            public const string ExifDTOrigSS = "/app1/ifd/exif/{ushort=37521}";
            public const string ExifDTDigSS = "/app1/ifd/exif/{ushort=37522}";
            public const string ExifColorSpace = "/app1/ifd/exif/{ushort=40961}";
            public const string ExifPixXDim = "/app1/ifd/exif/{ushort=40962}";
            public const string ExifPixYDim = "/app1/ifd/exif/{ushort=40963}";
            public const string ExifSensingMethod = "/app1/ifd/exif/{ushort=41495}";
            public const string ExifFileSource = "/app1/ifd/exif/{ushort=41728}";
            public const string ExifSceneType = "/app1/ifd/exif/{ushort=41729}";
            public const string ExifCfaPattern = "/app1/ifd/exif/{ushort=41730}";
            public const string ExifCustomRendered = "/app1/ifd/exif/{ushort=41985}";
            public const string ExifExposureMode = "/app1/ifd/exif/{ushort=41986}";
            public const string ExifWhiteBalance = "/app1/ifd/exif/{ushort=41987}";
            public const string ExifDigitalZoomRatio = "/app1/ifd/exif/{ushort=41988}";
            public const string ExifFocalLengthIn35mmFilm = "/app1/ifd/exif/{ushort=41989}";
            public const string ExifSceneCaptureType = "/app1/ifd/exif/{ushort=41990}";
            public const string ExifGainControl = "/app1/ifd/exif/{ushort=41991}";
            public const string ExifContrast = "/app1/ifd/exif/{ushort=41992}";
            public const string ExifSaturation = "/app1/ifd/exif/{ushort=41993}";
            public const string ExifSharpness = "/app1/ifd/exif/{ushort=41994}";
            public const string ExifSubjectDistanceRange = "/app1/ifd/exif/{ushort=41996}";

            // Exif others
            public const string ExifTitle = "/app1/ifd/{ushort=270}";
            public const string ExifDateTime = "/app1/ifd/{ushort=306}";
            public const string ExifArtist = "/app1/ifd/{ushort=315}";
            public const string ExifCopyright = "/app1/ifd/{ushort=33432}";

            // GPS Info
            public const string ExifGpsLatitudeRef = "/app1/ifd/Gps/subifd:{uint=1}";
            public const string ExifGpsLatitude = "/app1/ifd/Gps/subifd:{uint=2}";
            public const string ExifGpsLongitudeRef = "/app1/ifd/Gps/subifd:{uint=3}";
            public const string ExifGpsLongitude = "/app1/ifd/Gps/subifd:{uint=4}";
            public const string ExifGpsAltitudeRef = "/app1/ifd/Gps/subifd:{uint=5}";
            public const string ExifGpsAltitude = "/app1/ifd/Gps/subifd:{uint=6}";
            public const string ExifGpsTimeStamp = "/app1/ifd/Gps/subifd:{uint=7}";
            public const string ExifGpsSatellites = "/app1/ifd/Gps/subifd:{uint=8}";
            public const string ExifGpsStatus = "/app1/ifd/Gps/subifd:{uint=9}";
            public const string ExifGpsMeasureMode = "/app1/ifd/Gps/subifd:{uint=10}";
            public const string ExifGpsDop = "/app1/ifd/Gps/subifd:{uint=11}";
            public const string ExifGpsSpeedRef = "/app1/ifd/Gps/subifd:{uint=12}";
            public const string ExifGpsSpeed = "/app1/ifd/Gps/subifd:{uint=13}";
            public const string ExifGpsTrackRef = "/app1/ifd/Gps/subifd:{uint=14}";
            public const string ExifGpsTrack = "/app1/ifd/Gps/subifd:{uint=15}";
            public const string ExifGpsImgDirectionRef = "/app1/ifd/Gps/subifd:{uint=16}";
            public const string ExifGpsImgDirection = "/app1/ifd/Gps/subifd:{uint=17}";
            public const string ExifGpsMapDatum = "/app1/ifd/Gps/subifd:{uint=18}";
            public const string ExifGpsDestLatitudeRef = "/app1/ifd/Gps/subifd:{uint=19}";
            public const string ExifGpsDestLatitude = "/app1/ifd/Gps/subifd:{uint=20}";
            public const string ExifGpsDestLongitudeRef = "/app1/ifd/Gps/subifd:{uint=21}";
            public const string ExifGpsDestLongitude = "/app1/ifd/Gps/subifd:{uint=22}";
            public const string ExifGpsDestBearingRef = "/app1/ifd/Gps/subifd:{uint=23}";
            public const string ExifGpsDestBearing = "/app1/ifd/Gps/subifd:{uint=24}";
            public const string ExifGpsDestDistanceRef = "/app1/ifd/Gps/subifd:{uint=25}";
            public const string ExifGpsDestDistance = "/app1/ifd/Gps/subifd:{uint=26}";
            public const string ExifGpsProcessingMethod = "/app1/ifd/Gps/subifd:{uint=27}";
            public const string ExifGpsAreaInformation = "/app1/ifd/Gps/subifd:{uint=28}";
            public const string ExifGpsDateStamp = "/app1/ifd/Gps/subifd:{uint=29}";
            public const string ExifGpsDifferential = "/app1/ifd/Gps/subifd:{uint=30}";

            // alt query string
            public const string IptcKeywordsAlt = "/app13/{ushort=0}/{ulonglong=61857348781060}/iptc/{str=Keywords}";

            // XP System Property
            public const string ExifImageDescription = "/app1/ifd/{ushort=270}";
            public const string ExifXPTitle = "/app1/ifd/exif:{ushort=40091}";
            public const string ExifXPComment = "/app1/ifd/exif:{ushort=40092}";
            public const string ExifXPAuthor = "/app1/ifd/exif:{ushort=40093}";
            public const string ExifXPKeywords = "/app1/ifd/exif:{ushort=40094}";
            public const string ExifXPSubject = "/app1/ifd/exif:{ushort=40095}";

            // XMP Query Path
            public const string XmpSubject = "/xmp/dc:subject";

            // Extentions for metadata padding
            // Queries for the EXIF, IFD & XMP Padding
            public const string paddingExif = "/app1/ifd/exif/PaddingSchema:Padding";
            public const string paddingIfd = "/app1/ifd/PaddingSchema:Padding";
            public const string paddingXmp = "/xmp/PaddingSchema:Padding";    // Queries for the EXIF, IFD & XMP Padding
            public const string paddingIptc = "/app13/irb/8bimiptc/iptc/PaddingSchema:Padding";
            public const string padding8bimiptc = "/app13/irb/8bimiptc/PaddingSchema:Padding";
            public const string paddinIrb = "/app13/irb//PaddingSchema:Padding";
            public const string paddingApp13 = "/app13/PaddingSchema:Padding";
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CornerRegion
    {
        #region public properties
        private RectangleF _tl;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF TopLeft
        {
            get { return ( _tl ); }
        }

        private RectangleF _tc;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF TopCenter
        {
            get { return ( _tc ); }
        }

        private RectangleF _tr;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF TopRight
        {
            get { return ( _tr ); }
        }

        private RectangleF _ml;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF MiddleLeft
        {
            get { return ( _ml ); }
        }

        private RectangleF _mc;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF MiddleCenter
        {
            get { return ( _mc ); }
        }

        private RectangleF _mr;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF MiddleRight
        {
            get { return ( _mr ); }
        }

        private RectangleF _bl;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF BottomLeft
        {
            get { return ( _bl ); }
        }

        private RectangleF _bc;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF BottomCenter
        {
            get { return ( _bc ); }
        }

        private RectangleF _br;
        /// <summary>
        /// 
        /// </summary>
        public RectangleF BottomRight
        {
            get { return ( _br ); }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public CornerRegion()
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="ratio"></param>
        public CornerRegion( Rectangle region, float ratio = 8 )
        {
            _tl = new RectangleF( region.Left - ratio, region.Top - ratio, ratio * 2, ratio * 2 );
            _tc = new RectangleF( region.Left + ratio, region.Top - ratio, region.Width - ratio * 2, ratio * 2 );
            _tr = new RectangleF( region.Right - ratio, region.Top - ratio, ratio * 2, ratio * 2 );

            _ml = new RectangleF( region.Left - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );
            _mc = new RectangleF( region.Left + ratio, region.Top + ratio, region.Width - ratio * 2, region.Height - ratio * 2 );
            _mr = new RectangleF( region.Right - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );

            _bl = new RectangleF( region.Left - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
            _bc = new RectangleF( region.Left + ratio, region.Bottom - ratio, region.Width - ratio * 2, ratio * 2 );
            _br = new RectangleF( region.Right - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="ratio"></param>
        public CornerRegion( RectangleF region, float ratio = 8 )
        {
            _tl = new RectangleF( region.Left - ratio, region.Top - ratio, ratio * 2, ratio * 2 );
            _tc = new RectangleF( region.Left + ratio, region.Top - ratio, region.Width - ratio * 2, ratio * 2 );
            _tr = new RectangleF( region.Right - ratio, region.Top - ratio, ratio * 2, ratio * 2 );

            _ml = new RectangleF( region.Left - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );
            _mc = new RectangleF( region.Left + ratio, region.Top + ratio, region.Width - ratio * 2, region.Height - ratio * 2 );
            _mr = new RectangleF( region.Right - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );

            _bl = new RectangleF( region.Left - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
            _bc = new RectangleF( region.Left + ratio, region.Bottom - ratio, region.Width - ratio * 2, ratio * 2 );
            _br = new RectangleF( region.Right - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public CornerRegionType GetRegion( PointF p )
        {
            if ( _tl.Contains( p ) )
                return ( CornerRegionType.TopLeft );
            else if ( _tc.Contains( p ) )
                return ( CornerRegionType.TopCenter );
            else if ( _tr.Contains( p ) )
                return ( CornerRegionType.TopRight );
            else if ( _ml.Contains( p ) )
                return ( CornerRegionType.MiddleLeft );
            else if ( _mc.Contains( p ) )
                return ( CornerRegionType.MiddleCenter );
            else if ( _mr.Contains( p ) )
                return ( CornerRegionType.MiddleRight );
            else if ( _bl.Contains( p ) )
                return ( CornerRegionType.BottomLeft );
            else if ( _bc.Contains( p ) )
                return ( CornerRegionType.BottomCenter );
            else if ( _br.Contains( p ) )
                return ( CornerRegionType.BottomRight );

            return ( CornerRegionType.None );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MetaInfo
    {
        public List<PropertyItem> EXIF;
        public Dictionary<string, string> IPTC;
        public System.Windows.Media.Imaging.BitmapMetadata Meta;
    }

    [Serializable]
    public class SaveOption
    {
        public bool KeepExif = true;
        public bool Overwrite = true;
        public int Quality = 90;
        public string RenameMask=string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class CircularStack<T>
    {
        private List<T> _items = new List<T>();
        private int _limit = 0;

        public int Count
        {
            get { return ( _items is List<T> ? _items.Count : -1 ); }
        }

        public CircularStack()
        {
            _items = new List<T>();
        }

        public CircularStack( int limit )
        {
            _limit = limit;
            _items = new List<T>( limit );
        }

        public void Push( T item )
        {
            _items.Add( item );
            if ( _items.Count >= _limit )
            {
                _items.RemoveAt( 0 );
            }
        }

        public T Pop()
        {
            if ( _items is List<T> )
            {
                if ( Count > 0 )
                {
                    var item = _items.Last();
                    _items.Remove( _items.Last() );
                    return ( item );
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public T Peek()
        {
            return ( _items.Last() );
        }

        public void Clear()
        {
            _items.Clear();
        }

    }


}
