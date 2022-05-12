using System.Collections;
using System.ComponentModel.DataAnnotations;
using helping;
using WebApplication1.models;

namespace WebApplication1.helping;

public class attributes
{
    public class BaseAttribute : ValidationAttribute
    {
        virtual public Action<object> func { get; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                func(value);
            }
            catch (Exception e)
            {
                return new ValidationResult(e.Message);
            }
            return ValidationResult.Success;
        }
    }
    public class PositiveInteger : BaseAttribute
    {
        override public Action<object> func
        {
            get => value => validation.validation.positive_integer((int?) value);
        }
    }
    public class Name: BaseAttribute
    {
        override public Action<object> func
        {
            get => value => validation.validation.name((string?) value);
        }
    }

    public class InternationalPassport : BaseAttribute
    {
        override public Action<object> func
        {
            get => value => validation.validation.regex_match((string?) value, config.international_passport);
        }
    }
    public class Password : BaseAttribute
    {
        override public Action<object> func
        {
            get => value => validation.validation.regex_match((string?)value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
    }
    public class Email : BaseAttribute
    {
        override public Action<object> func
        {
            get => value => validation.validation.regex_match((string?)value, "^[a-z0-9.]+@[a-z0-9.]+.[a-z0-9.]+");
        }
    }
    public class Vaccine: BaseAttribute
    {

        override public Action<object> func
        {
            get => value => validation.validation.in_array(((string?) value).ToLower(), config.vaccine_types);
        }
    }

    public class BirthDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var certificate = (Certificate) validationContext.ObjectInstance;
            var end = helping_func.max_or_not_null(certificate.start_date, DateTime.Today);
            try
            {
                validation.validation.date_in_range(DateTime.Parse(value.ToString()), config.birth_date, end);
            }
            catch (Exception e)
            {
                return new ValidationResult(e.Message);
            }
            return ValidationResult.Success;
        }
    }
    public class StartDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var certificate = (Certificate) validationContext.ObjectInstance;
            var start = helping_func.max_or_not_null(config.pandemic_start_date, certificate.birth_date);
            var end = helping_func.max_or_not_null(certificate.end_date, DateTime.Today);
            try
            {
                validation.validation.date_in_range(DateTime.Parse(value.ToString()), start, end);
            }
            catch (Exception e)
            {
                return new ValidationResult(e.Message);
            }
            return ValidationResult.Success;
        }
    }
    public class EndDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var certificate = (Certificate) validationContext.ObjectInstance;
             var start = helping_func.max_or_not_null(config.pandemic_start_date, certificate.start_date);
            try
            {
                validation.validation.date_in_range(DateTime.Parse(value.ToString()), start, config.certificate_end_date);
            }
            catch (Exception e)
            {
                return new ValidationResult(e.Message);
            }
            return ValidationResult.Success;
        }
    }
}