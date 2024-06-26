﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Exam.Shared.Attributes
{
    public class PersonalIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var personalCode = value as string;

            if (string.IsNullOrEmpty(personalCode))
            {
                return new ValidationResult("Personal code is required.");
            }

            if (!Regex.IsMatch(personalCode, @"^\d{11}$"))
            {
                return new ValidationResult("Personal code must be 11 digits.");
            }
            if (IsException(personalCode))
            {
                return ValidationResult.Success;
            }

            if (!ValidateDatePattern(personalCode))
            {
                return new ValidationResult("Personal code does not match the required pattern for date");
            }

            if (!ValidateChecksum(personalCode))
            {
                return new ValidationResult("Personal code checksum is invalid.");
            }

            return ValidationResult.Success;
        }

        private bool IsException(string personalCode)
        {
            int firstDigit = int.Parse(personalCode[0].ToString());
            if (firstDigit == 9)
            {
                return true;
            }

            return false;
        }
        private bool ValidateDatePattern(string personalCode)
        {
            int firstDigit = int.Parse(personalCode[0].ToString());
            if (firstDigit < 1 || firstDigit > 6)
            {
                return false;
            }

            int fourthDigit = int.Parse(personalCode[3].ToString());
            if (fourthDigit < 0 || fourthDigit > 1)
            {
                return false;
            }
            int fifthhDigit = int.Parse(personalCode[5].ToString());
            if (fifthhDigit < 0 || fifthhDigit > 3)
            {
                return false;
            }

            return true;
        }

        private bool ValidateChecksum(string personalCode)
        {
            int[] multipliers1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            int[] multipliers2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 };

            int sum1 = 0, sum2 = 0;

            for (int i = 0; i < 10; i++)
            {
                sum1 += (personalCode[i] - '0') * multipliers1[i];
                sum2 += (personalCode[i] - '0') * multipliers2[i];
            }

            int checksum = sum1 % 11;
            if (checksum == 10)
            {
                checksum = sum2 % 11;
                if (checksum == 10)
                {
                    checksum = 0;
                }
            }

            return checksum == int.Parse(personalCode[10].ToString());
        }
    }



}
