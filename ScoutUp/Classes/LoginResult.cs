using System.Collections.Generic;

namespace ScoutUp.Classes
{
    public class LoginResult
    {
        int result;
        private string error;
        private IEnumerable<string> errors;
        private List<string> modelErrors;
        public LoginResult(int result,string error ="",IEnumerable<string> errors=null, List<string> modelErrors =null)
        {
            this.Result = result;
            this.Error = error;
            this.Errors = errors;
            this.ModelErrors = modelErrors;
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
        public string Error
        {
            get
            {
                return error;
            }

            set
            {
                error = value;
            }
        }
        public IEnumerable<string> Errors
        {
            get
            {
                return errors;
            }

            set
            {
                errors = value;
            }
        }
        public List<string> ModelErrors
        {
            get
            {
                return modelErrors;
            }

            set
            {
                modelErrors = value;
            }
        }

    }
}