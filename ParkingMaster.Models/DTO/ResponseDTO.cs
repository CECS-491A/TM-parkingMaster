using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class ResponseDTO<T>
	{
		// Automatic Properties
		public T Data { get; set; }
		public string Error { get; set; }
	}
}
