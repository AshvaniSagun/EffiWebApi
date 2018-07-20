using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public static class Error
    {
        public static List<ErrorMessage> ParameterEmpty(string innerMessage = "", string message = "")
        {
            message = (message == string.Empty || message == "") ? Resource.GetResxValueByName("ApplicationError") : message;
            List<ErrorMessage> errorlst = new List<ErrorMessage>();
            List<ModelState> err = InnerError(innerMessage);
            errorlst.Add(new ErrorMessage() { Message = message, ModelState = err });
            return errorlst;
        }
        public static List<ErrorMessage> ModelValidation(ModelState model)
        {
            List<ErrorMessage> errorlst = new List<ErrorMessage>();
            return errorlst;
        }
        private static List<ModelState> InnerError(string message)
        {
            List<ModelState> err = new List<ModelState>() { new ModelState() { Message = message } };
            
           
            return err;
        }
    }

    public class ErrorMessage
    {
        
        public string Message { get; set; }
        public List<ModelState> ModelState { get; set; }
    }
    public class ModelState
    {
        public string Message { get; set; }
    }

    public static class Resource
    {
        
        public static string GetResxValueByName(string key)
        {
            ResourceManager myManager = new ResourceManager(typeof(Message));
            return myManager.GetString(key);
        }

    }
   
}