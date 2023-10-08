using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEExecutionResult
    {
        public readonly bool IsSuccess;
        public readonly string ErrorMessage;
        public readonly string ErrorCode;
        public EXECommand OwningCommand { get; set; }

        private EXEExecutionResult(bool success, EXECommand owningCommand) : this(success, string.Empty, string.Empty, owningCommand) { }
        private EXEExecutionResult(bool success, string errorMessage, string errorCode, EXECommand owningCommand)
        {
            this.IsSuccess = success;
            this.ErrorMessage = errorMessage;
            this.ErrorCode = errorCode;
            this.OwningCommand = owningCommand;
        }

        public static EXEExecutionResult Success()
        {
            return Success(null);
        }
        public static EXEExecutionResult Success(EXECommand owningCommand)
        {
            return new EXEExecutionResult(true, owningCommand);
        }

        public static EXEExecutionResult Error(string errorMessage, string errorCode)
        {
            return Error(errorMessage, errorCode, null);
        }
        public static EXEExecutionResult Error(string errorMessage, string errorCode, EXECommand owningCommand)
        {
            return new EXEExecutionResult(false, errorMessage, errorCode, owningCommand);
        }

        public override string ToString()
        {
            return string
                    .Format
                    (
                        "{{\n\tsuccess: '{0}',\n\tcommand: '{1}',\n\tcommandType: '{2}',\n\terrorMessage: '{3}',\n\terrorCode: '{4}'\n}}",
                        this.IsSuccess,
                        OwningCommand == null ? string.Empty : this.OwningCommand.ToCode(),
                        OwningCommand == null ? string.Empty : this.OwningCommand.GetType().Name,
                        ErrorMessage ?? string.Empty,
                        ErrorCode ?? string.Empty
                    );
        }
    }
}
