using System;

namespace TB.Kutuphane.Common.Enum
{
    [Serializable]
    public enum ResultType : short
    {
        Unspecified = 0,
        Success = 1,
        UnSuccess = 2,
        LoginSuccess = 3,
        LoginUnSuccess = 4,
        LoginClientIpBlocked = 5,
        LoginClientIpLimitExceeded = 6,
        LoginAccountExpired = 7,
        LoginAccountSuspended = 8,
        CariNotExits = 9,
        PasswordMatchError = 10,
        OldPasswordError = 11,
        PasswordSuccess = 12,
        LicenceExpired = 13,
        LicenceError = 14,
        BankError = 15,
    }
}
