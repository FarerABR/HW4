using BLL.Repository;
using Newtonsoft.Json;
using DAL.Entity.Product;
using DAL.Entity;
using System.Text;

namespace BLL.data_base
{
	public static class Data_Access
	{
		public static void ReadAllData()
		{
			// ########## User data ##########
			string userPath = "../../../../DAL/data/User/User_datafile.json";
			if (!File.Exists(userPath))
			{
				using (FileStream fs = File.Create(userPath))
				{ 
					Byte[] text = new UTF8Encoding(true).GetBytes("[]");
					fs.Write(text);
				}
			}
			string userStr = File.ReadAllText(userPath);
			List<User> userInsertData = JsonConvert.DeserializeObject<List<User>>(userStr);
			if(userInsertData != null)
				UserRepository.User_List = userInsertData;

			// ########## Graphics card data ##########
			string graphPath = "../../../../DAL/data/Product/GraphicsCard_datafile.json";
			if (!File.Exists(graphPath))
			{
				using (FileStream fs = File.Create(graphPath))
				{
					Byte[] text = new UTF8Encoding(true).GetBytes("[]");
					fs.Write(text);
				}
			}
			string graphStr = File.ReadAllText(graphPath);
			List<GraphicsCard> graphInsertData = JsonConvert.DeserializeObject<List<GraphicsCard>>(graphStr);
			if (graphInsertData != null)
				ProductsRepository.GraphicsCard_List = graphInsertData;

			// ########## Processor data ##########
			string procPath = "../../../../DAL/data/Product/Processor_datafile.json";
			if (!File.Exists(procPath))
			{
				using (FileStream fs = File.Create(procPath))
				{
					Byte[] text = new UTF8Encoding(true).GetBytes("[]");
					fs.Write(text);
				}
			}
			string procStr = File.ReadAllText(procPath);
			List<Processor> procInsertedData = JsonConvert.DeserializeObject<List<Processor>>(procStr);
			if (procInsertedData != null)
				ProductsRepository.Processor_List = procInsertedData;

			// ########## Ram data ##########
			string ramPath = "../../../../DAL/data/Product/Ram_datafile.json";
			if (!File.Exists(ramPath))
			{
				using (FileStream fs = File.Create(ramPath))
				{
					Byte[] text = new UTF8Encoding(true).GetBytes("[]");
					fs.Write(text);
				}
			}
			string ramStr = File.ReadAllText(ramPath);
			List<Ram> ramInsertedData = JsonConvert.DeserializeObject<List<Ram>>(ramStr);
			if (ramInsertedData != null)
				ProductsRepository.Ram_List = ramInsertedData;

			// ########## Motherboard data ##########
			string motherPath = "../../../../DAL/data/Product/Motherboard_datafile.json";
			if (!File.Exists(motherPath))
			{
				using (FileStream fs = File.Create(motherPath))
				{
					Byte[] text = new UTF8Encoding(true).GetBytes("[]");
					fs.Write(text);
				}
			}
			string motherStr = File.ReadAllText(motherPath);
			List<Motherboard> motherInsertedData = JsonConvert.DeserializeObject<List<Motherboard>>(motherStr);
			if (motherInsertedData != null)
				ProductsRepository.Motherboard_List = motherInsertedData;
		}

		public static void WriteAllData()
		{
			// ########## User data ##########
			string userStr = JsonConvert.SerializeObject(UserRepository.User_List);
			string userPath = "../../../../DAL/data/User/User_datafile.json";
			using (FileStream fs = File.Create(userPath))
			{
				Byte[] text = new UTF8Encoding(true).GetBytes(userStr);
				fs.Write(text);
			}

			// ########## Graphics card data ##########
			string graphStr = JsonConvert.SerializeObject(ProductsRepository.GraphicsCard_List);
			string graphPath = "../../../../DAL/data/Product/GraphicsCard_datafile.json";
			using (FileStream fs = File.Create(graphPath))
			{
				Byte[] text = new UTF8Encoding(true).GetBytes(graphStr);
				fs.Write(text);
			}

			// ########## Processor data ##########
			string procStr = JsonConvert.SerializeObject(ProductsRepository.Processor_List);
			string procPath = "../../../../DAL/data/Product/Processor_datafile.json";
			using (FileStream fs = File.Create(procPath))
			{
				Byte[] text = new UTF8Encoding(true).GetBytes(procStr);
				fs.Write(text);
			}

			// ########## Raam data ##########
			string ramStr = JsonConvert.SerializeObject(ProductsRepository.Ram_List);
			string ramPath = "../../../../DAL/data/Product/Ram_datafile.json";
			using (FileStream fs = File.Create(ramPath))
			{
				Byte[] text = new UTF8Encoding(true).GetBytes(ramStr);
				fs.Write(text);
			}

			// ########## MotherBoard data ##########
			string motherStr = JsonConvert.SerializeObject(ProductsRepository.Motherboard_List);
			string motherPath = "../../../../DAL/data/Product/Motherboard_datafile.json";
			using (FileStream fs = File.Create(motherPath))
			{
				Byte[] text = new UTF8Encoding(true).GetBytes(motherStr);
				fs.Write(text);
			}
		}
	}
}
