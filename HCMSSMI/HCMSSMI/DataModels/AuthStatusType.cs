namespace HCMSSMI.DataModels
{
    public enum AuthStatusType
    {
        None = 0,
        Active = 101,
        NotActive = 102,
        Registered = 201,
        Unregistered = 202,
        LoginSuccess = 203,
        LoginFailed = 204,
    }
}