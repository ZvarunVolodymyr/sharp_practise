using CertificateClass;
using validation;
using Validators;
namespace practice;


public class VaccinationRequest: IGetSet
{
	///  That i add for custom class, because i read from txt file (author of this class read from json), and i
	/// have reflaction replacement, because reflection is very slow
	private string[] field_list; 
	private Dictionary<string, Action<object?>> setters;

	private void create_fields()
	{
		setters = new Dictionary<string, Action<object?>>()
		{
			{"id", val => this.id = Convert.ToInt32(val)},
			{"PatientName", val => this.PatientName = val.ToString()},
			{"PatientPhone", val => this.PatientPhone = val.ToString()},
			{"Vaccine", val => this.Vaccine = val.ToString()},
			{"Date", val => this.Date = DateOnly.ParseExact((string)val, "dd.MM.yyyy")},
			{"StartTime", val => this.StartTime = TimeOnly.Parse((string)val)},
			{"EndTime", val => this.EndTime = TimeOnly.Parse((string)val)}
		};
		field_list = setters.Keys.ToArray();
	}

	public string[] get_fields_list()
	{
		return this.field_list;
	}
	
	public void set_field<T>(string name, T value)
	{
		this.setters[name](value);
	}
    
	public object? get_field(string name)
	{
		return this.GetType().GetProperty(name).GetValue(this);
	}
	/// END
	
	/// That add, because author of class doesn't have reading from conslo
	private void read(string name)
	{
		this.set_field(name, Console.ReadLine());
	}
	public VaccinationRequest read_from_console()
	{
		foreach (var name in this.get_fields_list())
		{
			Console.Write($"Write {name.Replace('_', ' ')} field\n");
			validation_functions.try_until_success(this.read, name);
		}
		return this;
	}
	
	///  END
	private int _ID;
	private string _patientName;
	private string _patientPhone;
	private string _vaccine;
	private DateOnly _date;
	private TimeOnly _startTime;
	private TimeOnly _endTime;
	
	private static TimeOnly _canStart = new TimeOnly(7, 0);
	private static TimeOnly _canEnd = new TimeOnly(21, 0);
	private static string[] _vaccineChoices 
		= {"pfizer", "moderna", "astrazeneca", "coronavac"};

	public VaccinationRequest()
	{
		this.id = 1;
		this.PatientName = "John Doe";
		this.PatientPhone = "+380123456789";
		this.Vaccine = "pfizer";
		this.Date = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
		this.StartTime = VaccinationRequest._canStart;
		this.EndTime = VaccinationRequest._canEnd.AddHours(1);
		
		this.create_fields();
		return;
	}

	public VaccinationRequest(
		int ID, 
		string patientName,
		string patientPhone,
		string vaccine,
		DateOnly date,
		TimeOnly startTime,
		TimeOnly endTime
	)
	{
		this.id = ID;
		this.PatientName = patientName;
		this.PatientPhone = patientPhone;
		this.Vaccine = vaccine;
		this.Date = date;
		this.StartTime = startTime;
		this.EndTime = endTime;
		return;
	}

	public VaccinationRequest(
		string ID, 
		string patientName,
		string patientPhone,
		string vaccine,
		DateOnly date,
		TimeOnly startTime,
		TimeOnly endTime
	)
	{
		this.id = int.Parse(ID);
		this.PatientName = patientName;
		this.PatientPhone = patientPhone;
		this.Vaccine = vaccine;
		this.Date = date;
		this.StartTime = startTime;
		this.EndTime = endTime;
		return;
	}

	public int? id
	{
		get => this._ID;
		set => this._ID = IntValidators.GreaterThanZero(value.ToString())
			?? throw new ArgumentException("ID cannot be lesser than 0.");
	}

	public string PatientName 
	{
		get => this._patientName;
		set => this._patientName = StringValidators.IsName(value)
			?? throw new ArgumentException("Not a name was passed.");
	}

	public string PatientPhone
	{
		get => this._patientPhone;
		set => this._patientPhone = StringValidators.IsUaPhoneNumber(value)
			?? throw new ArgumentException("Not a phone number was passed.");
	}
	
	public string Vaccine
	{
		get => this._vaccine;
		set => this._vaccine = VaccinationRequest.IsVaccine(value)
			?? throw new ArgumentException("Not a vaccine was passed.");
	}

	public DateOnly Date 
	{
		get
		{
			return (this._date);
		} 
		set
		{
			if (value < DateOnly.FromDateTime(DateTime.Today))
			{
				throw new Exception("Date cannot be lesser than today.");
			}

			this._date = value;
			return;
		}
	}

	public TimeOnly StartTime 
	{
		get
		{
			return (this._startTime);
		}
		set
		{
			if ((this.EndTime != default) && (value > this.EndTime))
			{
				throw new ArgumentException(
					"Start time cannot be greater than end time");
			}

			if (value < VaccinationRequest._canStart)
			{
				throw new ArgumentException("Cannot register earlier than"
					+ VaccinationRequest._canStart.ToString());
			}

			if (value > VaccinationRequest._canEnd)
			{
				throw new ArgumentException("Cannot register later than"
					+ VaccinationRequest._canEnd.ToString());
			}

			this._startTime = value;
			return;
		}
	}

	public TimeOnly EndTime 
	{
		get
		{
			return (this._endTime);
		}
		set
		{
			if (value <= this.StartTime)
			{
				throw new ArgumentException(
					"End time cannot be lesser than start time.");
			}

			if (value < VaccinationRequest._canStart.AddHours(1))
			{
				throw new ArgumentException("Registration cannot end earlier than"
					+ VaccinationRequest._canStart.AddHours(1).ToString());
			}

			if (value > VaccinationRequest._canEnd.AddHours(1))
			{
				throw new ArgumentException("Registration cannot end later than"
					+ VaccinationRequest._canEnd.AddHours(1).ToString());
			}

			this._endTime = value;
			return;
		}
	}

	public override string ToString()
	{
		string res = "";

		var elem_arr = this.GetType().GetProperties().ToArray();
		res += elem_arr[0].GetValue(this)?.ToString();
		for (int i = 1; i < elem_arr.Count(); ++i)
		{
			res += (' ' + elem_arr[i].GetValue(this)?.ToString());
		}

		return res;
	}

	public static bool TryStartTime(string? str)
		=> (TimeValidators.TryTime(str, 
			VaccinationRequest._canStart, 
			VaccinationRequest._canEnd));

	public static bool TryEndTime(string? str)
		=> (TimeValidators.TryTime(str, 
			VaccinationRequest._canStart.AddHours(1), 
			VaccinationRequest._canEnd.AddHours(1)));

	public static bool TryVaccine(string? str)
		=> ((str != null) 
			&& (_vaccineChoices.Contains(str.ToLower()) == true));

	public static string? IsVaccine(string? str)
		=> ((VaccinationRequest.TryVaccine(str) == true) ? str : null);
}
