namespace  cyberforgepc.Helpers.Exceptions
{
    using System;

    public abstract class CustomException : Exception
    {
        public int ExceptionCode { get; set; }

        public CustomException(string message) : base(message) { }
    }

    #region "Administrative Exceptions"

    public class UserAdminAlreadyExistException : CustomException
    {
        public UserAdminAlreadyExistException(string message) : base(message) => this.ExceptionCode = 4091;
    }

    public class UserAdminNotExistsException : CustomException
    {
        public UserAdminNotExistsException(string message) : base(message) => this.ExceptionCode = 4041;
    }

    #endregion

    #region "City Exceptions"

    public class CityNotFoundException : CustomException
    {
        public CityNotFoundException(string message) : base(message) => this.ExceptionCode = 4042;
    }

    #endregion

    #region "Country Exceptions"

    public class CountryNotFoundException : CustomException
    {
        public CountryNotFoundException(string message) : base(message) => this.ExceptionCode = 4043;
    }

    #endregion

    #region "Department Exceptions"

    public class DepartmentNotFoundException : CustomException
    {
        public DepartmentNotFoundException(string message) : base(message) => this.ExceptionCode = 4044;
    }

    #endregion

    #region "Guide Exceptions"

    public class GuideNotFoundException : CustomException
    {
        public GuideNotFoundException(string message) : base(message) => this.ExceptionCode = 40415;
    }

    #endregion

    #region "Locker Exceptions"

    public class LockerNotFoundException : CustomException
    {
        public LockerNotFoundException(string message) : base(message) => this.ExceptionCode = 4045;
    }

    #endregion

    #region "Order Exceptions"

    public class OrderNotFoundException : CustomException
    {
        public OrderNotFoundException(string message) : base(message) => this.ExceptionCode = 4046;
    }

    #endregion

    #region "Product Exceptions"

    public class ProductNotFoundException : CustomException
    {
        public ProductNotFoundException(string message) : base(message) => this.ExceptionCode = 4047;
    }

    #endregion

    #region "Quote Exceptions"

    public class QuoteNotFoundException : CustomException
    {
        public QuoteNotFoundException(string message) : base(message) => this.ExceptionCode = 4048;
    }

    #endregion

    #region "Quote Request Exceptions"

    public class QuoteRequestNotFoundException : CustomException
    {
        public QuoteRequestNotFoundException(string message) : base(message) => this.ExceptionCode = 4049;
    }

    #endregion

    #region "User Exceptions"

    public class UserAlreadyExistsException : CustomException
    {
        public UserAlreadyExistsException(string message) : base(message) => this.ExceptionCode = 4092;
    }

    public class UserNotExistsException : CustomException
    {
        public UserNotExistsException(string message) : base(message) => this.ExceptionCode = 40411;
    }

    public class UserInactiveException : CustomException
    {
        public UserInactiveException(string message) : base(message) => this.ExceptionCode = 4031;
    }

    public class UserNotPasswordException : CustomException
    {
        public UserNotPasswordException(string message) : base(message) => this.ExceptionCode = 4032;
    }

    public class UserPasswordIncorrectException : CustomException
    {
        public UserPasswordIncorrectException(string message) : base(message) => this.ExceptionCode = 4033;
    }

    public class UserFacebookNotExistsException : CustomException
    {
        public UserFacebookNotExistsException(string message) : base(message) => this.ExceptionCode = 40414;
    }

    public class UserIsActiveException : CustomException
    {
        public UserIsActiveException(string message) : base(message) => this.ExceptionCode = 4093;
    }

    public class UserTokenExpiredException : CustomException
    {
        public UserTokenExpiredException(string message) : base(message) => this.ExceptionCode = 4034;
    }

    public class UserSecurityTokenException : CustomException
    {
        public UserSecurityTokenException(string message) : base(message) => this.ExceptionCode = 4035;
    }

    #endregion

    #region "Voucher Exceptions"

    public class VoucherNotFoundException : CustomException
    {
        public VoucherNotFoundException(string message) : base(message) => this.ExceptionCode = 40412;
    }

    #endregion

    #region "Warehouse Exceptions"

    public class WarehouseNotFoundException : CustomException
    {
        public WarehouseNotFoundException(string message) : base(message) => this.ExceptionCode = 40413;
    }

    #endregion

    #region "WebRequest Exceptions"

    public class WebRequestNotFoundException : CustomException
    {
        public WebRequestNotFoundException(string message) : base(message) => this.ExceptionCode = 40414;
    }

    #endregion

    #region "Status Exceptions"

    public class StatusNotFoundException : CustomException
    {
        public StatusNotFoundException(string message) : base(message) => this.ExceptionCode = 40417;
    }

    public class InvalidStatusGuideException : CustomException
    {
        public InvalidStatusGuideException(string message) : base(message) => this.ExceptionCode = 42201;
    }

    public class InvalidStatusOrderException : CustomException
    {
        public InvalidStatusOrderException(string message) : base(message) => this.ExceptionCode = 42202;
    }

    public class InvalidStatusQuoteException : CustomException
    {
        public InvalidStatusQuoteException(string message) : base(message) => this.ExceptionCode = 42203;
    }

    public class InvalidStatusQuoteRequestException : CustomException
    {
        public InvalidStatusQuoteRequestException(string message) : base(message) => this.ExceptionCode = 42204;
    }

    public class InvalidStatusWebRequestException : CustomException
    {
        public InvalidStatusWebRequestException(string message) : base(message) => this.ExceptionCode = 42205;
    }

    #endregion
}
