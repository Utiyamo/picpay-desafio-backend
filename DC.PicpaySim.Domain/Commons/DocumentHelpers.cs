using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Commons
{
    public static class DocumentHelpers
    {
        public static bool IsValidCPF(string cpf)
        {
            // Remove non-numeric characters
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // Check if all digits are the same
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Validate first check digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (10 - i) * (cpf[i] - '0');

            int firstCheckDigit = sum % 11;
            firstCheckDigit = firstCheckDigit < 2 ? 0 : 11 - firstCheckDigit;

            if (cpf[9] - '0' != firstCheckDigit)
                return false;

            // Validate second check digit
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (11 - i) * (cpf[i] - '0');

            int secondCheckDigit = sum % 11;
            secondCheckDigit = secondCheckDigit < 2 ? 0 : 11 - secondCheckDigit;

            return cpf[10] - '0' == secondCheckDigit;
        }

        public static bool IsValidCNPJ(string cnpj)
        {
            // Remove non-numeric characters
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            // Check if all digits are the same
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // Validate first check digit
            int[] multipliers1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (cnpj[i] - '0') * multipliers1[i];

            int firstCheckDigit = sum % 11;
            firstCheckDigit = firstCheckDigit < 2 ? 0 : 11 - firstCheckDigit;

            if (cnpj[12] - '0' != firstCheckDigit)
                return false;

            // Validate second check digit
            int[] multipliers2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += (cnpj[i] - '0') * multipliers2[i];

            int secondCheckDigit = sum % 11;
            secondCheckDigit = secondCheckDigit < 2 ? 0 : 11 - secondCheckDigit;

            return cnpj[13] - '0' == secondCheckDigit;
        }
    }
}
