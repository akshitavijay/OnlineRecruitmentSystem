﻿//=============================================
//  Developer:	<Subin Sunu Jacob>
//  Create date: <9th Oct,2020>
//  Related To : To recieve all application related exceptions
//=============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRecruitmentSystem.ExceptionLayer
{

    /// <summary>
    /// Custom Exception class for recieving all exceptions related to user model
    /// </summary>
    public class UserException : ApplicationException
    {
        /// <summary>
        /// recieves the exception generated by the application in user model
        /// </summary>
        public UserException()
        {
        }

        /// <summary>
        /// Recieves the exception generated along with the message the exception carried
        /// </summary>
        /// <param name="message">exception message that was generated and then passed along</param>
        public UserException(string message) : base(message)
        {

        }
    }
}