using System;

internal class Validation
{
	private string Password = "5d410190959d2289ab3688ea0dc4d9e2";

	public Validation(string password)
	{
		if (password != Password)
		{
			throw new Exception("Senha fornecida para a biblioteca está incorreta");
		}
	}
}
