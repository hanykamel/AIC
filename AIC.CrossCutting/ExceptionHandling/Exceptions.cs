using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.ExceptionHandling
{
    public abstract class Exceptions : Exception
    {
        protected Exceptions() { }

        protected Exceptions(string message) : base(message) { }
    }
    public class UserForbiddenException : Exceptions
    {
        public UserForbiddenException() { }

        public UserForbiddenException(string message) : base(message) { }
    }
    public class UserRejectedException : Exceptions
    {
        public UserRejectedException() { }

        public UserRejectedException(string message) : base(message) { }
    }
    public class UserAlreadyRegisteredException : Exceptions
    {
        public UserAlreadyRegisteredException() { }

        public UserAlreadyRegisteredException(string message) : base(message) { }
    }
    public class UserAlreadyUnsubscribedException : Exceptions
    {
        public UserAlreadyUnsubscribedException() { }

        public UserAlreadyUnsubscribedException(string message) : base(message) { }
    }
    public class DuplicatedItemException : Exceptions
    {
        public DuplicatedItemException() { }

        public DuplicatedItemException(string message) : base(message) { }
    }
    public class DuplicatePasswordException : Exceptions
    {
        public DuplicatePasswordException() { }

        public DuplicatePasswordException(string message) : base(message) { }
    }
    public class InternalServerErrorException : Exceptions
    {
        public InternalServerErrorException() : base() { }
        public InternalServerErrorException(string message) : base(message) { }
    }

    public class ModelStateException : Exceptions
    {
        public ModelStateException() : base() { }
        public ModelStateException(string message) : base(message) { }
    }

    public class UserNotFoundException : Exceptions
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(string message) : base(message) { }
    }

    public class TokenExpiredException : Exceptions
    {
        public TokenExpiredException() : base() { }
        public TokenExpiredException(string message) : base(message) { }
    }
    public class InvalidTokenException : Exceptions
    {
        public InvalidTokenException() : base() { }
        public InvalidTokenException(string message) : base(message) { }
    }
    public class RoleNotFoundException : Exceptions
    {
        public RoleNotFoundException() : base() { }
        public RoleNotFoundException(string message) : base(message) { }
    }

    public class NotSystemUserException : Exceptions
    {
        public NotSystemUserException() : base() { }
        public NotSystemUserException(string message) : base(message) { }
    }

    public class UserLockedOutException : Exceptions
    {
        public UserLockedOutException() : base() { }
        public UserLockedOutException(string message) : base(message) { }
    }

    public class UnAuthorizedException : Exceptions
    {
        public UnAuthorizedException() : base() { }
        public UnAuthorizedException(string message) : base(message) { }
    }
    public class FirstLoginException : Exceptions
    {
        public FirstLoginException() : base() { }
        public FirstLoginException(string message) : base(message) { }
    }

    public class UserNotActivatedException : Exceptions
    {
        public UserNotActivatedException() : base() { }
        public UserNotActivatedException(string message) : base(message) { }
    }

    public class UserCredintialException : Exceptions
    {
        public UserCredintialException() : base() { }
        public UserCredintialException(string message) : base(message) { }
    }
    public class SPException : Exceptions
    {
        public SPException() : base() { }
        public SPException(string message) : base(message) { }
    }
    public class NotFoundException : Exceptions
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
    public class NothingChangedException : Exceptions
    {
        public NothingChangedException() : base() { }
        public NothingChangedException(string message) : base(message) { }
    }
    public class PreConditionFailedException : Exceptions
    {
        public PreConditionFailedException() : base() { }
        public PreConditionFailedException(string message) : base(message) { }
    }
    public class NotSavedException : Exceptions
    {
        public NotSavedException() : base() { }
        public NotSavedException(string message) : base(message) { }
    } 
    public class CantBeDeletedException : Exceptions
    {
        public CantBeDeletedException() : base() { }
        public CantBeDeletedException(string message) : base(message) { }
    } 
    public class EmailNotSentException : Exceptions
    {
        public EmailNotSentException() : base() { }
        public EmailNotSentException(string message) : base(message) { }
    }
    public class FormURLNotValidException : Exceptions
    {
        public FormURLNotValidException() : base() { }
        public FormURLNotValidException(string message) : base(message) { }
    }
    public class ModelExpiredException : Exceptions
    {
        public ModelExpiredException() : base() { }
        public ModelExpiredException(string message) : base(message) { }
    }
    public class AppliedBeforeException : Exceptions
    {
        public AppliedBeforeException() : base() { }
        public AppliedBeforeException(string message) : base(message) { }
    }

}
