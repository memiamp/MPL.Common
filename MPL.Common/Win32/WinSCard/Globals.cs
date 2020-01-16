using System;

namespace MPL.Common.Win32.WinSCard
{
    #region Declarations
    #region _Enumerations_
    /// <summary>
    /// An enumeration that defines a context scope for a winscard operation.
    /// </summary>
    internal enum SCardContextScope : uint
    {
        /// <summary>
        /// Operations are performed within the scope of the system.
        /// </summary>
        SCOPE_SYSTEM = 2,

        /// <summary>
        /// Operations are performed within the scope of the terminal.
        /// </summary>
        SCOPE_TERMINAL = 1,

        /// <summary>
        /// Operations are performed within the scope of the user.
        /// </summary>
        SCOPE_USER = 0
    }

    /// <summary>
    /// An enumeration that defines the action to take on a card when closed.
    /// </summary>
    public enum SCardDisposition : uint
    {
        /// <summary>
        /// Eject the card.
        /// </summary>
        EjectCard = 3,

        /// <summary>
        /// Do not do anything specific.
        /// </summary>
        LeaveCard = 0,

        /// <summary>
        /// Reset the card.
        /// </summary>
        ResetCard = 1,

        /// <summary>
        /// Power down the card.
        /// </summary>
        UnpowerCard = 2
    }

    /// <summary>
    /// An enumeration that defines the protocols for an winscard card operation.
    /// </summary>
    [Flags()]
    public enum SCardProtocol : uint
    {
        /// <summary>
        /// No protocol is defined. May only be used when the reader share mode is Direct.
        /// </summary>
        None = 0,

        /// <summary>
        /// The protocal is raw.
        /// </summary>
        Raw = 4,

        /// <summary>
        /// The protocol is T=0.
        /// </summary>
        T0 = 1,

        /// <summary>
        /// The protocol is T=1.
        /// </summary>
        T1 = 2,

        /// <summary>
        /// The protocol is T=15.
        /// </summary>
        T15 = 8
    }

    /// <summary>
    /// An enumeration that defines a reader event state.
    /// </summary>
    [Flags()]
    public enum SCardReaderEventState : uint
    {
        /// <summary>
        /// There is a card in the reader with an ATR that matches one of the target cards.
        /// </summary>
        SCARD_STATE_ATRMATCH = 0x00000040,

        /// <summary>
        /// There is a difference between the state believed by the application, and the state known by Smart Cards for Windows.
        /// </summary>
        SCARD_STATE_CHANGED = 0x00000002,

        /// <summary>
        /// There is no card in the reader.
        /// </summary>
        SCARD_STATE_EMPTY = 0x00000010,

        /// <summary>
        /// The card in the reader is allocated for exclusive use by another application.
        /// </summary>
        SCARD_STATE_EXCLUSIVE = 0x00000080,

        /// <summary>
        /// The application requested that this reader be ignored.
        /// </summary>
        SCARD_STATE_IGNORE = 0x00000001,

        /// <summary>
        /// The card in the reader is in use by one or more other applications, but it can be connected to in shared mode.
        /// </summary>
        SCARD_STATE_INUSE = 0x00000100,

        /// <summary>
        /// The card in the reader is unresponsive or is not supported by the reader or software.
        /// </summary>
        SCARD_STATE_MUTE = 0x00000200,

        /// <summary>
        /// There is a card in the reader.
        /// </summary>
        SCARD_STATE_PRESENT = 0x00000020,

        /// <summary>
        /// The actual state of this reader is not available.
        /// </summary>
        SCARD_STATE_UNAVAILABLE = 0x00000008,

        /// <summary>
        /// The application requires the current state but does not know it.
        /// </summary>
        SCARD_STATE_UNAWARE = 0x00000000,

        /// <summary>
        /// The reader name is not recognized by Smart Cards for Windows.
        /// </summary>
        SCARD_STATE_UNKNOWN = 0x00000004,

        /// <summary>
        /// This implies that the card in the reader has not been turned on.
        /// </summary>
        SCARD_STATE_UNPOWERED = 0x00000400
    }

    /// <summary>
    /// An enumeration that defines a reader group for winscard operation.
    /// </summary>
    internal enum SCardReaderGroup : int
    {
        /// <summary>
        /// The group includes all readers.
        /// </summary>
        AllReaders = 0,

        /// <summary>
        /// The group includes default readers.
        /// </summary>
        DefaultReaders,

        /// <summary>
        /// The group includes local readers.
        /// </summary>
        LocalReaders,

        /// <summary>
        /// The group includes system readers.
        /// </summary>
        SystemReaders
    }

    /// <summary>
    /// An enumeration that defines a response to an winscard request.
    /// </summary>
    public enum SCardResponse : uint
    {
        /// <summary>
        /// The client attempted a smart card operation in a remote session, such as a client session running on a terminal server, and the operating system in use does not support smart card redirection.
        /// </summary>
        ERROR_BROKEN_PIPE = 0x00000109,

        /// <summary>
        /// An error occurred in setting the smart card file object pointer.
        /// </summary>
        SCARD_E_BAD_SEEK = 0x80100029,

        /// <summary>
        /// The action was canceled by an SCardCancel request.
        /// </summary>
        SCARD_E_CANCELLED = 0x80100002,

        /// <summary>
        /// The system could not dispose of the media in the requested manner.
        /// </summary>
        SCARD_E_CANT_DISPOSE = 0x8010000E,

        /// <summary>
        /// The smart card does not meet minimal requirements for support.
        /// </summary>
        SCARD_E_CARD_UNSUPPORTED = 0x8010001C,

        /// <summary>
        /// The requested certificate could not be obtained.
        /// </summary>
        SCARD_E_CERTIFICATE_UNAVAILABLE = 0x8010002D,

        /// <summary>
        /// A communications error with the smart card has been detected.
        /// </summary>
        SCARD_E_COMM_DATA_LOST = 0x8010002F,

        /// <summary>
        /// The specified directory does not exist in the smart card.
        /// </summary>
        SCARD_E_DIR_NOT_FOUND = 0x80100023,

        /// <summary>
        /// The reader driver did not produce a unique reader name.
        /// </summary>
        SCARD_E_DUPLICATE_READER = 0x8010001B,

        /// <summary>
        /// The specified file does not exist in the smart card.
        /// </summary>
        SCARD_E_FILE_NOT_FOUND = 0x80100024,

        /// <summary>
        /// The requested order of object creation is not supported.
        /// </summary>
        SCARD_E_ICC_CREATEORDER = 0x80100021,

        /// <summary>
        /// No primary provider can be found for the smart card.
        /// </summary>
        SCARD_E_ICC_INSTALLATION = 0x80100020,

        /// <summary>
        /// The data buffer for returned data is too small for the returned data.
        /// </summary>
        SCARD_E_INSUFFICIENT_BUFFER = 0x80100008,

        /// <summary>
        /// An ATR string obtained from the registry is not a valid ATR string.
        /// </summary>
        SCARD_E_INVALID_ATR = 0x80100015,

        /// <summary>
        /// The supplied PIN is incorrect.
        /// </summary>
        SCARD_E_INVALID_CHV = 0x8010002A,

        /// <summary>
        /// The supplied handle was not valid.
        /// </summary>
        SCARD_E_INVALID_HANDLE = 0x80100003,

        /// <summary>
        /// One or more of the supplied parameters could not be properly interpreted.
        /// </summary>
        SCARD_E_INVALID_PARAMETER = 0x80100004,

        /// <summary>
        /// Registry startup information is missing or not valid.
        /// </summary>
        SCARD_E_INVALID_TARGET = 0x80100005,

        /// <summary>
        /// One or more of the supplied parameter values could not be properly interpreted.
        /// </summary>
        SCARD_E_INVALID_VALUE = 0x80100011,

        /// <summary>
        /// Access is denied to the file.
        /// </summary>
        SCARD_E_NO_ACCESS = 0x80100027,

        /// <summary>
        /// The supplied path does not represent a smart card directory.
        /// </summary>
        SCARD_E_NO_DIR = 0x80100025,

        /// <summary>
        /// The supplied path does not represent a smart card file.
        /// </summary>
        SCARD_E_NO_FILE = 0x80100026,

        /// <summary>
        /// The requested key container does not exist on the smart card.
        /// </summary>
        SCARD_E_NO_KEY_CONTAINER = 0x80100030,

        /// <summary>
        /// Not enough memory available to complete this command.
        /// </summary>
        SCARD_E_NO_MEMORY = 0x80100006,

        /// <summary>
        /// The smart card PIN cannot be cached.
        /// </summary>
        SCARD_E_NO_PIN_CACHE = 0x80100033,

        /// <summary>
        /// No smart card reader is available.
        /// </summary>
        SCARD_E_NO_READERS_AVAILABLE = 0x8010002E,

        /// <summary>
        /// The smart card resource manager is not running.
        /// </summary>
        SCARD_E_NO_SERVICE = 0x8010001D,

        /// <summary>
        /// The operation requires a smart card, but no smart card is currently in the device.
        /// </summary>
        SCARD_E_NO_SMARTCARD = 0x8010000C,

        /// <summary>
        /// The requested certificate does not exist.
        /// </summary>
        SCARD_E_NO_SUCH_CERTIFICATE = 0x8010002C,

        /// <summary>
        /// The reader or card is not ready to accept commands.
        /// </summary>
        SCARD_E_NOT_READY = 0x80100010,

        /// <summary>
        /// An attempt was made to end a nonexistent transaction.
        /// </summary>
        SCARD_E_NOT_TRANSACTED = 0x80100016,

        /// <summary>
        /// The PCI receive buffer was too small.
        /// </summary>
        SCARD_E_PCI_TOO_SMALL = 0x80100019,

        /// <summary>
        /// The smart card PIN cache has expired.
        /// </summary>
        SCARD_E_PIN_CACHE_EXPIRED = 0x80100032,

        /// <summary>
        /// The requested protocols are incompatible with the protocol currently in use with the card.
        /// </summary>
        SCARD_E_PROTO_MISMATCH = 0x8010000F,

        /// <summary>
        /// The smart card is read-only and cannot be written to.
        /// </summary>
        SCARD_E_READ_ONLY_CARD = 0x80100034,

        /// <summary>
        /// The specified reader is not currently available for use.
        /// </summary>
        SCARD_E_READER_UNAVAILABLE = 0x80100017,

        /// <summary>
        /// The reader driver does not meet minimal requirements for support.
        /// </summary>
        SCARD_E_READER_UNSUPPORTED = 0x8010001A,

        /// <summary>
        /// The smart card resource manager is too busy to complete this operation.
        /// </summary>
        SCARD_E_SERVER_TOO_BUSY = 0x80100031,

        /// <summary>
        /// The smart card resource manager has shut down.
        /// </summary>
        SCARD_E_SERVICE_STOPPED = 0x8010001E,

        /// <summary>
        /// The smart card cannot be accessed because of other outstanding connections.
        /// </summary>
        SCARD_E_SHARING_VIOLATION = 0x8010000B,

        /// <summary>
        /// The action was canceled by the system, presumably to log off or shut down.
        /// </summary>
        SCARD_E_SYSTEM_CANCELLED = 0x80100012,

        /// <summary>
        /// The user-specified time-out value has expired.
        /// </summary>
        SCARD_E_TIMEOUT = 0x8010000A,

        /// <summary>
        /// An unexpected card error has occurred.
        /// </summary>
        SCARD_E_UNEXPECTED = 0x8010001F,

        /// <summary>
        /// The specified smart card name is not recognized.
        /// </summary>
        SCARD_E_UNKNOWN_CARD = 0x8010000D,

        /// <summary>
        /// The specified reader name is not recognized.
        /// </summary>
        SCARD_E_UNKNOWN_READER = 0x80100009,

        /// <summary>
        /// An unrecognized error code was returned.
        /// </summary>
        SCARD_E_UNKNOWN_RES_MNG = 0x8010002B,

        /// <summary>
        /// This smart card does not support the requested feature.
        /// </summary>
        SCARD_E_UNSUPPORTED_FEATURE = 0x80100022,

        /// <summary>
        /// An attempt was made to write more data than would fit in the target object.
        /// </summary>
        SCARD_E_WRITE_TOO_MANY = 0x80100028,

        /// <summary>
        /// An internal communications error has been detected.
        /// </summary>
        SCARD_F_COMM_ERROR = 0x80100013,

        /// <summary>
        /// An internal consistency check failed.
        /// </summary>
        SCARD_F_INTERNAL_ERROR = 0x80100001,

        /// <summary>
        /// An internal error has been detected, but the source is unknown.
        /// </summary>
        SCARD_F_UNKNOWN_ERROR = 0x80100014,

        /// <summary>
        /// An internal consistency timer has expired.
        /// </summary>
        SCARD_F_WAITED_TOO_LONG = 0x80100007,

        /// <summary>
        /// The operation has been aborted to allow the server application to exit.
        /// </summary>
        SCARD_P_SHUTDOWN = 0x80100018,

        /// <summary>
        /// No error was encountered.
        /// </summary>
        SCARD_S_SUCCESS = 0,

        /// <summary>
        /// The requested item could not be found in the cache.
        /// </summary>
        SCARD_W_CACHE_ITEM_NOT_FOUND = 0x80100070,

        /// <summary>
        /// The requested cache item is too old and was deleted from the cache.
        /// </summary>
        SCARD_W_CACHE_ITEM_STALE = 0x80100071,

        /// <summary>
        /// The new cache item exceeds the maximum per-item size defined for the cache.
        /// </summary>
        SCARD_W_CACHE_ITEM_TOO_BIG = 0x80100072,

        /// <summary>
        /// The action was canceled by the user.
        /// </summary>
        SCARD_W_CANCELLED_BY_USER = 0x8010006E,

        /// <summary>
        /// No PIN was presented to the smart card.
        /// </summary>
        SCARD_W_CARD_NOT_AUTHENTICATED = 0x8010006F,

        /// <summary>
        /// The card cannot be accessed because the maximum number of PIN entry attempts has been reached.
        /// </summary>
        SCARD_W_CHV_BLOCKED = 0x8010006C,

        /// <summary>
        /// The end of the smart card file has been reached.
        /// </summary>
        SCARD_W_EOF = 0x8010006D,

        /// <summary>
        /// The smart card has been removed, so further communication is not possible.
        /// </summary>
        SCARD_W_REMOVED_CARD = 0x80100069,

        /// <summary>
        /// The smart card was reset.
        /// </summary>
        SCARD_W_RESET_CARD = 0x80100068,

        /// <summary>
        /// Access was denied because of a security violation.
        /// </summary>
        SCARD_W_SECURITY_VIOLATION = 0x8010006A,

        /// <summary>
        /// Power has been removed from the smart card, so that further communication is not possible.
        /// </summary>
        SCARD_W_UNPOWERED_CARD = 0x80100067,

        /// <summary>
        /// The smart card is not responding to a reset.
        /// </summary>
        SCARD_W_UNRESPONSIVE_CARD = 0x80100066,

        /// <summary>
        /// The reader cannot communicate with the card, due to ATR string configuration conflicts.
        /// </summary>
        SCARD_W_UNSUPPORTED_CARD = 0x80100065,

        /// <summary>
        /// 
        /// </summary>
        SCARD_W_WRONG_CHV = 0x8010006B
    }

    /// <summary>
    /// An enumeration that defines the share mode of a winscard reader.
    /// </summary>
    public enum SCardShareMode : uint
    {
        /// <summary>
        /// The share mode is undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The application is allocating the reader for private use and may not be shared.
        /// </summary>
        Direct = 3,

        /// <summary>
        /// The application is not willing to share the card with other applications.
        /// </summary>
        Exclusive = 1,

        /// <summary>
        /// The application is willing to share the card with other applications.
        /// </summary>
        Shared = 2
    }

    #endregion
    #endregion
}