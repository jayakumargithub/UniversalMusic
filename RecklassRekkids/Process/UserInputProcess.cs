using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.Process
{
    public class UserInputProcess
    {
        private readonly string _userInput;
        public UserSerachCriteria UserSerachCriteria { get; set; }
        public List<string> Errors { get; set; }
        public bool IsUserInputValid { get; set; }

        public UserInputProcess(string userInput)
        {
            UserSerachCriteria = new UserSerachCriteria();
            Errors = new List<string>();
            _userInput = userInput;
            Process();
        }

        public void Process()
        {
           
            var userInput = _userInput.Split(new[] { ' ' });
            if (userInput.Length == 1)
            {
                Errors.Add("Input is not valid.");
                IsUserInputValid = false;
            }
            if (IsUserInputValid)
            {
                UserSerachCriteria.Partner = userInput[0];
                var searchDatestring =
                    CommonUtility.RemoveDaySuffix(userInput[1] + " " + userInput[2].ToLower() + " " +
                                                  userInput[3]);
                DateTime userDate;
                var isValid = DateTime.TryParse(searchDatestring, out userDate);
                if (!isValid)
                {
                    Errors.Add("Date is not valid. Please correct and try agian.");
                    IsUserInputValid = false;
                }
            }

        }
    }
}
