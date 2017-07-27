namespace AutoResponse.Client
{
    using System.Collections.Generic;

    internal class ErrorRegistrationEqualityComparer : IEqualityComparer<ErrorRegistration>
    {
        public bool Equals(ErrorRegistration x, ErrorRegistration y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            if (!x.StatusCode.Equals(y.StatusCode))
            {
                return false;
            }

            if (x.ErrorCode == null && y.ErrorCode == null)
            {
                return true;
            }

            if (x.ErrorCode == null || y.ErrorCode == null)
            {
                return false;
            }

            return x.ErrorCode.Equals(y.ErrorCode);
        }

        public int GetHashCode(ErrorRegistration errorRegistration)
        {
            if (errorRegistration == null)
            {
                return 0;
            }

            unchecked
            {
                var hash = 17;

                hash = (hash * 23) + errorRegistration.StatusCode.GetHashCode();

                if (errorRegistration.ErrorCode != null)
                {
                    hash = (hash * 23) + errorRegistration.ErrorCode.GetHashCode();
                }

                return hash;
            }
        }
    }
}