namespace Sistema;

public static class Validador {
    public static bool CpfValido(string c) {
        c = new string(c.Where(char.IsDigit).ToArray());
        // Requisito: Operadores lógicos (&&, ||, !=)
        if (c.Length != 11 || c.Distinct().Count() == 1) return false;
        
        for (int j = 0; j < 2; j++) {
            int s = 0, p = 10 + j;
            for (int i = 0; i < 9 + j; i++) s += (c[i] - '0') * (p - i);
            int d = (s * 10) % 11;
            if ((d >= 10 ? 0 : d) != (c[9 + j] - '0')) return false;
        }
        return true;
    }
}