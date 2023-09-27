using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WeatherSampleOne.Domain.Network
{
	public class ApiError
	{
        public ApiError() { }
        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(int responseCode, string message)
        {
            ResponseCode = responseCode;
            ResponseMessage = message;
        }
        public string Message
        {
            get;
            set;
        }

        public HttpStatusCode? StatusCode { get; set; }

        public bool IsResponseAmbiguous => StatusCode == HttpStatusCode.RequestTimeout || StatusCode == HttpStatusCode.Conflict;

        public string ConcatenatedErrors
        {
            get
            {
                if (Errors != null)
                {
                    return string.Join("\n", Errors);
                }
                else
                {
                    return ResponseMessage;
                }

            }
        }
        public IDictionary<string, List<string>> ModelState
        {
            get;
            set;
        }
        private List<string> errors = new List<string>();
        public List<string> Errors
        {
            get
            {
                if (!errors.Any())
                {

                    if (ModelState != null)
                    {
                        errors.AddRange(from model in ModelState
                                        from error in model.Value
                                        select error.Trim());
                    }
                    else if (!string.IsNullOrWhiteSpace(Message))
                        errors.Add(Message);
                }
                return errors;
            }
        }

        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }

        private string error { get; set; }
        public string Error
        {
            get
            {
                Message = error;
                return error;
            }
            set
            {
                Message = value;
                error = value;
            }
        }
    }
}

