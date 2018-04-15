using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Classes
{
    public class LoginResult
    {
        int result;

        public LoginResult(int result)
        {
            this.Result = result;
        }

        public int Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }
    }
}