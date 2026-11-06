using System;

namespace Modbus;

public sealed class Datastore
{
	private const int MAX_ELEMENTS = 65536;

	private bool[] discrete_inputs;

	private bool[] coils;

	private ushort[] input_registers;

	private ushort[] holding_registers;

	private byte unit_id;

	public bool[] DiscreteInputs
	{
		get
		{
			return discrete_inputs;
		}
		set
		{
			discrete_inputs = value;
		}
	}

	public bool[] Coils
	{
		get
		{
			return coils;
		}
		set
		{
			coils = value;
		}
	}

	public ushort[] InputRegisters
	{
		get
		{
			return input_registers;
		}
		set
		{
			input_registers = value;
		}
	}

	public ushort[] HoldingRegisters
	{
		get
		{
			return holding_registers;
		}
		set
		{
			holding_registers = value;
		}
	}

	public byte UnitID
	{
		get
		{
			return unit_id;
		}
		set
		{
			unit_id = value;
		}
	}

	public Datastore(byte uid, int n_discrete_inputs, int n_coils, int n_input_registers, int n_holding_registers)
	{
		unit_id = uid;
		if (n_discrete_inputs >= 0 && n_discrete_inputs <= 65536 && n_coils >= 0 && n_coils <= 65536 && n_input_registers >= 0 && n_input_registers <= 65536 && n_holding_registers >= 0 && n_holding_registers <= 65536)
		{
			discrete_inputs = new bool[n_discrete_inputs];
			coils = new bool[n_coils];
			input_registers = new ushort[n_input_registers];
			holding_registers = new ushort[n_holding_registers];
			return;
		}
		throw new Exception("Definizione del database errata, ogni set di registri deve essere compreso fra 0 e " + 65536 + "!");
	}

	public Datastore(byte uid)
	{
		unit_id = uid;
		discrete_inputs = new bool[65536];
		coils = new bool[65536];
		input_registers = new ushort[65536];
		holding_registers = new ushort[65536];
	}
}
