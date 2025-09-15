namespace MotorcycleRentals.src.Application.Validation
{
    public static class DocumentValidator
    {
        public static bool IsValidCnpj(string cnpj)
        {
            // Implemente validação de dígito verificador conforme Receita Federal
            // ... (código padrão de validação)
            return true; // ou false
        }

        public static bool IsValidCnh(string cnh)
        {
            // Implemente validação de CNH conforme regras do DETRAN
            // ... (código padrão de validação)
            return true; // ou false
        }
    }
}
