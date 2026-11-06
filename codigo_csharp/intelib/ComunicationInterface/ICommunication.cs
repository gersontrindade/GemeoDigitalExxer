using System.Collections.Generic;

namespace ComunicationInterface;

public interface ICommunication
{
	bool Conectado { get; }

	bool Heartbeat { get; }

	ushort TamanhoBuffer { get; set; }

	List<Tag> TagsLidas { get; set; }

	List<Tag> TagsEscrever { get; set; }

	void Conectar();

	void AtualizarDados(bool offset, int offsetSaidas, int offsetEntradas);

	void Desconectar();
}
