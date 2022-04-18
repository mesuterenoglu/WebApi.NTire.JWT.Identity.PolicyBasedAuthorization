

using System;

namespace Common.Token
{
    public class Token
    {
        public string AccessToken { get; set; }

        public DateTime TokenExpiration { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
