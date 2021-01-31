namespace HCMSSMI.DataModels
{
    public static class AuthStatus
    {
        public static int Status(AuthStatusType status)
        {
            switch (status)
            {
                case AuthStatusType.None:
                    return 0;
                case AuthStatusType.Registered:
                    return 201;
                case AuthStatusType.Unregistered:
                    return 202;
                case AuthStatusType.LoginSuccess:
                    return 203;
                case AuthStatusType.LoginFailed:
                    return 204;
                case AuthStatusType.Active:
                    return 101;
                case AuthStatusType.NotActive:
                    return 102;

                default:
                    return 0;
            }
        }
    }
}